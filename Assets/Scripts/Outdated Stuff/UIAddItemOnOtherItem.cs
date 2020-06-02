using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

public class UIAddItemOnOtherItem : MonoBehaviour {
	private Color[] _ogColor;
	private IVector2 _position;
	public IVector2 Position
	{
		set { _position = value; }
		get { return _position; }
	}
	private bool _goToOGcolor;

	void Start()
	{
		_goToOGcolor = true;
		_ogColor = new Color[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild (i).GetComponent<Renderer> () != null) {
				_ogColor [i] = transform.GetChild (i).GetComponent<Renderer> ().material.color;
			} else {
				_ogColor [i] = transform.GetChild (i).GetChild (0).GetComponent<Renderer> ().material.color;
			}
		}
	}

	void Update()
	{
		if (_goToOGcolor) {
			for (int i = 0; i < transform.childCount; i++) {
				if (transform.GetChild (i).GetComponent<Renderer> () != null) {
					transform.GetChild (i).GetComponent<Renderer> ().material.color = _ogColor [i];
				} else {
					transform.GetChild (i).GetChild (0).GetComponent<Renderer> ().material.color = _ogColor [i];
				}
			}
		}
	}

	void OnMouseEnter()
	{
		if (ConstantHandler.Instance.ComponentDragged)
			_goToOGcolor = false;
	}

	void OnMouseOver()
	{
		if (ConstantHandler.Instance.ComponentDragged) {
			for (int i = 0; i < transform.childCount; i++) {
				if (transform.GetChild (i).GetComponent<Renderer> () != null) {
					transform.GetChild (i).GetComponent<Renderer> ().material.color = Color.red;
				} else {
					transform.GetChild (i).GetChild (0).GetComponent<Renderer> ().material.color = Color.red;
				}
				ConstantHandler.Instance.ComponentAdded = true;
				//ConstantHandler.Instance.PositionAdded = _position;
			}
		}
	}

	void OnMouseExit()
	{
		_goToOGcolor = true;
		ConstantHandler.Instance.ComponentDragged = false;
		ConstantHandler.Instance.ComponentAdded = false;
		//ConstantHandler.Instance.PositionAdded = IVector2.zero;
	}
}
