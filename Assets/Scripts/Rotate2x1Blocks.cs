using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

//Right click to delete the 2x1 blocks and D on hover to delete
public class Rotate2x1Blocks : MonoBehaviour {
	private GridHandler.ItemDirection _direction;
	public GridHandler.ItemDirection Direction
	{
		set { _direction = value; }
		get { return _direction; }
	}

	private IVector3 _position;
	public IVector3 Position
	{
		set { _position = value; }
		get { return _position; }
	}
	private GridHandler.Item _type;
	public GridHandler.Item Type
	{
		set { _type = value; }
		get { return _type; }
	}
	private Vector3 originalPos;
	RaycastHit hitMovement;
	RaycastHit[] hits;
	Ray ray;
	bool _hitSphere;
	IVector2 newPos;

	void Start () {
		originalPos = transform.position;
	}

	void OnMouseOver()
	{
		if (Input.GetKey(KeyCode.D)) {
			switch (_direction) {
			case GridHandler.ItemDirection.LEFT:
				GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, _position.z, GridHandler.Item.EMPTY);
				//GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z + 1, GridHandler.Item.EMPTY);
				//GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, _position.z + 1, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _position.z, GridHandler.ItemDirection.LEFT);
				GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
				GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y, _position.z);
				//GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y, _position.z + 1);
				//GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z + 1);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
				break;
			case GridHandler.ItemDirection.DOWN:
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, _position.z, GridHandler.Item.EMPTY);
				//GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z + 1, GridHandler.Item.EMPTY);
				//GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, _position.z + 1, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _position.z, GridHandler.ItemDirection.LEFT);
				GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1, _position.z);
				//GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1, _position.z + 1);
				//GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z + 1);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
				break;
			case GridHandler.ItemDirection.RIGHT:
				GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, _position.z, GridHandler.Item.EMPTY);
				//GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z + 1, GridHandler.Item.EMPTY);
				//GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, _position.z + 1, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _position.z, GridHandler.ItemDirection.LEFT);
				GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
				GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y, _position.z);
				//GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y, _position.z + 1);
				//GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z + 1);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
				break;
			case GridHandler.ItemDirection.UP:
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, _position.z, GridHandler.Item.EMPTY);
				//GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z + 1, GridHandler.Item.EMPTY);
				//GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, _position.z + 1, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, _position.z, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _position.z, GridHandler.ItemDirection.LEFT);
				GameHandler.Instance._grid.SetFrictionForTile (_position.x, _position.y, _position.z, GridHandler.FrictionValue.LOW);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y + 1, _position.z);
				//GameHandler.Instance._grid.AttachItem (_position.x, _position.y + 1, _position.z + 1);
				//GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z + 1);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y, _position.z);
				break;
			}
		}
		if (Input.GetMouseButtonUp (1)) {
			switch (_direction) {
			case GridHandler.ItemDirection.UP:
				if (GameHandler.Instance._grid.isOnGrid (_position.x-1, _position.y,_position.z) && 
					GameHandler.Instance._grid.GetTileType (_position.x-1, _position.y,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y+1,_position.z, GridHandler.Item.EMPTY);						
					GameHandler.Instance._grid.SetTileToType (_position.x-1, _position.y,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.LEFT);
					GameHandler.Instance._grid.AttachItem (_position.x -1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y+1,_position.z);
				} else if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y-1,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x, _position.y-1,_position.z) == GridHandler.Item.EMPTY) {
					GameHandler.Instance._grid.SetTileToType (_position.x , _position.y+1,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y-1,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.DOWN);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y+1,_position.z);
				} else if (GameHandler.Instance._grid.isOnGrid (_position.x+1, _position.y,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x+1, _position.y,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y+1,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x+1, _position.y,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.RIGHT);
					GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y+1,_position.z);
				}
				break;
			case GridHandler.ItemDirection.LEFT:
				if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y-1,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x, _position.y-1,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y-1 ,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z,GridHandler.ItemDirection.DOWN);
					GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1 ,_position.z);
				} else if (GameHandler.Instance._grid.isOnGrid (_position.x + 1, _position.y,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x + 1, _position.y,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z,GridHandler.ItemDirection.RIGHT);
					GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y,_position.z);
				} else if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y+1,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x, _position.y +1,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y+1,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.UP);
					GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y+1,_position.z);
				}
				break;
			case GridHandler.ItemDirection.DOWN:
				if (GameHandler.Instance._grid.isOnGrid (_position.x+1, _position.y,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x+1, _position.y,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y-1,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x+1, _position.y,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.RIGHT);
					GameHandler.Instance._grid.AttachItem (_position.x +1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1,_position.z);
				} else if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y+1,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x, _position.y+1,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x , _position.y-1,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y+1,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.UP);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y+1,_position.z);
				} else if (GameHandler.Instance._grid.isOnGrid (_position.x-1, _position.y,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x-1, _position.y,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y-1,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x-1, _position.y,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.LEFT);
					GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1,_position.z);
				}
				break;
			case GridHandler.ItemDirection.RIGHT:
				if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y+1,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x, _position.y+1,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y+1 ,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.UP);
					GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y+1,_position.z);
				} else if (GameHandler.Instance._grid.isOnGrid (_position.x - 1, _position.y,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x - 1, _position.y,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.LEFT);
					GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y,_position.z);
				} else if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y-1,_position.z) &&
					GameHandler.Instance._grid.GetTileType (_position.x, _position.y-1,_position.z) == GridHandler.Item.EMPTY){
					GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y,_position.z, GridHandler.Item.EMPTY);
					GameHandler.Instance._grid.SetTileToType (_position.x, _position.y-1,_position.z, GridHandler.Item.DISAPPEAR);
					GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y,_position.z, GridHandler.ItemDirection.DOWN);
					GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y,_position.z);
					GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1,_position.z);
				}
				break;
			}
		}
	}
}
