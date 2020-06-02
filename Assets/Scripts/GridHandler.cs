using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

//Main class which handles grid data
public class GridHandler : MonoBehaviour {

	private Tile[,,] _tile;
	GameObject obj;

	public GameObject ball;
	private Dictionary<GameObject, Vector3> _tileToPos;
	GameObject[] _allGameObjects;
	GameObject nearest;
	// Create empty grid space
	void Start () {
		_allGameObjects = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[];
		_tileToPos = new Dictionary<GameObject, Vector3> ();
		_tile = new Tile[ConstantHandler.Instance.GridWidth, ConstantHandler.Instance.GridHeight, ConstantHandler.Instance.GridLength];
		for (int i = 0; i < ConstantHandler.Instance.GridWidth; i++) {
			for (int j = 0; j < ConstantHandler.Instance.GridHeight; j++) {
				for (int k = 0; k < ConstantHandler.Instance.GridLength; k++) {
					float x_pos = (float)i / 2;
					float y_pos = (float)j / 2;
					float z_pos = (float)k / 2;
					Vector3 posi = new Vector3 (x_pos, z_pos, y_pos);
					GameObject obj = FindGameObjectAtPosition (posi);
					if(obj==null)
					{
						obj = Instantiate (PrefabHandler.Instance.EmptyTile, posi, Quaternion.identity) as GameObject;
						obj.AddComponent<UIAddItem> ();
					}
					obj.GetComponent<UIAddItem> ().Position = new IVector3 (i, j, k);
					_tile [i, j, k] = new Tile (obj, new IVector3 (i, j, k));
					_tileToPos.Add (obj, posi);
					if (k > 0) {
						obj.GetComponent<Renderer> ().enabled = false;
						obj.GetComponent<SphereCollider> ().enabled = false;
					}
				}
			}
		}
		GameObject objBall = FindGameObjectAtPosition (new Vector3(2.75f,4.996076f,2.75f));
		if (objBall != null) {
			Destroy (objBall);
			GameHandler.Instance.CreateGameBall (5, 5, 5);
		}
		else
			GameHandler.Instance.CreateGameBall (5, 5, 5);
		GameObject objGoal = FindGameObjectAtPosition (new Vector3 (1, -0.5f, 1));
		if (objGoal == null) {
			IVector3 pos = new IVector3 (2, 2, 0);
			GameHandler.Instance._grid.SetTileToType (pos.x, pos.y, pos.z, GridHandler.Item.GOAL);
			GameHandler.Instance._grid.SetTileToType (pos.x + 1, pos.y, pos.z, GridHandler.Item.DISAPPEAR);
			GameHandler.Instance._grid.SetTileToType (pos.x - 1, pos.y, pos.z, GridHandler.Item.DISAPPEAR);
			GameHandler.Instance._grid.SetTileToType (pos.x, pos.y + 1, pos.z, GridHandler.Item.DISAPPEAR);
			GameHandler.Instance._grid.SetTileToType (pos.x, pos.y - 1, pos.z, GridHandler.Item.DISAPPEAR);
			GameHandler.Instance._grid.SetTileToType (pos.x + 1, pos.y + 1, pos.z, GridHandler.Item.DISAPPEAR);
			GameHandler.Instance._grid.SetTileToType (pos.x + 1, pos.y - 1, pos.z, GridHandler.Item.DISAPPEAR);
			GameHandler.Instance._grid.SetTileToType (pos.x - 1, pos.y + 1, pos.z, GridHandler.Item.DISAPPEAR);
			GameHandler.Instance._grid.SetTileToType (pos.x - 1, pos.y - 1, pos.z, GridHandler.Item.DISAPPEAR);
			GameHandler.Instance._grid.AttachItem (pos.x, pos.y, pos.z);

			GameHandler.Instance._grid.AttachItem (pos.x - 1, pos.y - 1, pos.z);
			GameHandler.Instance._grid.AttachItem (pos.x + 1, pos.y, pos.z);
			GameHandler.Instance._grid.AttachItem (pos.x - 1, pos.y, pos.z);
			GameHandler.Instance._grid.AttachItem (pos.x, pos.y + 1, pos.z);
			GameHandler.Instance._grid.AttachItem (pos.x, pos.y - 1, pos.z);
			GameHandler.Instance._grid.AttachItem (pos.x + 1, pos.y + 1, pos.z);
			GameHandler.Instance._grid.AttachItem (pos.x + 1, pos.y - 1, pos.z);
			GameHandler.Instance._grid.AttachItem (pos.x - 1, pos.y + 1, pos.z);
		}
	}

