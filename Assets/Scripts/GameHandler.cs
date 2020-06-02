using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puzzlets;
using CustomTools;
using UnityEngine.UI;

//Handles main player interactions as well as designer interactions
public class GameHandler : MonoBehaviour {
	public static GameHandler Instance = null;
	private Vector3 _ballLocation;
	public GridHandler _grid;
	public GameObject _designerUI, _playerUI, _followBall, _changeCam;
	public InputField _field;
	public Button _switchToPlayer;
	public GameObject _gameBall;
	public enum GameState
	{
		PLAYER,
		DESIGNER
	}
	public GameState _gameState;

	void Awake()
	{
		//Singleton called at beginning of scene
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);

		_gameState = GameState.DESIGNER;
		_playerUI.transform.GetChild (0).gameObject.SetActive (false);
	}

	public void CreateGameBall(int X, int Y, int Z)
	{
		if (_gameBall != null)
			Destroy (_gameBall);
		_gameBall = Instantiate (PrefabHandler.Instance.Ball, new Vector3 ((float)X / 2+0.25f, 5, (float)Y / 2+0.15f), Quaternion.identity) as GameObject;
		_gameBall.AddComponent<UpdateBallPosition> ();
		Destroy (_gameBall.GetComponent<Rigidbody> ());
		_gameBall.GetComponent<UpdateBallPosition> ()._heightField = _field;
	}

	private void InstantiateBall()
	{
		Destroy (_gameBall);
		_gameBall = Instantiate (PrefabHandler.Instance.Ball, _ballLocation, Quaternion.identity) as GameObject;
		_gameBall.AddComponent<UpdateBallPosition> ();
		Destroy (_gameBall.GetComponent<Rigidbody> ());
		_gameBall.GetComponent<UpdateBallPosition> ()._heightField = _field;
	}

	public void ChangeToPlayer()
	{
		_ballLocation = _gameBall.transform.position;
		_playerUI.transform.GetChild (0).gameObject.SetActive (true);
		_gameState = GameState.PLAYER;
		_designerUI.SetActive (false);
		for (int i = 0; i < ConstantHandler.Instance.GridWidth; i++) {
			for (int j = 0; j < ConstantHandler.Instance.GridHeight; j++) {
				for (int k = 0; k < ConstantHandler.Instance.GridLength; k++) {
					GameObject _item = _grid.GetItemGO (i,j,k);
					if (_item != null) {
						if (_item.GetComponent<Rotate2x1Blocks> () != null)
							Destroy (_item.GetComponent<Rotate2x1Blocks> ());
						if (_item.GetComponent<Rotate2x2Blocks> () != null)
							Destroy (_item.GetComponent<Rotate2x2Blocks> ());
						if (_item.transform.childCount>0)
							if(_item.transform.GetChild (0).GetComponent<Rotate2x1Blocks> () != null)
								Destroy (_item.transform.GetChild (0).GetComponent<Rotate2x1Blocks> ());
						if (_item.transform.childCount>0)
							if(_item.transform.GetChild (0).GetComponent<Rotate2x2Blocks> () != null)
								Destroy (_item.transform.GetChild (0).GetComponent<Rotate2x2Blocks> ());
						if (_item.GetComponent<ApplyFrictionPad> () != null)
							Destroy (_item.GetComponent<ApplyFrictionPad> ());
						if (_item.transform.childCount>0)
							if(_item.transform.GetChild (0).GetComponent<ApplyFrictionPad> () != null)
								Destroy (_item.transform.GetChild (0).GetComponent<ApplyFrictionPad> ());
					}

				}
			}
		}
	}

	void Update()
	{
		if (_gameState == GameState.PLAYER) {
			if (Input.GetKey (KeyCode.R)) {
				Reset ();
			}
		}
		if (_gameBall == null)
			_switchToPlayer.interactable = false;
		else
			_switchToPlayer.interactable = true;
	}

	public void StartSimulation()
	{
		if(_gameBall.GetComponent<Rigidbody>()==null)
			_gameBall.AddComponent<Rigidbody> ();
		_gameBall.AddComponent<BallCollisionScript> ();
		for (int i = 0; i < ConstantHandler.Instance.GridWidth; i++) {
			for (int j = 0; j < ConstantHandler.Instance.GridHeight; j++) {
				for (int k = 0; k < ConstantHandler.Instance.GridLength; k++) {
					_grid.GetObj (i, j, k).GetComponent<Renderer> ().enabled = false;
					_grid.GetObj (i, j, k).GetComponent<SphereCollider> ().enabled = false;
				}
			}
		}
	}

	public void Reset()
	{
		InstantiateBall ();
		for (int i = 0; i < ConstantHandler.Instance.GridWidth; i++) {
			for (int j = 0; j < ConstantHandler.Instance.GridHeight; j++) {
				for (int k = 0; k < ConstantHandler.Instance.GridLength; k++) {
					if (_grid.GetTileType (i, j, k) == GridHandler.Item.EMPTY && k<1) {
						_grid.GetObj (i, j, k).GetComponent<Renderer> ().enabled = true;
						_grid.GetObj (i, j, k).GetComponent<SphereCollider> ().enabled = true;
					}
				}
			}
		}
	}
}
