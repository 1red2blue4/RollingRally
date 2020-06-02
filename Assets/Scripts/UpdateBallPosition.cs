using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Update ball height based on input field text
public class UpdateBallPosition : MonoBehaviour {

	public InputField _heightField;
	private Vector3 _ballPosition;
	void Start()
	{
		_ballPosition = transform.position;
		var se = new InputField.SubmitEvent ();
		se.AddListener (GetHeight);
		_heightField.onEndEdit = se;
	}

	private void GetHeight(string text)
	{
		transform.position = new Vector3 (_ballPosition.x, float.Parse(text), _ballPosition.z);
	}
}
