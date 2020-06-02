using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Useless for now
public class RemovePhysicsOnReachingGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Plane") {
			//Destroy (gameObject.GetComponent<Rigidbody> ());
		}
	}
}
