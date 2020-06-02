using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CustomTools;

//Drag friction pad from UI to piece at gridspot
public class DragUIFriction : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler {
	
	public GridHandler.FrictionValue _frictionValue;

	public void OnBeginDrag(PointerEventData data)
	{
		GameObject i = Instantiate (gameObject, gameObject.GetComponent<RectTransform> ().position, transform.rotation) as GameObject;
		i.transform.SetParent (GameHandler.Instance._playerUI.transform, false);
		i.GetComponent<RectTransform> ().position = gameObject.GetComponent<RectTransform> ().position;
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
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			GameHandler.Instance._grid.SetFrictionForTile (pos.x, pos.y,pos.z, _frictionValue);
			GameHandler.Instance._grid.AttachItem (pos.x, pos.y,pos.z);
			ConstantHandler.Instance.PositionAdded = IVector3.zero;
			ConstantHandler.Instance.ComponentAdded = false;
		}
		Destroy (gameObject);
	}
}
