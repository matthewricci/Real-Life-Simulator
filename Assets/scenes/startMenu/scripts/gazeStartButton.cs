using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gazeStartButton : MonoBehaviour {


	void startTheGame () {
		// Reset the Tasks Completed to zero.
		gameOverlordScript.numTasksCompleted = 0;
		// Load the matrix scene.
		SceneManager.LoadScene ("Matrix");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
