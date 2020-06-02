using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CustomTools;

//Drag goal from UI to gridspot
public class DragGoal : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

	GameObject _previewPiece;
	public void OnBeginDrag(PointerEventData data)
	{
	}

	public void OnDrag(PointerEventData data)
	{
		ConstantHandler.Instance.ComponentDragged = true;
		if (ConstantHandler.Instance.ComponentAdded) {
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			Destroy (_previewPiece);
			_previewPiece = Instantiate (PrefabHandler.Instance.Goal, new Vector3 ((float)pos.x / 2, -0.5f, (float)pos.y / 2),
				Quaternion.identity);
			Destroy (_previewPiece.GetComponent<Rigidbody> ());
		}
		//gameObject.GetComponent<RectTransform> ().position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
	}

	public void OnEndDrag(PointerEventData data)
	{
		if (_previewPiece != null)
			Destroy (_previewPiece);
		ConstantHandler.Instance.ComponentDragged = false;
		if (ConstantHandler.Instance.ComponentAdded) {
			IVector3 oldPos = GameHandler.Instance._grid.GetGoalPosition ();
			GameHandler.Instance._grid.SetTileToType (oldPos.x, oldPos.y, oldPos.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (oldPos.x + 1, oldPos.y, oldPos.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (oldPos.x - 1, oldPos.y, oldPos.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (oldPos.x, oldPos.y+1, oldPos.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (oldPos.x, oldPos.y-1, oldPos.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (oldPos.x + 1, oldPos.y+1, oldPos.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (oldPos.x + 1, oldPos.y-1, oldPos.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (oldPos.x - 1, oldPos.y+1, oldPos.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (oldPos.x - 1, oldPos.y-1, oldPos.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.AttachItem (oldPos.x, oldPos.y, oldPos.z);

			GameHandler.Instance._grid.AttachItem (oldPos.x-1, oldPos.y-1, oldPos.z);
			GameHandler.Instance._grid.AttachItem (oldPos.x+1, oldPos.y, oldPos.z);
			GameHandler.Instance._grid.AttachItem (oldPos.x-1, oldPos.y, oldPos.z);
			GameHandler.Instance._grid.AttachItem (oldPos.x, oldPos.y+1, oldPos.z);
			GameHandler.Instance._grid.AttachItem (oldPos.x, oldPos.y-1, oldPos.z);
			GameHandler.Instance._grid.AttachItem (oldPos.x+1, oldPos.y+1, oldPos.z);
			GameHandler.Instance._grid.AttachItem (oldPos.x+1, oldPos.y-1, oldPos.z);
			GameHandler.Instance._grid.AttachItem (oldPos.x-1, oldPos.y+1, oldPos.z);
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			if (GameHandler.Instance._grid.GetTileType (pos.x, pos.y, pos.z) == GridHandler.Item.EMPTY) {
				if (GameHandler.Instance._grid.GetTileType (pos.x + 1, pos.y, pos.z) == GridHandler.Item.EMPTY &&
				   GameHandler.Instance._grid.GetTileType (pos.x - 1, pos.y, pos.z) == GridHandler.Item.EMPTY &&
				   GameHandler.Instance._grid.GetTileType (pos.x, pos.y + 1, pos.z) == GridHandler.Item.EMPTY &&
				   GameHandler.Instance._grid.GetTileType (pos.x, pos.y - 1, pos.z) == GridHandler.Item.EMPTY &&
				   GameHandler.Instance._grid.GetTileType (pos.x + 1, pos.y - 1, pos.z) == GridHandler.Item.EMPTY &&
				   GameHandler.Instance._grid.GetTileType (pos.x + 1, pos.y + 1, pos.z) == GridHandler.Item.EMPTY &&
				   GameHandler.Instance._grid.GetTileType (pos.x - 1, pos.y + 1, pos.z) == GridHandler.Item.EMPTY &&
				   GameHandler.Instance._grid.GetTileType (pos.x - 1, pos.y - 1, pos.z) == GridHandler.Item.EMPTY) {
					GameHandler.Instance._grid.SetTileToType (pos.x, pos.y, pos.z, GridHandler.Item.GOAL);
					GameHandler.Instance._grid.SetTileToType (pos.x + 1, pos.y, pos.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (pos.x - 1, pos.y, pos.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (pos.x, pos.y+1, pos.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (pos.x, pos.y-1, pos.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (pos.x + 1, pos.y+1, pos.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (pos.x + 1, pos.y-1, pos.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (pos.x - 1, pos.y+1, pos.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetTileToType (pos.x - 1, pos.y-1, pos.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.AttachItem (pos.x, pos.y, pos.z);

					GameHandler.Instance._grid.AttachItem (pos.x-1, pos.y-1, pos.z);
					GameHandler.Instance._grid.AttachItem (pos.x+1, pos.y, pos.z);
					GameHandler.Instance._grid.AttachItem (pos.x-1, pos.y, pos.z);
					GameHandler.Instance._grid.AttachItem (pos.x, pos.y+1, pos.z);
					GameHandler.Instance._grid.AttachItem (pos.x, pos.y-1, pos.z);
					GameHandler.Instance._grid.AttachItem (pos.x+1, pos.y+1, pos.z);
					GameHandler.Instance._grid.AttachItem (pos.x+1, pos.y-1, pos.z);
					GameHandler.Instance._grid.AttachItem (pos.x-1, pos.y+1, pos.z);
				}
			}
			ConstantHandler.Instance.PositionAdded = IVector3.zero;
			ConstantHandler.Instance.ComponentAdded = false;
		}
		//Destroy (gameObject);
	}
}
