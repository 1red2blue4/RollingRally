using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Follow cam, angled cams as well as normal cam
public class CameraController : MonoBehaviour {

	private List<CameraDetails> _cameraDetails;
	public Camera _mainCamera;
	public Camera _shotCamera;
	private int _indexVal = -1;
	private bool _followCam;
	private CameraDetails _overheadView;
	// Use this for initialization
	void Start () {
		_overheadView = new CameraDetails (new Vector3 (2.84f, 10.1f, 1.96f), new Vector3 (90, 0, 0));
		_cameraDetails = new List<CameraDetails> ();
		_cameraDetails.Add(new CameraDetails(new Vector3(-0.82f,7.15f,-2.4f), new Vector3(45,45,0)));
		_cameraDetails.Add(new CameraDetails(new Vector3(-1.8F,5.36F,8.06F), new Vector3(45,135,0)));
		_cameraDetails.Add (new CameraDetails (new Vector3 (8.67f, 5.69f, 8.06f), new Vector3 (135, 45, 180)));
		_cameraDetails.Add (new CameraDetails (new Vector3 (8.22f, 5.91f, -1.98f), new Vector3 (45, -45, 0)));
		_mainCamera.gameObject.SetActive (true);
		_shotCamera.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FollowCam()
	{
		_followCam = !_followCam;
		if (!_followCam) {
			Destroy (_mainCamera.GetComponent<UnityStandardAssets.Utility.SmoothFollow> ());
			_mainCamera.transform.position = _overheadView._position;
			_mainCamera.transform.eulerAngles = _overheadView._eulerAngles;
		} else {
			if (GameHandler.Instance._gameBall != null) {
				_mainCamera.gameObject.AddComponent<UnityStandardAssets.Utility.SmoothFollow> ();
				_mainCamera.GetComponent<UnityStandardAssets.Utility.SmoothFollow> ().target = GameHandler.Instance._gameBall.transform;
			}
			else
				Debug.Log ("No Ball here");
		}
	}

	public struct CameraDetails
	{
		public Vector3 _position;
		public Vector3 _eulerAngles;
		public CameraDetails(Vector3 pos, Vector3 rot)
		{
			_position=pos;
			_eulerAngles=rot;
		}
	}

	public void MoveCameraPosition()
	{
		_indexVal++;
		if (_indexVal > 3)
			_indexVal = -1;
		if (_indexVal < 0) {
			_mainCamera.gameObject.SetActive (true);
			_shotCamera.gameObject.SetActive (false);
		} else {
			_mainCamera.gameObject.SetActive (false);
			_shotCamera.gameObject.SetActive (true);
			_shotCamera.transform.position = _cameraDetails [_indexVal]._position;
			_shotCamera.transform.eulerAngles = _cameraDetails [_indexVal]._eulerAngles;
		}
	}
}
