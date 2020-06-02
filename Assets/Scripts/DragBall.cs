using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CustomTools;

//Drag ball to different gridspot from UI
public class DragBall : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

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
			_previewPiece = Instantiate (PrefabHandler.Instance.Ball, new Vector3 ((float)pos.x / 2 + 0.25f, 5, (float)pos.y / 2 + 0.25f),
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
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			GameHandler.Instance.CreateGameBall (pos.x, pos.y, pos.z);
			ConstantHandler.Instance.PositionAdded = IVector3.zero;
			ConstantHandler.Instance.ComponentAdded = false;
		}
	}
}
