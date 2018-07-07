using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y <= 0.5f) {
			gameOverlordScript.lives = 0;
			gameOverlordScript.numTasksCompleted -= 1;
			gameOverlordScript.microgameDone = true;
		}
	}
}
