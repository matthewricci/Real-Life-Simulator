using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tickler : MonoBehaviour {

	public Rigidbody thisRigidbody;

	// Use this for initialization
	void Start () {
		thisRigidbody = this.GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter () {
		thisRigidbody.useGravity = true;
	}
}
