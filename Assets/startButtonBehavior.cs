using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButtonBehavior : MonoBehaviour {

	// Starts the game.
	public void startTheGame () {
		// Reset the Tasks Completed to zero.
		gameOverlordScript.numTasksCompleted = 0;
		// Load the transition scene.
		SceneManager.LoadScene ("Matrix");
	}


}
