using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour {

	public float speed = 5f;
	public Vector3 destination; // the number will always swim towards its destination
	public bool correctNumber=false;
	public bool gameLost = false;
	public Rigidbody myRigidbody;

	void Start(){
		myRigidbody = this.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
	if (gameLost==true){
			myRigidbody.velocity = Vector3.up*5f;
		}else {
			// make the number always swim towards its destination
			Vector3 moveDir = destination - transform.position; // vector from A to B = B - A
			Debug.DrawLine (transform.position, destination, Color.yellow); // visualize path in Scene tab

			// normalize the vector so that the number doesn't teleport
			if (moveDir.magnitude > 1f) { // but only normalize if it's far from its destination!
				//moveDir = moveDir.normalized; 
			}
			// moveDir = Vector3.Normalize( moveDir ); // this does the same thing too

			// actually move the number now!
			myRigidbody.velocity = moveDir * speed * Time.deltaTime * 5f;

			// smoothly rotate the number
			myRigidbody.AddTorque(Vector3.Normalize(moveDir * speed));
		}
	}
}