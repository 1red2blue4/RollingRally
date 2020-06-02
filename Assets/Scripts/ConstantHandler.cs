using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

//Singleton to handle constant values being used in game
public class ConstantHandler : Singleton<ConstantHandler> {
	private bool _componentAdded;
	public bool ComponentAdded
	{
		set { _componentAdded = value; }
		get { return _componentAdded; }
	}
	private IVector3 _positionAdded;
	public IVector3 PositionAdded
	{
		set { _positionAdded = value; }
		get { return _positionAdded; }
	}
	private bool _isUIdragged;
	public bool ComponentDragged
	{
		set { _isUIdragged = value; }
		get { return _isUIdragged; }
	}
	[SerializeField]
	private int _gridHeight=15;
	public int GridHeight
	{
		get { return _gridHeight; }
	}
	[SerializeField]
	private int _gridWidth=15;
	public int GridWidth
	{
		get { return _gridWidth; }
	}
	[SerializeField]
	private int _gridLength=10;
	public int GridLength
	{
		get { return _gridLength; }
	}
}
