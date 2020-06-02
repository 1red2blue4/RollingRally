﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;
using CustomTools.ForwardEvents;
using UnityEngine.EventSystems;

public class RotateItemOnClickRamp : MonoBehaviour {
	private GridHandler.ItemDirection _direction;
	public GridHandler.ItemDirection Direction
	{
		set { _direction = value; }
		get { return _direction; }
	}

	private IVector2 _position;
	public IVector2 Position
	{
		set { _position = value; }
		get { return _position; }
	}
	private Vector3 originalPos;
	private GridHandler.FrictionValue _frictionVal;

	RaycastHit hitMovement;
	RaycastHit[] hits;
	Ray ray;
	bool _hitSphere;
	IVector2 newPos;

	void Start()
	{
		originalPos = transform.position;
	} 

	/*void OnMouseDrag()
	{
		Destroy (gameObject.GetComponent<Rigidbody> ());
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hitMovement)) {
			Vector3 pos = hitMovement.point;
			pos.y = originalPos.y+2;
			transform.position =pos;
		}
		GameHandler.Instance._grid.GetObj (_position.x, _position.y).GetComponent<Renderer> ().enabled = true;
		switch (_direction) {
		case GridHandler.ItemDirection.LEFT:
			GameHandler.Instance._grid.GetObj (_position.x - 1, _position.y).GetComponent<Renderer> ().enabled = true;
			break;
		case GridHandler.ItemDirection.UP:
			GameHandler.Instance._grid.GetObj (_position.x, _position.y + 1).GetComponent<Renderer> ().enabled = true;
			break;
		case GridHandler.ItemDirection.RIGHT:
			GameHandler.Instance._grid.GetObj (_position.x + 1, _position.y).GetComponent<Renderer> ().enabled = true;
			break;
		case GridHandler.ItemDirection.DOWN:
			GameHandler.Instance._grid.GetObj (_position.x, _position.y - 1).GetComponent<Renderer> ().enabled = true;
			break;
		}
	}*/
	/*void OnMouseUp()
	{
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		hits = Physics.RaycastAll (ray);
		foreach (RaycastHit hit in hits) {
			if (hit.transform.gameObject.tag == "Sphere") {
				_hitSphere = true;
				newPos = hit.transform.gameObject.GetComponent<UIAddItem> ().Position;
			}
		}
		_frictionVal = GameHandler.Instance._grid.GetTileFriction (_position.x, _position.y);
		if (_hitSphere) {
			GameHandler.Instance._grid.SetTileToType (newPos.x, newPos.y, GridHandler.Item.RAMP);
			GameHandler.Instance._grid.SetFrictionForTile (newPos.x, newPos.y, _frictionVal);
			GameHandler.Instance._grid.SetDirectionOfTile (newPos.x, newPos.y, _direction);
			GameHandler.Instance._grid.AttachItem (newPos.x, newPos.y);
			switch (_direction) {
			case GridHandler.ItemDirection.LEFT:
				GameHandler.Instance._grid.SetTileToType (newPos.x - 1, newPos.y, GridHandler.Item.RAMP_NOTHING);
				GameHandler.Instance._grid.AttachItem (newPos.x - 1, newPos.y);
				GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y);
				break;
			case GridHandler.ItemDirection.UP:
				GameHandler.Instance._grid.SetTileToType (newPos.x, newPos.y+1, GridHandler.Item.RAMP_NOTHING);
				GameHandler.Instance._grid.AttachItem (newPos.x, newPos.y+1);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y+1, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
				break;
			case GridHandler.ItemDirection.RIGHT:
				GameHandler.Instance._grid.SetTileToType (newPos.x+1, newPos.y, GridHandler.Item.RAMP_NOTHING);
				GameHandler.Instance._grid.AttachItem (newPos.x +1, newPos.y);
				GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y);
				break;
			case GridHandler.ItemDirection.DOWN:
				GameHandler.Instance._grid.SetTileToType (newPos.x, newPos.y-1, GridHandler.Item.RAMP_NOTHING);
				GameHandler.Instance._grid.AttachItem (newPos.x, newPos.y-1);
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y-1, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1);
				break;
			}
			GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
		} else {
			GameHandler.Instance._grid.SetTileToType (_position.x, _position.y, GridHandler.Item.EMPTY);
			GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
			switch (_direction) {
			case GridHandler.ItemDirection.LEFT:
				GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y);
				break;
			case GridHandler.ItemDirection.UP:
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y+1, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
				break;
			case GridHandler.ItemDirection.RIGHT:
				GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y);
				break;
			case GridHandler.ItemDirection.DOWN:
				GameHandler.Instance._grid.SetTileToType (_position.x, _position.y-1, GridHandler.Item.EMPTY);
				GameHandler.Instance._grid.AttachItem (_position.x, _position.y-1);
				break;
			}
		}
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp (1)) {
				switch (_direction) {
				case GridHandler.ItemDirection.LEFT:
					{
						if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y + 1) && GameHandler.Instance._grid.GetTileType (_position.x, _position.y + 1) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.UP;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y + 1);
						} else if (GameHandler.Instance._grid.isOnGrid (_position.x + 1, _position.y) && GameHandler.Instance._grid.GetTileType (_position.x + 1, _position.y) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.RIGHT;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y);
						} else if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y - 1) && GameHandler.Instance._grid.GetTileType (_position.x, _position.y - 1) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.DOWN;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1);
						}
						break;
					}
				case GridHandler.ItemDirection.UP:
					{
						if (GameHandler.Instance._grid.isOnGrid (_position.x + 1, _position.y) && GameHandler.Instance._grid.GetTileType (_position.x + 1, _position.y) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.RIGHT;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y + 1);
						} else if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y - 1) && GameHandler.Instance._grid.GetTileType (_position.x, _position.y - 1) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.DOWN;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y + 1);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1);
						} else if (GameHandler.Instance._grid.isOnGrid (_position.x - 1, _position.y) && GameHandler.Instance._grid.GetTileType (_position.x - 1, _position.y) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.LEFT;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y + 1);
						}
						break;
					}
				case GridHandler.ItemDirection.RIGHT:
					{
						if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y - 1) && GameHandler.Instance._grid.GetTileType (_position.x, _position.y - 1) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.DOWN;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1);
						} else if (GameHandler.Instance._grid.isOnGrid (_position.x - 1, _position.y) && GameHandler.Instance._grid.GetTileType (_position.x - 1, _position.y) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.LEFT;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y);
						} else if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y + 1) && GameHandler.Instance._grid.GetTileType (_position.x, _position.y + 1) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.UP;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y + 1);
						}
						break;
					}
				case GridHandler.ItemDirection.DOWN:
					{
						if (GameHandler.Instance._grid.isOnGrid (_position.x - 1, _position.y) && GameHandler.Instance._grid.GetTileType (_position.x - 1, _position.y) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.LEFT;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x - 1, _position.y, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x - 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1);
						} else if (GameHandler.Instance._grid.isOnGrid (_position.x, _position.y + 1) && GameHandler.Instance._grid.GetTileType (_position.x, _position.y + 1) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.UP;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y + 1, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y + 1);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1);
						} else if (GameHandler.Instance._grid.isOnGrid (_position.x + 1, _position.y) && GameHandler.Instance._grid.GetTileType (_position.x + 1, _position.y) == GridHandler.Item.EMPTY) {
							_direction = GridHandler.ItemDirection.RIGHT;
							GameHandler.Instance._grid.SetDirectionOfTile (_position.x, _position.y, _direction);
							GameHandler.Instance._grid.SetTileToType (_position.x, _position.y - 1, GridHandler.Item.EMPTY);
							GameHandler.Instance._grid.SetTileToType (_position.x + 1, _position.y, GridHandler.Item.RAMP_NOTHING);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x + 1, _position.y);
							GameHandler.Instance._grid.AttachItem (_position.x, _position.y - 1);
						}
						break;
					}
				}
		}
	}*/
}
