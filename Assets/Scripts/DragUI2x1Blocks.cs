using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CustomTools;

//Drag 2x1 blocks from UI to gridspot
public class DragUI2x1Blocks : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

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

	public void OnDrag(PointerEventData data)
	{
		ConstantHandler.Instance.ComponentDragged = true;
		if (ConstantHandler.Instance.ComponentAdded) {
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			Destroy (_previewPiece);
			switch (_itemType) {
			case GridHandler.Item.HALF_BLOCK_V:
				_previewPiece = Instantiate (PrefabHandler.Instance.HalfBlockVertical, new Vector3 ((float)pos.x / 2 - 0.25f, pos.z, (float)pos.y / 2),
					Quaternion.Euler (0, 90, 90));
				_previewPiece.AddComponent<UIAddItem> ();
				_previewPiece.GetComponent<UIAddItem> ().Position = pos;
				break;
			case GridHandler.Item.TALL_RAMP:
				_previewPiece = Instantiate (PrefabHandler.Instance.TallRamp, new Vector3 ((float)pos.x / 2 - 0.25f, pos.z, (float)pos.y / 2),
					Quaternion.Euler (0, 90, 0));
				_previewPiece.transform.GetChild(0).gameObject.AddComponent<UIAddItem> ();
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<UIAddItem> ().Position = pos;
				break;
			}
		}
		//gameObject.GetComponent<RectTransform> ().position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
	}

	public void OnEndDrag(PointerEventData data)
	{
		if (_previewPiece != null)
			Destroy (_previewPiece);
		ConstantHandler.Instance.ComponentDragged = false;
		if (ConstantHandler.Instance.ComponentAdded) {
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			if (_grid.GetTileType (pos.x, pos.y,pos.z) == GridHandler.Item.EMPTY) {
				if (_grid.isOnGrid (pos.x - 1, pos.y,pos.z)
					&& _grid.GetTileType (pos.x - 1, pos.y,pos.z) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x - 1, pos.y,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.AttachItem (pos.x - 1, pos.y,pos.z);
					_grid.SetTileToType (pos.x, pos.y,pos.z, _itemType);
					_grid.SetDirectionOfTile (pos.x, pos.y,pos.z, GridHandler.ItemDirection.LEFT);
					_grid.AttachItem (pos.x, pos.y,pos.z);
				} else if (_grid.isOnGrid (pos.x, pos.y+1,pos.z)
					&& _grid.GetTileType (pos.x, pos.y+1,pos.z) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x, pos.y+1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.AttachItem (pos.x, pos.y+1,pos.z);
					_grid.SetTileToType (pos.x, pos.y,pos.z, _itemType);
					_grid.SetDirectionOfTile (pos.x, pos.y,pos.z, GridHandler.ItemDirection.UP);
					_grid.AttachItem (pos.x, pos.y,pos.z);
				} else if (_grid.isOnGrid (pos.x + 1, pos.y,pos.z)
					&& _grid.GetTileType (pos.x + 1, pos.y,pos.z) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x + 1, pos.y,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.AttachItem (pos.x + 1, pos.y,pos.z);
					_grid.SetTileToType (pos.x, pos.y,pos.z, _itemType);
					_grid.SetDirectionOfTile (pos.x, pos.y,pos.z, GridHandler.ItemDirection.RIGHT);
					_grid.AttachItem (pos.x, pos.y,pos.z);
				} else if (_grid.isOnGrid (pos.x, pos.y-1,pos.z)
					&& _grid.GetTileType (pos.x, pos.y-1,pos.z) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x, pos.y-1,pos.z, GridHandler.Item.DISAPPEAR);
					_grid.AttachItem (pos.x, pos.y-1,pos.z);
					_grid.SetTileToType (pos.x, pos.y,pos.z, _itemType);
					_grid.SetDirectionOfTile (pos.x, pos.y,pos.z, GridHandler.ItemDirection.DOWN);
					_grid.AttachItem (pos.x, pos.y,pos.z);
				}
			}
			ConstantHandler.Instance.PositionAdded = IVector3.zero;
			ConstantHandler.Instance.ComponentAdded = false;
		}
	}
}