	//Helper functions to retrieve data from struct
	public IVector3 GetGoalPosition()
	{
		for (int i = 0; i < ConstantHandler.Instance.GridWidth; i++) {
			for (int j = 0; j < ConstantHandler.Instance.GridHeight; j++) {
				if(GetTileType(i,j,0)==Item.GOAL)
					return new IVector3(i,j,0);
			}
		}
		return new IVector3 (1, 1, 0);
	}

	private GameObject FindGameObjectAtPosition(Vector3 pos)
	{
		GameObject obj = null;
		foreach (GameObject g in _allGameObjects) {
			if (g.transform.position == pos) {
				obj = g;
			}
		}
		return obj;
	}
	
	public bool isOnGrid(int X, int Y, int Z)
	{
		return (X >= 0 && X < ConstantHandler.Instance.GridWidth) && (Y >= 0 && Y < ConstantHandler.Instance.GridHeight) && (Z>=0 &&
			Z<ConstantHandler.Instance.GridLength);
	}

	public GameObject GetObj(int X, int Y, int Z)
	{
		if (isOnGrid (X, Y,Z))
			return _tile [X, Y,Z].Object;
		return null;
	}

	public GameObject GetItemGO(int X, int Y, int Z)
	{
		if (isOnGrid (X, Y,Z))
			return _tile [X, Y,Z].ItemAttached;
		return null;
	}

	public void AttachItem(int X, int Y, int Z)
	{
		if (isOnGrid (X, Y,Z))
			_tile [X, Y,Z].AttachItem ();
	}

	public void SetDirectionOfTile(int X, int Y,int Z, ItemDirection dir)
	{
		if (isOnGrid (X, Y,Z))
			_tile [X, Y,Z].Direction = dir;
	}

	public void SetTileToType(int X, int Y, int Z, Item type)
	{
		if (isOnGrid (X, Y,Z))
			_tile [X, Y,Z].ItemType = type;
	}

	public void SetFrictionForTile(int X, int Y, int Z, FrictionValue val)
	{
		if (isOnGrid (X, Y,Z))
			_tile [X, Y,Z].Friction = val;
	}

	public FrictionValue GetTileFriction(int X, int Y, int Z)
	{
		if (isOnGrid (X, Y, Z))
			return _tile [X, Y,Z].Friction;
		return FrictionValue.LOW;
	}

	public Item GetTileType(int X, int Y, int Z)
	{
		if (isOnGrid (X, Y,Z))
			return _tile [X, Y,Z].ItemType;
		return Item.EMPTY;
	}

	public ItemDirection GetDirectionOfTile(int X, int Y, int Z)
	{
		if (isOnGrid (X, Y,Z))
			return _tile [X, Y,Z].Direction;
		return ItemDirection.LEFT;
	}

	//Different item types
	public enum Item
	{
		EMPTY,
		RAMP_45,
		WALL_45,
		SHORT_RAMP,
		TALL_RAMP,
		FULL_BLOCK,
		HALF_BLOCK_H,
		HALF_BLOCK_V,
		QUARTER_WALL,
		BALL,
		GOAL,
		DISAPPEAR,
		FAIL
	}

	//Item directions
	public enum ItemDirection
	{
		LEFT, RIGHT, UP, DOWN
	}

	//Firction values
	public enum FrictionValue
	{
		LOW, MEDIUM, HIGH
	}

	//Data struct to hold for Tile with diffeerent properties
	public struct Tile
	{
		private ItemDirection _direction;
		public ItemDirection Direction
		{
			set { _direction = value; }
			get { return _direction; }
		}
		private IVector3 _position;
		public IVector3 Position {
			set { _position = value; }
			get { return _position; }
		}

