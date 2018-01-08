using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightGameAgent : MonoBehaviour {

	// This object holds the redLever Object:
	public GameObject redLever;

	// Timer for this particular scene:
	float microgameTimer = 9;

	// This bool determines whether the player has won or not. It is turned on or off by the lever script.
	public bool gameWon = false;

	// This variable represents the total number of time it takes for the scene to pass:
	float totalTimeForWatch;

	// This string contains a word or phrase which says whether the game is won, lost, or in progress.
	string gameResult = "In Progress";

	// This bool measures whether the game is over or not.
	bool microgameOver = false;

	// This int determines which wall the lever will appear on.
	public int wallIndex;

	// This is the array that will hold all of the walls.
	public Transform[] walls;

	// This is the transform of the wall on which the lever will be spawned.
	Transform chosenWall;

	// This variable represents the x value of the spawn position of the lever.
	float leverSpawnPosX;
	// This variable represents the y value of the spawn position of the lever.
	float leverSpawnPosY;
	// This variable represents the z value of the spawn position of the lever.
	float leverSpawnPosZ;

	// These variables hold the various lights in the scene, which are turned on or off at the win condition.
	public GameObject flashlightLight;
	public GameObject ceilingLight;
	public GameObject areaLight;

	// Gate for first leverTrigger.
	bool firstLeverDone = false;
	// Audiosource for SFX.
	public AudioSource flashlightGameSFXPlayer;
	// Victory SFX
	public AudioClip surpriseSFX;

	void Awake () {
		// This variable represents the total number of time it takes for the scene to pass:
		totalTimeForWatch = microgameTimer;
		// We change the public static variable for wristwatchTime to the duration fo this scene.
		gameOverlordScript.wristWatchTime = totalTimeForWatch;
	}

	// Use this for initialization
	void Start () {
		// Set up the flashlightGame at the given difficulty.
		startFlashlightGame (gameOverlordScript.difficulty);
	}
	
	// Update is called once per frame
	void Update () {
		// Countdown the microgame timer.
		microgameTimer -= Time.deltaTime;
		// Ask flashlight internal the result of the current game.
		gameResult = flashLightGameInternal ();
		// Take that result and act accordingly in the check game progress function:
		//Debug.Log(gameResult);
		// The checkGameProgress function will do the level acounting and end the game if 
		// flashLightGameInternal returns a "Win" or "Lose" condition.
		checkGameProgress (gameResult);

	}
		
	// This game sets up the game intially.
	void startFlashlightGame (int difficulty) {

		// TO DO:
		// Add randomization:
		// Spawn a redLever.

		// Pick one of four walls to spawn on.
		wallIndex = Random.Range (0, 4);

		// Store the chosen wall in a wall variable.
		chosenWall = walls [wallIndex];

		// Set the leverPosition to the position of theWall.
		Vector3 leverPosition = chosenWall.transform.position;
		// Chose a random position on x axis for the lever relative to the wall's position.
		float leverSpawnPosX = Random.Range (-1f,1.5f);
		// Chose a random position on y axis for the lever relative to the wall's position.
		float leverSpawnPosY = Random.Range (-1f, 1.5f);

		// Take the the wall's relative position on the x axis and multiply by the random amount chosen above.
		leverPosition += chosenWall.right * leverSpawnPosX;
		// Take the the wall's relative position on the y axis and multiply by the random amount chosen above.
		leverPosition += chosenWall.up * leverSpawnPosY;
		// Take the the wall's relative position on the z axis and multiply by the random amount chosen above.
		leverPosition += (chosenWall.forward * .2f);

		// Store the rotation of the chosen wall.
		Quaternion leverSpawnRotationQuaternion = chosenWall.transform.rotation;
		// Convert the stored rotation of the chosen wall to Euler Angles (Vector3) so you can manipulate them.
		Vector3 leverSpawnRotation = leverSpawnRotationQuaternion.eulerAngles;
		// Adjust the Y and Z values of the rotation Vector3 so that the lever is orientated towards the center
		// of the room.
		leverSpawnRotation.z = chosenWall.rotation.z + 90;
		leverSpawnRotation.y = chosenWall.rotation.y - 270;

		// Spawn the redLever prefab at the postion and rotation determined above.
		GameObject newRedLever = Instantiate (redLever, leverPosition, chosenWall.transform.rotation);
		// Rotate the redLever so it faces the front of the room.
		newRedLever.transform.Rotate(leverSpawnRotation);




		
//		Vector3 leverSpawnPosTotal = new Vector3 (leverSpawnPosX, leverSpawnPosY, leverSpawnPosZ);
//		
//		Vector3 leverSpawnRotation = new Vector3 ();
		
		//Instantiate (redLever, leverSpawnPosTotal, leverPositionOne.rotation);

//		if (thisWall == "Front Wall") {
//
//
//
//			leverSpawnPosX = 0;
//
//			leverSpawnPosY = 0;
//
//			leverSpawnPosZ = 0;
//
//			Vector3 leverSpawnPosTotal = new Vector3 (leverSpawnPosX, leverSpawnPosY, leverSpawnPosZ);
//
//			Vector3 leverSpawnRotation = new Vector3 ();
//
//			Instantiate (redLever, leverSpawnPosTotal, leverPositionOne.rotation);
//
//		} else if (thisWall == "Back Wall" ) {
//
//		} else if (thisWall == "Left Wall" ) {
//
//		} else if (thisWall == "Right Wall" ) {
//
//		} else if (thisWall == "Ceiling" ) {
//
//		}




	}


	// This function checks if the win or lose condition for this game has been met.
	string flashLightGameInternal () {
		
		if (gameWon && firstLeverDone == false) {
			// Turn off the flashlight and ceiling light:
			flashlightLight.SetActive (false);
			ceilingLight.SetActive (false);
			// Turn on the area light.
			areaLight.SetActive (true);
			// Close the gate.
			firstLeverDone = true;
			// Set the Surprise SFX onto the audiosource.
			flashlightGameSFXPlayer.clip = surpriseSFX;
			// Turn on the SFX.
			flashlightGameSFXPlayer.Play ();


		}

		// LOSE CONDITION: Run out of time:
		if (microgameTimer < 0) {
			return "Lost";

		// WIN CONDITION: Switch has been turned on.
		} else if (gameWon && microgameTimer < .4f) {

			return "Won";
		}

		return "In Progress";

	}

	// This function checks the result from internal game function. If you win or lose, it sets the appropriate
	// GameOverLord variable.
	void checkGameProgress (string result) {
		// If you won or lost, set "DidPlayerWin" to Win or Lose, then set "MicrogameDone to Done".
		if (result == "Won" || result == "Lost") {
			Debug.Log ("In End Game Function");

			if (result == "Won") {
				// Set didPlayer Win to True:
				gameOverlordScript.didPlayerWin = true;
			} else if (result == "Lost") {
				// Set didPlayer Win to false:
				gameOverlordScript.didPlayerWin = false;
			} 

			Debug.Log("Setting Microgame Done to True.");
			if (microgameOver == false) {
				// Tell GameOverlord that the microgame is over.
				gameOverlordScript.microgameDone = true;

				// Close the door behind this function.
				microgameOver = true;
			// GameOverlord should handle the rest.
			}
		}
	}




}
