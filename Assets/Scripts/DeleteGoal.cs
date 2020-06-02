using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

//Delete goal on pressing D on top of goal
public class DeleteGoal : MonoBehaviour {
	private IVector3 _position;
	public IVector3 Position
	{
		set { _position = value; }
		get { return _position; }
	}

	void OnMouseOver()
	{
		if (Input.GetKey (KeyCode.D)) {
			Debug.Log ("Hello");
			GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, _position.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, _position.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, _position.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, _position.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y + 1, _position.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y - 1, _position.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y + 1, _position.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y - 1, _position.z, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y, _position.z);
			GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y, _position.z);
			GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y+1, _position.z);
			GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y-1, _position.z);
			GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y+1, _position.z);
			GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y-1, _position.z);
			GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1, _position.z);
			GameHandler.Instance._grid.AttachItem (_position.x, _position.y+1, _position.z);
			GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
		}
	}
}
