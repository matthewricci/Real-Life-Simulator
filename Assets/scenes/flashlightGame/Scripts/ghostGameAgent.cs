using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostGameAgent : MonoBehaviour {

	Renderer frontWallRend, leftWallRend, backWallRend, rightWallRend;

	//Renderer[] wallsRend = frontWallRend, leftWallRend, backWallRend, rightWallRend;

//	Renderer paintingFrontOneRend, paintingFrontTwoRend, paintingFrontThreeRend, paintingFrontFourRend, paintingFrontFiveRend, paintingFrontSixRend;
//
//	public Material paintingFrontOneHappy, paintingFrontTwoHappy, paintingFrontThreeHappy, paintingFrontFourHappy, paintingFrontFiveHappy;
//
//	public GameObject paintingFrontOneGO, paintingFrontTwoGO, paintingFrontThreeGO, paintingFrontFourGO, paintingFrontFiveGO, paintingFrontSixGO;

	public Material happyWallMaterial;

//	public Material happyFaceMaterial;

	public GameObject frontWall, leftWall, backWall, rightWall;

	public GameObject[] flashlights;

	// This is the number of ghost you need to zap. This is determined by the difficulty level.
	// Level One: 1 Static Ghost
	// Level Two: 1 Dynamic Ghost
	// Level Three: Two Dynamic Ghosts.
	public int ghostsToZap = 1;

	// Timer for this particular scene:
	float microgameTimer = 20;

	// This bool determines whether the player has won or not. 
	public bool gameWon = false;

	// This variable represents the total number of time it takes for the scene to pass:
	float totalTimeForWatch;

	// This string contains a word or phrase which says whether the game is won, lost, or in progress.
	string gameResult = "In Progress";

	// This bool measures whether the game is over or not.
	bool microgameOver = false;

	// These variables hold the various lights in the scene, which are turned on or off at the win condition.
	public GameObject flashlightLight;
	public GameObject ceilingLight;
	public GameObject areaLight;

	// ghost game SFX player.
	public AudioSource ghostGameSFXPlayer;
	// Victory SFX
	public AudioClip ghostVictorySFX;
	// Defeat SFX
	public AudioClip ghostDefeatSFX;

	// Has the VictoryCondition been activaed.
	public bool victoryConditionPlayed = false;
	// Has the defeat condition been played.
	public bool defeatConditionPlayed = false;

	// Prefab for the still ghost.
	public GameObject stillGhostPrefab;
	// Prefab for the ghosts that move around.
	public GameObject dynamicGhostPrefab;

	// The current ghost that is still.
	GameObject thisStillGhost;
	// The current ghost that moves around.
	GameObject thisDynamicGhost;

	public Color redLightColor;

	// This is the position where the player Starts.
	public Transform playerStartingPosition;

	// The Ghost has spawned in an inappropriate area (too close to the player). Spawn again.
	bool needToSpawnAgain = true;

	// Determines if there is a flashlight on the ghost. If so, the game cannot end yet.
	public bool flashlightOnGhost = false;

	// The time since the player achieved the win condition.
	float timeSinceMicrogameOver = 0;

	public static bool switchToHappy = false;


	void Awake () {

		switchToHappy = false;
		// This variable represents the total number of time it takes for the scene to pass:
		totalTimeForWatch = microgameTimer;
		// We change the public static variable for wristwatchTime to the duration fo this scene.
		gameOverlordScript.wristWatchTime = totalTimeForWatch;
	}

	// Use this for initialization
	void Start () {
		// Set up the flashlightGame at the given difficulty.
		startGhostGame (gameOverlordScript.difficulty);



		// Get all renderers.
		rightWallRend = rightWall.GetComponent<Renderer> ();
		leftWallRend = leftWall.GetComponent<Renderer> ();
		frontWallRend = frontWall.GetComponent<Renderer> ();
		backWallRend = backWall.GetComponent<Renderer> ();

		// Turn renderers on.
		rightWallRend.enabled = true;
		leftWallRend.enabled = true;
		frontWallRend.enabled = true;
		backWallRend.enabled = true;

		// Get all renderers.
//		paintingFrontOneRend = paintingFrontOneGO.GetComponent<Renderer> ();
//		paintingFrontTwoRend = paintingFrontTwoGO.GetComponent<Renderer> ();
//		paintingFrontThreeRend = paintingFrontThreeGO.GetComponent<Renderer> ();
//		paintingFrontFourRend = paintingFrontFourGO.GetComponent<Renderer> ();
//		paintingFrontFiveRend = paintingFrontFiveGO.GetComponent<Renderer> ();
//		paintingFrontSixRend = paintingFrontSixGO.GetComponent<Renderer> ();
//
//		paintingFrontOneRend.enabled = true; 
//		paintingFrontTwoRend.enabled = true; 
//		paintingFrontThreeRend.enabled = true; 
//		paintingFrontFourRend.enabled = true; 
//		paintingFrontFiveRend.enabled = true; 
//		paintingFrontSixRend.enabled = true; 


	




		// Turn renderers on.
		rightWallRend.enabled = true;
		leftWallRend.enabled = true;
		frontWallRend.enabled = true;
		backWallRend.enabled = true;



	}

	void victoryCondition () {
		
	}

	// This game sets up the game intially.
	void startGhostGame (int difficulty) {
		
		// For the first level, spawn one static ghost.
		if (difficulty == 1 && needToSpawnAgain == true) {
			Debug.Log ("The ghost is being spawned.");
			// Spawn a still ghost somewhere within a sphere around the player.
			thisStillGhost = Instantiate(stillGhostPrefab, (playerStartingPosition.position + (Random.insideUnitSphere * 4f)), Quaternion.identity);
			// If the ghost spawns in an area to close to the player, they will collide with an object with the tag "Don't Spawn Here"
			// And Spawn again.

			thisStillGhost.transform.LookAt (playerStartingPosition);

			// Probably obsolete:
//			while (thisGhost.GetComponent<ghostCode> ().spawnAgain == true) {
//				thisGhost = Instantiate(ghostPrefab, Random.insideUnitSphere, Quaternion.identity);
//			}

		// TO DO:
		// If dynamic ghost is spawned in the "dont spawn here" area, respawn it.
		// For the second level, spawn one dynamic ghost.
		} else if (difficulty == 2) {
			Debug.Log ("The dynamic ghost is being spawned.");
			// Spawn a dynamic ghost.
			thisDynamicGhost = Instantiate(dynamicGhostPrefab, (playerStartingPosition.position + (Random.insideUnitSphere * 4f)), Quaternion.identity);
			// Set ghosts to zap to one.
			ghostsToZap = 1;
			thisDynamicGhost.transform.LookAt (playerStartingPosition);


		// If the difficulty is three or above,
		} else if (difficulty >= 3) {
			
			Debug.Log ("The first dynamic ghost is being spawned.");
			// Spawn a ghost.
			thisDynamicGhost = Instantiate(dynamicGhostPrefab, (playerStartingPosition.position + (Random.insideUnitSphere * 4f)), Quaternion.identity);
			thisDynamicGhost.transform.LookAt (playerStartingPosition);
			Debug.Log ("The second dynamic ghost is being spawned.");
			// Spawn a second ghost.
			thisDynamicGhost = Instantiate(dynamicGhostPrefab, (playerStartingPosition.position + (Random.insideUnitSphere * 4f)), Quaternion.identity);
			thisDynamicGhost.transform.LookAt (playerStartingPosition);
			// Set ghosts to zap to two.
			ghostsToZap = 2;
		}
	}


	// Update is called once per frame
	void Update () {

		//Debug.Log (timeSinceMicrogameOver);

		// If the difficulty is set to level one.
		if (gameOverlordScript.difficulty == 1) {
			// If the ghost tells you it needs to spawn again.
			if (thisStillGhost.GetComponent<ghostCode> ().spawnAgain == true) {
				Debug.Log ("The ghost is being spawned again.");
				// Destroy that ghost.
				Destroy (thisStillGhost);
				// Run the sequence over again and spawn a new ghost.
				startGhostGame (1);
				// If less than five seconds have gone by since the start of the game,
			} else if (microgameTimer <= (microgameTimer - .5f)) {
				// Set ghosts to zap to one.
				ghostsToZap = 1;
			}
		}

		// If you won the game,
		if (gameWon) {
			// Start tracking how much time has gone by since the game ended,
			timeSinceMicrogameOver += Time.deltaTime;
		}

		// Countdown the microgame timer.
		microgameTimer -= Time.deltaTime;
		// Ask ghost game internal the result of the current game.
		gameResult = ghostGameInternal ();

		// Take that result and act accordingly in the check game progress function:
		// The checkGameProgress function will do the level acounting and end the game if 
		// flashLightGameInternal returns a "Win" or "Lose" condition.
		checkGameProgress (gameResult);

	}

	// This function checks if the win or lose condition for this game has been met.
	string ghostGameInternal () {

		// If there are no more ghosts left to zap and the game has not already been won,
		if (ghostsToZap == 0 && !gameWon) {
			// Set gameWon to true,
			gameWon = true;
			switchToHappy = true;
		}



		// VICTORY CONDITION:
		// If the game has been won, no victory condition has been played, and there are more than .5 seconds before the end of the game,
		if (gameWon && victoryConditionPlayed == false && timeSinceMicrogameOver >= .01f) {
			// TO DO: 
			// Add Victory Condition Actions.
			switchLights ();
//			// Turn off the flashlight and ceiling light:


//			// For all walls in the wall rend array,
//			for (int i = 0; i < wallsRend.Length; i++) {
//				Debug.Log ("In for Loop");
//				// Change their material to happy wall texture.
//				wallsRend [i].sharedMaterial = happyWallMaterial;
//			}

//			rightWallRend.sharedMaterial = happyWallMaterial;
//			leftWallRend.sharedMaterial = happyWallMaterial;
//			frontWallRend.sharedMaterial = happyWallMaterial;
//			backWallRend.sharedMaterial = happyWallMaterial;

//			paintingFrontOneRend.sharedMaterial = happyFaceMaterial;
//			paintingFrontTwoRend.sharedMaterial = happyFaceMaterial;
//			paintingFrontThreeRend.sharedMaterial = happyFaceMaterial;
//			paintingFrontFourRend.sharedMaterial = happyFaceMaterial;
//			paintingFrontFiveRend.sharedMaterial = happyFaceMaterial;
//			paintingFrontSixRend.sharedMaterial = happyFaceMaterial;


			// Close the gate.
			victoryConditionPlayed = true;
			// Set the Surprise SFX onto the audiosource.
			ghostGameSFXPlayer.clip = ghostVictorySFX;
			// Turn on the SFX.
			ghostGameSFXPlayer.Play ();

			ghostGameSFXPlayer.loop = false;


		}  

		Debug.Log ("Microgame timer: " + microgameTimer);

		// LOSE CONDITION: Run out of time, flashlight is not on ghost, and game has not already been won.
		if (microgameTimer <= 4 && !gameWon && !flashlightOnGhost) {

			if (defeatConditionPlayed == false) {
				defeatConditionPlayed = true;
				defeatCondition ();
			}
			return "Lost";


		} else if (gameWon) {

			return "Won";
		}
		// If neither the win or lose condition has been returned, return the "In Progress" string.
		return "In Progress";
	}

	void switchLights () {
		flashlightLight.SetActive (false);
		ceilingLight.SetActive (false);
		//			// Turn on the area light.
		areaLight.SetActive (true);
	}



	void defeatCondition () {
		Debug.Log ("Playing defeat condition.");

		switchLights ();

		for (int i = 0; i < flashlights.Length - 1; i++) {
			flashlights [i].SetActive (false);
		}



		areaLight.GetComponent<Light> ().color = redLightColor;
		for (int i = 0; i < 60; i++) {
			thisStillGhost = Instantiate(stillGhostPrefab, (playerStartingPosition.position + (Random.insideUnitSphere * 4f)), Quaternion.identity);
			// If the ghost spawns in an area to close to the player, they will collide with an object with the tag "Don't Spawn Here"
			// And Spawn again.
			thisStillGhost.GetComponent<AudioSource>().mute = true;
			thisStillGhost.GetComponent<ghostCode> ().ghostsCanDie = false;
			thisStillGhost.transform.LookAt (playerStartingPosition);
			thisStillGhost.GetComponent<ghostCode> ().extraGhost = true;
		}
		ghostGameSFXPlayer.clip = ghostDefeatSFX;
		ghostGameSFXPlayer.Play (); 
		ghostGameSFXPlayer.loop = false;
	
	}

	void checkGameProgress (string result) {
		// If you won or lost, set "DidPlayerWin" to Win or Lose, then set "MicrogameDone to Done".
		// If two seconds have passed since you won the game OR time is up AND you "WON" or "LOST", enter 
		// your current "WIN/LOSE" status.
		if (timeSinceMicrogameOver >= 4.5 && result == "Won" || (microgameTimer <= 0 && result == "Lost")) {
			Debug.Log ("In End Game Function");


			// If you won,
			if (result == "Won") {
				// Set didPlayer Win to True:
				gameOverlordScript.didPlayerWin = true;
				// If you lost,
			} else if (result == "Lost") {
				// Set didPlayer Win to false:
				gameOverlordScript.didPlayerWin = false;
			} 

			Debug.Log("Setting Microgame Done to True.");
			// If you have not yet sent the microgame to done,
			if (microgameOver == false) {
				// Close the door behind this function.
				microgameOver = true;
				// GameOverlord should handle the rest.
				// Tell GameOverlord that the microgame is over.
				gameOverlordScript.microgameDone = true;


			}
		}
	}

}
