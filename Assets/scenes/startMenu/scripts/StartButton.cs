using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Valve.VR.InteractionSystem;

public class StartButton : MonoBehaviour {
	public float xPos;
	public float yPos;
	public Rigidbody myRigidBody;

	void Start(){
		xPos = this.gameObject.transform.position.x;
		yPos = this.gameObject.transform.position.y;
		myRigidBody = this.gameObject.GetComponent<Rigidbody> ();
	}

	void Update(){
		if (this.gameObject.transform.position.z >= 2.1f) {

			// Reset the Tasks Completed to zero.
			gameOverlordScript.numTasksCompleted = 0;

			SceneManager.LoadScene ("Matrix");
		}

		//this.gameObject.transform.position = new Vector3 (xPos, yPos, (this.gameObject.transform.position.z-0.1f));
		if (this.gameObject.transform.position.z <= 1.805f) {
			myRigidBody.AddForce (0f, 0f, 0.5f);
		}
	}
}