		private FrictionValue _frictionVal;
		public FrictionValue Friction
		{
			get { return _frictionVal; }
			set { _frictionVal = value; }
		}

		private Item _itemType;
		public Item ItemType
		{
			set { _itemType = value; }
			get { return _itemType; }
		}

		private GameObject _obj;
		public GameObject Object
		{
			get { return _obj; }
		}

		private GameObject _itemAttached;
		public GameObject ItemAttached
		{
			get { return _itemAttached; }
		}

		public Tile(GameObject obj,IVector3 pos)
		{
			_position=pos;
			_itemType=Item.EMPTY;
			_itemAttached=null;
			_obj=obj;
			_frictionVal=FrictionValue.LOW;
			_direction=ItemDirection.LEFT;
		}

		private void ClearItem()
		{
			if (_itemAttached != null) {
				Destroy (_itemAttached);
				_obj.GetComponent<Renderer> ().enabled = true;
				_obj.GetComponent<SphereCollider> ().enabled = true;
			}
		}

		public void AttachItem()
		{
			ClearItem ();
			switch (_itemType) {
			//EMPTY
			case Item.EMPTY:
				_obj.GetComponent<Renderer> ().enabled = true;
				_obj.GetComponent<SphereCollider> ().enabled = true;
				break;
			//DISAPPEAR
			case Item.DISAPPEAR:
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				break;
			//FULL BLOCK
			case Item.FULL_BLOCK:
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				switch (_direction) {
				case ItemDirection.UP:
					_itemAttached = Instantiate (PrefabHandler.Instance.FullBlock, _obj.transform.position + new Vector3 (0.25f, 0, 0.25f),
						Quaternion.identity) as GameObject;
					break;
				case ItemDirection.RIGHT:
					_itemAttached = Instantiate (PrefabHandler.Instance.FullBlock, _obj.transform.position + new Vector3 (0.25f, 0, -0.25f),
						Quaternion.identity) as GameObject;
					break;
				case ItemDirection.DOWN:
					_itemAttached = Instantiate (PrefabHandler.Instance.FullBlock, _obj.transform.position + new Vector3 (-0.25f, 0, -0.25f),
						Quaternion.identity) as GameObject;
					break;
				case ItemDirection.LEFT:
					_itemAttached = Instantiate (PrefabHandler.Instance.FullBlock, _obj.transform.position + new Vector3 (-0.25f, 0, 0.25f),
						Quaternion.identity) as GameObject;
					break;
				}
				if (GameHandler.Instance._gameState == GameHandler.GameState.DESIGNER) {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				} else {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.GetComponent<Renderer> ().material.color = Color.green;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.GetComponent<Renderer> ().material.color = Color.yellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.GetComponent<Renderer> ().material.color = Color.red;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				}
				_itemAttached.AddComponent<Rotate2x2Blocks> ();
				_itemAttached.GetComponent<Rotate2x2Blocks> ().Position = _position;
				_itemAttached.GetComponent<Rotate2x2Blocks> ().Direction = _direction;
				_itemAttached.GetComponent<Rotate2x2Blocks> ().Type = Item.FULL_BLOCK;
				_itemAttached.AddComponent<ApplyFrictionPad> ();
				_itemAttached.GetComponent<ApplyFrictionPad> ().Position = _position;
				break;
			//HALF BLOCK HORIZONTAL
			case Item.HALF_BLOCK_H:
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				switch (_direction) {
				case ItemDirection.UP:
					_itemAttached = Instantiate (PrefabHandler.Instance.HalfBlockHorizontal, _obj.transform.position + new Vector3 (0.25f, 0, 0.25f),
						Quaternion.identity) as GameObject;
					break;
				case ItemDirection.RIGHT:
					_itemAttached = Instantiate (PrefabHandler.Instance.HalfBlockHorizontal, _obj.transform.position + new Vector3 (0.25f, 0, -0.25f),
						Quaternion.identity) as GameObject;
					break;
				case ItemDirection.DOWN:
					_itemAttached = Instantiate (PrefabHandler.Instance.HalfBlockHorizontal, _obj.transform.position + new Vector3 (-0.25f, 0, -0.25f),
						Quaternion.identity) as GameObject;
					break;
				case ItemDirection.LEFT:
					_itemAttached = Instantiate (PrefabHandler.Instance.HalfBlockHorizontal, _obj.transform.position + new Vector3 (-0.25f, 0, 0.25f),
						Quaternion.identity) as GameObject;
					break;
				}
				if (GameHandler.Instance._gameState == GameHandler.GameState.DESIGNER) {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				} else {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.GetComponent<Renderer> ().material.color = Color.green;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.GetComponent<Renderer> ().material.color = Color.yellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.GetComponent<Renderer> ().material.color = Color.red;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				}
				_itemAttached.AddComponent<Rotate2x2Blocks> ();
				_itemAttached.GetComponent<Rotate2x2Blocks> ().Position = _position;
				_itemAttached.GetComponent<Rotate2x2Blocks> ().Direction = _direction;
				_itemAttached.GetComponent<Rotate2x2Blocks> ().Type = Item.HALF_BLOCK_H;
				_itemAttached.AddComponent<ApplyFrictionPad> ();
				_itemAttached.GetComponent<ApplyFrictionPad> ().Position = _position;
				break;
			case Item.HALF_BLOCK_V:
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				switch(_direction)
				{
				case ItemDirection.UP:
					_itemAttached = Instantiate (PrefabHandler.Instance.HalfBlockVertical, _obj.transform.position + new Vector3 (0, 0, 0.25f),
						Quaternion.Euler(0,0,90)) as GameObject;
					break;
				case ItemDirection.RIGHT:
					_itemAttached = Instantiate (PrefabHandler.Instance.HalfBlockVertical, _obj.transform.position + new Vector3 (0.25f, 0, 0),
						Quaternion.Euler(0,90,90)) as GameObject;
					break;
				case ItemDirection.DOWN:
					_itemAttached = Instantiate (PrefabHandler.Instance.HalfBlockVertical, _obj.transform.position + new Vector3 (0, 0, -0.25f),
						Quaternion.Euler(0,0,90)) as GameObject;
					break;
				case ItemDirection.LEFT:
					_itemAttached = Instantiate (PrefabHandler.Instance.HalfBlockVertical, _obj.transform.position + new Vector3 (-0.25f, 0, 0),
						Quaternion.Euler(0,90,90)) as GameObject;
					break;
				}
				if (GameHandler.Instance._gameState == GameHandler.GameState.DESIGNER) {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				} else {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.GetComponent<Renderer> ().material.color = Color.green;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.GetComponent<Renderer> ().material.color = Color.yellow;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.GetComponent<Renderer> ().material.color = Color.red;
						_itemAttached.GetComponent<BoxCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				}
				_itemAttached.AddComponent<Rotate2x1Blocks> ();
				_itemAttached.GetComponent<Rotate2x1Blocks> ().Position = _position;
				_itemAttached.GetComponent<Rotate2x1Blocks> ().Direction = _direction;
				_itemAttached.GetComponent<Rotate2x1Blocks> ().Type = Item.HALF_BLOCK_V;
				_itemAttached.AddComponent<ApplyFrictionPad> ();
				_itemAttached.GetComponent<ApplyFrictionPad> ().Position = _position;
				break;
			case Item.SHORT_RAMP:
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				switch (_direction) {
				case ItemDirection.UP:
					_itemAttached = Instantiate (PrefabHandler.Instance.ShortRamp, _obj.transform.position + new Vector3 (0.25f, 0, 0.25f),
						Quaternion.Euler (0, -90, 0)) as GameObject;
					break;
				case ItemDirection.RIGHT:
					_itemAttached = Instantiate (PrefabHandler.Instance.ShortRamp, _obj.transform.position + new Vector3 (0.25f, 0, -0.25f),
						Quaternion.identity) as GameObject;
					break;
				case ItemDirection.DOWN:
					_itemAttached = Instantiate (PrefabHandler.Instance.ShortRamp, _obj.transform.position + new Vector3 (-0.25f, 0, -0.25f),
						Quaternion.Euler (0, 90, 0)) as GameObject;
					break;
				case ItemDirection.LEFT:
					_itemAttached = Instantiate (PrefabHandler.Instance.ShortRamp, _obj.transform.position + new Vector3 (-0.25f, 0, 0.25f),
						Quaternion.Euler (0, 180, 0)) as GameObject;
					break;
				}
				if (GameHandler.Instance._gameState == GameHandler.GameState.DESIGNER) {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionLow;

						break;
					case FrictionValue.MEDIUM:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionMed;

						break;
					case FrictionValue.HIGH:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionHigh;

						break;
					}
				} else {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.green;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.red;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				}
				_itemAttached.transform.GetChild(0).gameObject.AddComponent<Rotate2x2Blocks> ();
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x2Blocks> ().Position = _position;
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x2Blocks> ().Direction = _direction;
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x2Blocks> ().Type = Item.SHORT_RAMP;
				_itemAttached.transform.GetChild(0).gameObject.AddComponent<ApplyFrictionPad> ();
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<ApplyFrictionPad> ().Position = _position;
				break;
			case Item.RAMP_45:
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				switch (_direction) {
				case ItemDirection.UP:
					_itemAttached = Instantiate (PrefabHandler.Instance.Ramp45, _obj.transform.position + new Vector3 (0.25f, 0, 0.25f),
						Quaternion.Euler(0,-90,0)) as GameObject;
					break;
				case ItemDirection.RIGHT:
					_itemAttached = Instantiate (PrefabHandler.Instance.Ramp45, _obj.transform.position + new Vector3 (0.25f, 0, -0.25f),
						Quaternion.identity) as GameObject;
					break;
				case ItemDirection.DOWN:
					_itemAttached = Instantiate (PrefabHandler.Instance.Ramp45, _obj.transform.position + new Vector3 (-0.25f, 0, -0.25f),
						Quaternion.Euler(0,90,0)) as GameObject;
					break;
				case ItemDirection.LEFT:
					_itemAttached = Instantiate (PrefabHandler.Instance.Ramp45, _obj.transform.position + new Vector3 (-0.25f, 0, 0.25f),
						Quaternion.Euler(0,180,0)) as GameObject;
					break;
				}
				if (GameHandler.Instance._gameState == GameHandler.GameState.DESIGNER) {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionLow;

						break;
					case FrictionValue.MEDIUM:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionMed;

						break;
					case FrictionValue.HIGH:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionHigh;

						break;
					}
				} else {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.green;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.red;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				}
				_itemAttached.transform.GetChild(0).gameObject.AddComponent<Rotate2x2Blocks> ();
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x2Blocks> ().Position = _position;
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x2Blocks> ().Direction = _direction;
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x2Blocks> ().Type = Item.RAMP_45;
				_itemAttached.transform.GetChild(0).gameObject.AddComponent<ApplyFrictionPad> ();
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<ApplyFrictionPad> ().Position = _position;
				break;
			case Item.TALL_RAMP:
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				switch(_direction)
				{
				case ItemDirection.UP:
					_itemAttached = Instantiate (PrefabHandler.Instance.TallRamp, _obj.transform.position + new Vector3 (0f, 0, 0.25f),
						Quaternion.Euler(0,180,0)) as GameObject;
					break;
				case ItemDirection.RIGHT:
					_itemAttached = Instantiate (PrefabHandler.Instance.TallRamp, _obj.transform.position + new Vector3 (0.25f, 0, 0f),
						Quaternion.Euler(0,-90,0)) as GameObject;
					break;
				case ItemDirection.DOWN:
					_itemAttached = Instantiate (PrefabHandler.Instance.TallRamp, _obj.transform.position + new Vector3 (0f, 0, -0.25f),
						Quaternion.identity) as GameObject;
					break;
				case ItemDirection.LEFT:
					_itemAttached = Instantiate (PrefabHandler.Instance.TallRamp, _obj.transform.position + new Vector3 (-0.25f, 0, 0),
						Quaternion.Euler(0,90,0)) as GameObject;
					break;
				}
				if (GameHandler.Instance._gameState == GameHandler.GameState.DESIGNER) {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionLow;

						break;
					case FrictionValue.MEDIUM:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionMed;

						break;
					case FrictionValue.HIGH:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionHigh;

						break;
					}
				} else {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.green;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.red;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				}
				_itemAttached.transform.GetChild(0).gameObject.AddComponent<Rotate2x1Blocks> ();
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x1Blocks> ().Position = _position;
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x1Blocks> ().Direction = _direction;
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x1Blocks> ().Type = Item.TALL_RAMP;
				_itemAttached.transform.GetChild(0).gameObject.AddComponent<ApplyFrictionPad> ();
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<ApplyFrictionPad> ().Position = _position;
				break;
			case Item.WALL_45:
				//Debug.Log (_direction);
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				switch(_direction)
				{
				case ItemDirection.UP:
					_itemAttached = Instantiate (PrefabHandler.Instance.Wall45, _obj.transform.position + new Vector3 (0.25f, 0, 0.25f),
						Quaternion.Euler(90,0,0)) as GameObject;
					break;
				case ItemDirection.RIGHT:
					_itemAttached = Instantiate (PrefabHandler.Instance.Wall45, _obj.transform.position + new Vector3 (0.25f, 0, -0.25f),
						Quaternion.Euler(90,90,0)) as GameObject;
					break;
				case ItemDirection.DOWN:
					_itemAttached = Instantiate (PrefabHandler.Instance.Wall45, _obj.transform.position + new Vector3 (-0.25f, 0, -0.25f),
						Quaternion.Euler(90,180,0)) as GameObject;
					break;
				case ItemDirection.LEFT:
					_itemAttached = Instantiate (PrefabHandler.Instance.Wall45, _obj.transform.position + new Vector3 (-0.25f, 0, 0.25f),
						Quaternion.Euler(90,-90,0)) as GameObject;
					break;
				}
				if (GameHandler.Instance._gameState == GameHandler.GameState.DESIGNER) {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionLow;

						break;
					case FrictionValue.MEDIUM:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionMed;

						break;
					case FrictionValue.HIGH:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.BoltedYellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionHigh;

						break;
					}
				} else {
					switch (_frictionVal) {
					case FrictionValue.LOW:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.green;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionLow;
						break;
					case FrictionValue.MEDIUM:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionMed;
						break;
					case FrictionValue.HIGH:
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = Color.red;
						_itemAttached.transform.GetChild (0).gameObject.GetComponent<MeshCollider> ().material = PrefabHandler.Instance.FrictionHigh;
						break;
					}
				}
				_itemAttached.transform.GetChild(0).gameObject.AddComponent<Rotate2x2Blocks> ();
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x2Blocks> ().Position = _position;
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x2Blocks> ().Direction = _direction;
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<Rotate2x2Blocks> ().Type = Item.WALL_45;
				_itemAttached.transform.GetChild(0).gameObject.AddComponent<ApplyFrictionPad> ();
				_itemAttached.transform.GetChild(0).gameObject.GetComponent<ApplyFrictionPad> ().Position = _position;
				break;
			case Item.GOAL:
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				_itemAttached = Instantiate (PrefabHandler.Instance.Goal, new Vector3 (_obj.transform.position.x, -0.5f, _obj.transform.position.z)
					, Quaternion.identity) as GameObject;
				foreach (Transform child in _itemAttached.transform) {
					child.gameObject.AddComponent<DeleteGoal> ();
					child.gameObject.GetComponent<DeleteGoal> ().Position = _position;
				}
				break;
			case Item.FAIL:
				_obj.GetComponent<Renderer> ().enabled = false;
				_obj.GetComponent<SphereCollider> ().enabled = false;
				_itemAttached = Instantiate (PrefabHandler.Instance.FailBlocks, _obj.transform.position+ new Vector3(0.25f,-0.5f,0.25f), Quaternion.identity) as GameObject;
				_itemAttached.AddComponent<Rotate2x2Blocks> ();
				_itemAttached.GetComponent<Rotate2x2Blocks> ().Position = _position;
				_itemAttached.GetComponent<Rotate2x2Blocks> ().Direction = _direction;
				_itemAttached.GetComponent<Rotate2x2Blocks> ().Type = Item.FAIL;
				break;
			}
		}
	}
}
