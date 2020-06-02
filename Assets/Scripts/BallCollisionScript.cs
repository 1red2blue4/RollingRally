using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reflect Physics for ball
public class BallCollisionScript : MonoBehaviour {
	Vector3 oldVel;
	float _colTime=0;

	void Update()
	{
		oldVel = GetComponent<Rigidbody> ().velocity;
	}

	private IEnumerator LossForPlayer()
	{
		yield return new WaitForSeconds (0.2f);
		GameHandler.Instance.Reset ();
	}

	void OnCollisionStay(Collision other)
	{
		if (other.gameObject.tag == "Goal") {
			_colTime += Time.deltaTime;
			if (_colTime > 2.0f)
				Debug.Log ("You win");
		}
	}
		
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Fail") {
			StartCoroutine (LossForPlayer ());
		}
		if (other.gameObject.tag == "BankShot") {
			ContactPoint cp = other.contacts [0];
			GetComponent<Rigidbody> ().velocity = Vector3.Reflect (oldVel, cp.normal);
		}
	}
}
