using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CustomTools;

//Drag 2x2 blocks from UI to gridspots
public class DragUI2x2Blocks : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

	private GridHandler _grid;
	public GridHandler.Item _itemType;
	GameObject _previewPiece;

	public void OnBeginDrag(PointerEventData data)
	{
		
	}

	void Start()
	{
		_grid = GameObject.Find ("Gridhandler").GetComponent<GridHandler> ();
	}

	//Create preview piece
	public void OnDrag(PointerEventData data)
	{
		ConstantHandler.Instance.ComponentDragged = true;
		if (ConstantHandler.Instance.ComponentAdded) {
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			Destroy (_previewPiece);
			switch (_itemType) {
			case GridHandler.Item.FULL_BLOCK:
				_previewPiece = Instantiate (PrefabHandler.Instance.FullBlock, new Vector3 ((float)pos.x / 2+0.25f, pos.z, (float)pos.y / 2+0.25f), Quaternion.identity);
				_previewPiece.AddComponent<UIAddItem> ();
				_previewPiece.GetComponent<UIAddItem> ().Position = pos;
				break;
			case GridHandler.Item.HALF_BLOCK_H:
				_previewPiece = Instantiate (PrefabHandler.Instance.HalfBlockHorizontal, new Vector3 ((float)pos.x / 2 + 0.25f, pos.z, (float)pos.y / 2 + 0.25f), Quaternion.identity);
				_previewPiece.AddComponent<UIAddItem> ();
				_previewPiece.GetComponent<UIAddItem> ().Position = pos;
				break;
			case GridHandler.Item.RAMP_45:
				_previewPiece = Instantiate (PrefabHandler.Instance.Ramp45, new Vector3 ((float)pos.x / 2 + 0.25f, pos.z, (float)pos.y / 2 + 0.25f),
					Quaternion.Euler (0, -90, 0));
				_previewPiece.transform.GetChild(0).gameObject.AddComponent<UIAddItem> ();
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<UIAddItem> ().Position = pos;
				break;
			case GridHandler.Item.SHORT_RAMP:
				_previewPiece = Instantiate (PrefabHandler.Instance.ShortRamp, new Vector3 ((float)pos.x / 2 + 0.25f, pos.z, (float)pos.y / 2 + 0.25f),
					Quaternion.Euler (0, -90, 0));
				_previewPiece.transform.GetChild(0).gameObject.AddComponent<UIAddItem> ();
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<UIAddItem> ().Position = pos;
				break;
			case GridHandler.Item.WALL_45:
				_previewPiece = Instantiate (PrefabHandler.Instance.Wall45, new Vector3 ((float)pos.x / 2 + 0.25f, pos.z, (float)pos.y / 2 + 0.25f),
					Quaternion.Euler (90, 0, 0));
				_previewPiece.transform.GetChild(0).gameObject.AddComponent<UIAddItem> ();
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<UIAddItem> ().Position = pos;
				break;
			case GridHandler.Item.FAIL:
				_previewPiece = Instantiate (PrefabHandler.Instance.FailBlocks, new Vector3 ((float)pos.x / 2 + 0.25f, -0.3f, (float)pos.y / 2 + 0.25f),
					Quaternion.identity);
				_previewPiece.AddComponent<UIAddItem> ();
				_previewPiece.GetComponent<UIAddItem> ().Position = pos;
				break;
			}
		}
	}

	//Set piece at gridspot
	public void OnEndDrag(PointerEventData data)
	{
		if (_previewPiece != null)
			Destroy (_previewPiece);
		ConstantHandler.Instance.ComponentDragged = false;
		if (ConstantHandler.Instance.ComponentAdded) {
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			if (_grid.GetTileType (pos.x, pos.y,pos.z) == GridHandler.Item.EMPTY) {
				if (_grid.isOnGrid (pos.x, pos.y,pos.z + 1) && _grid.isOnGrid (pos.x + 1, pos.y,pos.z) && _grid.isOnGrid (pos.x + 1, pos.y,pos.z + 1)
					&& _grid.GetTileType (pos.x, pos.y,pos.z + 1) == GridHandler.Item.EMPTY && _grid.GetTileType (pos.x + 1, pos.y,pos.z + 1) == GridHandler.Item.EMPTY
					&& _grid.GetTileType (pos.x + 1, pos.y,pos.z) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x + 1, pos.y,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.SetTileToType (pos.x + 1, pos.y+1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.SetTileToType (pos.x, pos.y+1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.AttachItem (pos.x + 1, pos.y,pos.z);
					_grid.AttachItem (pos.x + 1, pos.y+1,pos.z);
					_grid.AttachItem (pos.x, pos.y+1,pos.z);
					_grid.SetTileToType (pos.x, pos.y,pos.z, _itemType);
					_grid.SetDirectionOfTile (pos.x, pos.y,pos.z, GridHandler.ItemDirection.UP);
					_grid.AttachItem (pos.x, pos.y,pos.z);
				} else if (_grid.isOnGrid (pos.x, pos.y,pos.z - 1) && _grid.isOnGrid (pos.x + 1, pos.y,pos.z) && _grid.isOnGrid (pos.x + 1, pos.y,pos.z - 1)
					&& _grid.GetTileType (pos.x, pos.y,pos.z - 1) == GridHandler.Item.EMPTY && _grid.GetTileType (pos.x + 1, pos.y,pos.z - 1) == GridHandler.Item.EMPTY
					&& _grid.GetTileType (pos.x + 1, pos.y,pos.z) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x + 1, pos.y,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.SetTileToType (pos.x + 1, pos.y+1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.SetTileToType (pos.x, pos.y+1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.AttachItem (pos.x + 1, pos.y,pos.z);
					_grid.AttachItem (pos.x + 1, pos.y+1,pos.z);
					_grid.AttachItem (pos.x, pos.y+1,pos.z);
					_grid.SetTileToType (pos.x, pos.y,pos.z, _itemType);
					_grid.SetDirectionOfTile (pos.x, pos.y,pos.z, GridHandler.ItemDirection.RIGHT);
					_grid.AttachItem (pos.x, pos.y,pos.z);
				} else if (_grid.isOnGrid (pos.x, pos.y,pos.z - 1) && _grid.isOnGrid (pos.x - 1, pos.y,pos.z) && _grid.isOnGrid (pos.x - 1, pos.y,pos.z - 1)
					&& _grid.GetTileType (pos.x, pos.y,pos.z - 1) == GridHandler.Item.EMPTY && _grid.GetTileType (pos.x - 1, pos.y,pos.z - 1) == GridHandler.Item.EMPTY
					&& _grid.GetTileType (pos.x - 1, pos.y,pos.z) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x, pos.y,pos.z - 1, GridHandler.Item.DISAPPEAR);
					_grid.SetTileToType (pos.x - 1, pos.y,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.SetTileToType (pos.x - 1, pos.y+1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.SetTileToType (pos.x, pos.y+1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.AttachItem (pos.x - 1, pos.y,pos.z);
					_grid.AttachItem (pos.x - 1, pos.y+1,pos.z);
					_grid.AttachItem (pos.x, pos.y+1,pos.z);
					_grid.SetTileToType (pos.x, pos.y,pos.z, _itemType);
					_grid.SetDirectionOfTile (pos.x, pos.y,pos.z, GridHandler.ItemDirection.DOWN);
					_grid.AttachItem (pos.x, pos.y,pos.z);
				} else if (_grid.isOnGrid (pos.x, pos.y,pos.z + 1) && _grid.isOnGrid (pos.x - 1, pos.y,pos.z) && _grid.isOnGrid (pos.x - 1, pos.y,pos.z + 1)
					&& _grid.GetTileType (pos.x, pos.y,pos.z + 1) == GridHandler.Item.EMPTY
					&& _grid.GetTileType (pos.x - 1, pos.y,pos.z + 1) == GridHandler.Item.EMPTY
					&& _grid.GetTileType (pos.x - 1, pos.y,pos.z) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x - 1, pos.y,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.SetTileToType (pos.x - 1, pos.y+1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.SetTileToType (pos.x, pos.y+1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.AttachItem (pos.x - 1, pos.y,pos.z);
					_grid.AttachItem (pos.x - 1, pos.y+1,pos.z);
					_grid.AttachItem (pos.x, pos.y+1,pos.z);
					_grid.SetTileToType (pos.x, pos.y,pos.z, _itemType);
					_grid.SetDirectionOfTile (pos.x, pos.y,pos.z, GridHandler.ItemDirection.LEFT);
					_grid.AttachItem (pos.x, pos.y,pos.z);
				}
			}
			switch (_itemType) {
			case GridHandler.Item.FULL_BLOCK:
				_grid.GetObj (pos.x, pos.y, pos.z + 2).GetComponent<Renderer> ().enabled = true;
				_grid.GetObj (pos.x, pos.y, pos.z + 2).GetComponent<SphereCollider> ().enabled = true;
				_grid.GetObj (pos.x+1, pos.y, pos.z + 2).GetComponent<Renderer> ().enabled = true;
				_grid.GetObj (pos.x+1, pos.y, pos.z + 2).GetComponent<SphereCollider> ().enabled = true;
				_grid.GetObj (pos.x, pos.y+1, pos.z + 2).GetComponent<Renderer> ().enabled = true;
				_grid.GetObj (pos.x, pos.y+1, pos.z + 2).GetComponent<SphereCollider> ().enabled = true;
				_grid.GetObj (pos.x+1, pos.y+1, pos.z + 2).GetComponent<Renderer> ().enabled = true;
				_grid.GetObj (pos.x+1, pos.y+1, pos.z + 2).GetComponent<SphereCollider> ().enabled = true;
				Debug.Log(new Vector3(pos.x,pos.y,pos.z+1) + "activated");
				break;
			case GridHandler.Item.HALF_BLOCK_H:
				_grid.GetObj (pos.x, pos.y, pos.z + 2).GetComponent<Renderer> ().enabled = true;
				_grid.GetObj (pos.x, pos.y, pos.z + 2).GetComponent<SphereCollider> ().enabled = true;
				_grid.GetObj (pos.x+1, pos.y, pos.z + 2).GetComponent<Renderer> ().enabled = true;
				_grid.GetObj (pos.x+1, pos.y, pos.z + 2).GetComponent<SphereCollider> ().enabled = true;
				_grid.GetObj (pos.x, pos.y+1, pos.z + 2).GetComponent<Renderer> ().enabled = true;
				_grid.GetObj (pos.x, pos.y+1, pos.z + 2).GetComponent<SphereCollider> ().enabled = true;
				_grid.GetObj (pos.x+1, pos.y+1, pos.z + 2).GetComponent<Renderer> ().enabled = true;
				_grid.GetObj (pos.x+1, pos.y+1, pos.z + 2).GetComponent<SphereCollider> ().enabled = true;
				Debug.Log(new Vector3(pos.x,pos.y,pos.z+1) + "activated");
				break;
			}
			ConstantHandler.Instance.PositionAdded = IVector3.zero;
			ConstantHandler.Instance.ComponentAdded = false;
		}
	}
}
