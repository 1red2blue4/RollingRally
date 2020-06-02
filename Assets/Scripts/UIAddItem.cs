using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

//Empty grid spaces have this item to become red to give feedback
public class UIAddItem : MonoBehaviour {
	private Color _ogColor;
	private IVector3 _position;
	public IVector3 Position
	{
		set { _position = value; }
		get { return _position; }
	}
	private bool _goToOGcolor;

	void Start()
	{
		_ogColor = GetComponent<Renderer> ().material.color;
		_goToOGcolor = true;
	}

	void Update()
	{
		if (_goToOGcolor)
				GetComponent<Renderer> ().material.color = _ogColor;
	}

	void OnMouseEnter()
	{
		if (ConstantHandler.Instance.ComponentDragged) {
			_goToOGcolor = false;
		}
	}

	void OnMouseOver()
	{
		if (ConstantHandler.Instance.ComponentDragged) {
			GetComponent<Renderer> ().material.color = Color.red;
			ConstantHandler.Instance.ComponentAdded = true;
			ConstantHandler.Instance.PositionAdded = _position;
		}
	}

	void OnMouseExit()
	{
		_goToOGcolor = true;
		ConstantHandler.Instance.ComponentDragged = false;
		ConstantHandler.Instance.ComponentAdded = false;
		ConstantHandler.Instance.PositionAdded = IVector3.zero;
	}
}
