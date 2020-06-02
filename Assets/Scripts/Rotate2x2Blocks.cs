using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

//Right click to delete the 2x2 blocks and D on hover to delete
public class Rotate2x2Blocks : MonoBehaviour {
	private GridHandler.ItemDirection _direction;
	public GridHandler.ItemDirection Direction
	{
		set { _direction = value; }
		get { return _direction; }
	}

	private IVector3 _position;
	public IVector3 Position
	{
		set { _position = value; }
		get { return _position; }
	}
	private GridHandler.Item _type;
	public GridHandler.Item Type
	{
		set { _type = value; }
		get { return _type; }
	}
	private Vector3 originalPos;
	RaycastHit hitMovement;
	RaycastHit[] hits;
	Ray ray;
	bool _hitSphere;
	IVector2 newPos;

	private float lastClickTime=0.0f;

	void Start () {
		originalPos = transform.position;
	}

	void OnMouseOver()
	{
		if (Input.GetKey(KeyCode.D)) {
			switch (_direction) {
			case GridHandler.ItemDirection.UP:
				GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y+1, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y+1, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _position.z, GridHandler.ItemDirection.LEFT);
				GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
				GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y+1, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y+1, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
				break;
			case GridHandler.ItemDirection.LEFT:
				GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y + 1, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _position.z, GridHandler.ItemDirection.LEFT);
				GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y + 1, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y + 1, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
				break;
			case GridHandler.ItemDirection.DOWN:
				GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y-1, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y - 1, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _position.z, GridHandler.ItemDirection.LEFT);
				GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y - 1, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
				break;
			case GridHandler.ItemDirection.RIGHT:
				GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y - 1, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _position.z, GridHandler.ItemDirection.LEFT);
				GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y - 1, _position.z);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
				break;
			}
			switch (_type) {
			case GridHandler.Item.FULL_BLOCK:
				GameHandler.Instance._grid.GetObj (_position.x, _position.y, _position.z + 2).GetComponent<Renderer> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x, _position.y, _position.z + 2).GetComponent<SphereCollider> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x + 1, _position.y, _position.z + 2).GetComponent<Renderer> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x + 1, _position.y, _position.z + 2).GetComponent<SphereCollider> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x, _position.y + 1, _position.z + 2).GetComponent<Renderer> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x, _position.y + 1, _position.z + 2).GetComponent<SphereCollider> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x + 1, _position.y + 1, _position.z + 2).GetComponent<Renderer> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x + 1, _position.y + 1, _position.z + 2).GetComponent<SphereCollider> ().enabled = false;
				//Debug.Log(new Vector3(pos.x,pos.y,pos.z+1) + "activated");
				break;
			case GridHandler.Item.HALF_BLOCK_H:
				GameHandler.Instance._grid.GetObj (_position.x, _position.y, _position.z + 2).GetComponent<Renderer> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x, _position.y, _position.z + 2).GetComponent<SphereCollider> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x + 1, _position.y, _position.z + 2).GetComponent<Renderer> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x + 1, _position.y, _position.z + 2).GetComponent<SphereCollider> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x, _position.y + 1, _position.z + 2).GetComponent<Renderer> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x, _position.y + 1, _position.z + 2).GetComponent<SphereCollider> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x + 1, _position.y + 1, _position.z + 2).GetComponent<Renderer> ().enabled = false;
				GameHandler.Instance._grid.GetObj (_position.x + 1, _position.y + 1, _position.z + 2).GetComponent<SphereCollider> ().enabled = false;
				//Debug.Log(new Vector3(pos.x,pos.y,pos.z+1) + "activated");
				break;
			}
		}
		if (Input.GetMouseButtonUp (1)) {
			if (_type != GridHandler.Item.HALF_BLOCK_H && _type != GridHandler.Item.FULL_BLOCK) {
				switch (_direction) {
				case GridHandler.ItemDirection.UP:
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, _position.z, _type);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x + 1, _position.y, _position.z, GridHandler.ItemDirection.LEFT);
					GameHandler.Instance._grid.SetFrictionForTile (_position.x + 1, _position.y, _position.z, GameHandler.Instance._grid.GetTileFriction (_position.x, _position.y, _position.z));
					GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
					GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y, _position.z);
					break;
				case GridHandler.ItemDirection.LEFT:
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y+1, _position.z, _type);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y+1, _position.z, GridHandler.ItemDirection.DOWN);
					GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y+1, _position.z, GameHandler.Instance._grid.GetTileFriction (_position.x, _position.y, _position.z));
					GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y+1, _position.z);
					break;
				case GridHandler.ItemDirection.DOWN:
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, _position.z, _type);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x - 1, _position.y, _position.z, GridHandler.ItemDirection.RIGHT);
					GameHandler.Instance._grid.SetFrictionForTile (_position.x - 1, _position.y, _position.z, GameHandler.Instance._grid.GetTileFriction (_position.x, _position.y, _position.z));
					GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
					GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y, _position.z);
					break;
				case GridHandler.ItemDirection.RIGHT:
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y-1, _position.z, _type);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y-1, _position.z, GridHandler.ItemDirection.UP);
					GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y-1, _position.z, GameHandler.Instance._grid.GetTileFriction (_position.x, _position.y, _position.z));
					GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1, _position.z);
					break;
				}
			}
		}
	}
}
