using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

//Drag friction pad and apply on pos
public class ApplyFrictionPad : MonoBehaviour {
	private IVector3 _position;
	public IVector3 Position
	{
		set { _position = value; }
		get { return _position; }
	}

	void OnMouseOver()
	{
		if (ConstantHandler.Instance.ComponentDragged) {
			ConstantHandler.Instance.ComponentAdded = true;
			ConstantHandler.Instance.PositionAdded = _position;
		}
	}

	void OnMouseExit()
	{
		ConstantHandler.Instance.ComponentDragged = false;
		ConstantHandler.Instance.ComponentAdded = false;
		ConstantHandler.Instance.PositionAdded = IVector3.zero;
	}
}
