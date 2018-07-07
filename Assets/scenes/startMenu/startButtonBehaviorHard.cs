using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButtonBehaviorHard : MonoBehaviour {

	public int myDifMod;

	// Starts the game.
	public void startTheGameHard () {
		// Reset the Tasks Completed to zero.
		gameOverlordScript.numTasksCompleted = 0;
		// Set Difficulty
		gameOverlordScript.difficultyModifier=myDifMod;
		// Load the transition scene.
		SceneManager.LoadScene ("Matrix");
	}


}
