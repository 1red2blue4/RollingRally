using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CustomTools;

public class DragUIBankShot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

	private GridHandler _grid;
	public GridHandler.Item _itemType;

	public void OnBeginDrag(PointerEventData data)
	{
		GameObject i = Instantiate (gameObject, gameObject.GetComponent<RectTransform> ().position, transform.rotation) as GameObject;
		i.transform.SetParent (PrefabHandler.Instance.Canvas.transform, false);
		i.GetComponent<RectTransform> ().position = gameObject.GetComponent<RectTransform> ().position;
	}

	void Start()
	{
		_grid = GameObject.Find ("Gridhandler").GetComponent<GridHandler> ();
	}

	public void OnDrag(PointerEventData data)
	{
		ConstantHandler.Instance.ComponentDragged = true;
		gameObject.GetComponent<RectTransform> ().position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
	}

	public void OnEndDrag(PointerEventData data)
	{
		ConstantHandler.Instance.ComponentDragged = false;
		if (ConstantHandler.Instance.ComponentAdded) {
			//IVector2 pos = ConstantHandler.Instance.PositionAdded;
			/*if (_grid.GetTileType (pos.x, pos.y) == GridHandler.Item.RAMP) {
				switch (_grid.GetDirectionOfTile (pos.x, pos.y)) {
				case GridHandler.ItemDirection.LEFT:
					_grid.SetTileToType (pos.x - 1, pos.y, GridHandler.Item.EMPTY);
					_grid.AttachItem (pos.x - 1, pos.y);
					break;
				case GridHandler.ItemDirection.UP:
					_grid.SetTileToType (pos.x, pos.y + 1, GridHandler.Item.EMPTY);
					_grid.AttachItem (pos.x, pos.y + 1);
					break;
				case GridHandler.ItemDirection.RIGHT:
					_grid.SetTileToType (pos.x + 1, pos.y, GridHandler.Item.EMPTY);
					_grid.AttachItem (pos.x + 1, pos.y);
					break;
				case GridHandler.ItemDirection.DOWN:
					_grid.SetTileToType (pos.x, pos.y - 1, GridHandler.Item.EMPTY);
					_grid.AttachItem (pos.x, pos.y - 1);
					break;
				}
			} else if (_grid.GetTileType (pos.x, pos.y) == GridHandler.Item.BANKSHOT_FRIC_MEDIUM || _grid.GetTileType (pos.x, pos.y)
			           == GridHandler.Item.BANKSHOT_FRIC_LOW || _grid.GetTileType (pos.x, pos.y) == GridHandler.Item.BANKSHOT_NOTHING) {
				Debug.Log ("Bankshot time");
				switch (_grid.GetDirectionOfTile (pos.x, pos.y)) {
				case GridHandler.ItemDirection.LEFT:
					_grid.SetTileToType (pos.x + 1, pos.y, GridHandler.Item.EMPTY);
					_grid.SetTileToType (pos.x, pos.y - 1, GridHandler.Item.EMPTY);
					_grid.SetTileToType (pos.x + 1, pos.y - 1, GridHandler.Item.EMPTY);
					_grid.AttachItem (pos.x + 1, pos.y - 1);
					_grid.AttachItem (pos.x + 1, pos.y);
					_grid.AttachItem (pos.x, pos.y - 1);
					break;
				case GridHandler.ItemDirection.UP:
					_grid.SetTileToType (pos.x - 1, pos.y, GridHandler.Item.EMPTY);
					_grid.SetTileToType (pos.x, pos.y - 1, GridHandler.Item.EMPTY);
					_grid.SetTileToType (pos.x - 1, pos.y - 1, GridHandler.Item.EMPTY);
					_grid.AttachItem (pos.x - 1, pos.y - 1);
					_grid.AttachItem (pos.x - 1, pos.y);
					_grid.AttachItem (pos.x, pos.y - 1);
					break;
				case GridHandler.ItemDirection.RIGHT:
					_grid.SetTileToType (pos.x - 1, pos.y, GridHandler.Item.EMPTY);
					_grid.SetTileToType (pos.x, pos.y + 1, GridHandler.Item.EMPTY);
					_grid.SetTileToType (pos.x - 1, pos.y + 1, GridHandler.Item.EMPTY);
					_grid.AttachItem (pos.x - 1, pos.y + 1);
					_grid.AttachItem (pos.x - 1, pos.y);
					_grid.AttachItem (pos.x, pos.y + 1);
					break;
				case GridHandler.ItemDirection.DOWN:
					_grid.SetTileToType (pos.x + 1, pos.y, GridHandler.Item.EMPTY);
					_grid.SetTileToType (pos.x, pos.y + 1, GridHandler.Item.EMPTY);
					_grid.SetTileToType (pos.x + 1, pos.y + 1, GridHandler.Item.EMPTY);
					_grid.AttachItem (pos.x + 1, pos.y + 1);
					_grid.AttachItem (pos.x + 1, pos.y);
					_grid.AttachItem (pos.x, pos.y + 1);
					break;
				}
			}
				if (_grid.isOnGrid (pos.x + 1, pos.y) && _grid.isOnGrid (pos.x, pos.y - 1) && _grid.GetTileType (pos.x + 1, pos.y) == GridHandler.Item.EMPTY && _grid.GetTileType (pos.x, pos.y - 1) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x + 1, pos.y, GridHandler.Item.BANKSHOT_NOTHING);
					_grid.SetTileToType (pos.x, pos.y - 1, GridHandler.Item.BANKSHOT_NOTHING);
					_grid.SetDirectionOfTile (pos.x, pos.y, GridHandler.ItemDirection.LEFT);
					_grid.AttachItem (pos.x + 1, pos.y);
					_grid.AttachItem (pos.x, pos.y - 1);
				} else if (_grid.isOnGrid (pos.x, pos.y - 1) && _grid.isOnGrid (pos.x - 1, pos.y) && _grid.GetTileType (pos.x - 1, pos.y) == GridHandler.Item.EMPTY && _grid.GetTileType (pos.x, pos.y - 1) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x, pos.y - 1, GridHandler.Item.BANKSHOT_NOTHING);
					_grid.SetTileToType (pos.x - 1, pos.y, GridHandler.Item.BANKSHOT_NOTHING);
					_grid.SetDirectionOfTile (pos.x, pos.y, GridHandler.ItemDirection.UP);
					_grid.AttachItem (pos.x, pos.y - 1);
					_grid.AttachItem (pos.x - 1, pos.y);
				} else if (_grid.isOnGrid (pos.x - 1, pos.y) && _grid.isOnGrid (pos.x, pos.y + 1) && _grid.GetTileType (pos.x - 1, pos.y) == GridHandler.Item.EMPTY && _grid.GetTileType (pos.x, pos.y + 1) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x - 1, pos.y, GridHandler.Item.BANKSHOT_NOTHING);
					_grid.SetTileToType (pos.x, pos.y + 1, GridHandler.Item.BANKSHOT_NOTHING);
					_grid.SetDirectionOfTile (pos.x, pos.y, GridHandler.ItemDirection.RIGHT);
					_grid.AttachItem (pos.x - 1, pos.y);
					_grid.AttachItem (pos.x, pos.y + 1);
				} else if (_grid.isOnGrid (pos.x, pos.y + 1) && _grid.isOnGrid (pos.x + 1, pos.y) && _grid.GetTileType (pos.x + 1, pos.y) == GridHandler.Item.EMPTY && _grid.GetTileType (pos.x, pos.y + 1) == GridHandler.Item.EMPTY) {
					_grid.SetTileToType (pos.x, pos.y + 1, GridHandler.Item.BANKSHOT_NOTHING);
					_grid.SetTileToType (pos.x + 1, pos.y, GridHandler.Item.BANKSHOT_NOTHING);
					_grid.SetDirectionOfTile (pos.x, pos.y, GridHandler.ItemDirection.DOWN);
					_grid.AttachItem (pos.x + 1, pos.y);
					_grid.AttachItem (pos.x, pos.y + 1);
				}*/
			/*_grid.SetTileToType (pos.x, pos.y, _itemType);
			_grid.AttachItem (pos.x, pos.y);
			ConstantHandler.Instance.PositionAdded = IVector2.zero;
			ConstantHandler.Instance.ComponentAdded = false;*/
		}
		Destroy (gameObject);
	}
}
