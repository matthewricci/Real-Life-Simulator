using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncingObjectGameAgent : MonoBehaviour {

	//public GameObject concreteFloor;

	// What object should the game spawn?
	public GameObject eggPrefab;

	// How many eggs should the game spawn?
	public int literalEggsToSpawn;

	//IRRELEVANT: Use num of BouncedEggs now.
	// How long does the player have to stay alive?
	float eggTimer = 10;

	// The variable which tracks how much time has gone by since the scene started.
	public float roundTimer = 0;

	// Game management booleans.
	public bool microgameOver = false;
	public bool microgameStarted = false;
	public bool playGame = true;

	// How many how many eggs have been  cracked on floor.
	public int yolkNum;
	// Array which collects all dead (cracked) eggs.
	public GameObject[] yolkArray;

	//public float eggGameStartTime;

	// Player for this game:
	public AudioSource eggGamePlayerSFX;
	// SFX to play if you win.
	public AudioClip eggGameVictory;
	// SFX to play if you lose.
	public AudioClip eggGameDefeat;

	// The player.
	public Transform player;

	// Where you should be teleported if you win or lose.
	public Transform victoryTeleportPoint;
	public Transform defeatTeleportPoint;

	// The time that has past since the game has ended.
	public float timeSinceMicrogameOver;

	// Did the player win the microgame?
	bool microGameVictory = true;

	// Are you in the defeat room?
	bool inDefeatRoom = false;
	bool inVictoryRoom = false;

	// Float for egg X position.
	float eggXPosValue;
	// Float for egg Z position.
	float eggZPosValue;
	// What height should the first egg start from?
	public float intialEggHeight = 10f;

	// This variable represents the total number of time it takes for the scene to pass:
	float totalTimeForWatch;

	// This integer measures how many eggs have been bounced.
	public int eggsBounced = 0;

	// This is the newest spawned egg.
	GameObject newSpawnedEgg;

	// This is the drag of the new spawned egg.
	float newSpawnedEggDrag;

	AudioSource newSpawnedEggAudioSource;



	void Awake () {
		// This variable represents the total number of time it takes for the scene to pass:
		totalTimeForWatch = eggTimer;
		// We change the public static variable for wristwatchTime to the duration fo this scene.
		gameOverlordScript.wristWatchTime = totalTimeForWatch;
	}

	// Update is called once per frame
	void Update () {
		 //I don't think we need the code immediately below:
		 //If play game if you have not already.
				if (playGame) {
					eggFallingGame (gameOverlordScript.difficulty);
				}
	}


	// Main function for Egg Falling Game.
	void eggFallingGame (int difficultyLevel) {
		// Determine number of eggs to spawn by difficulty level.
		literalEggsToSpawn = difficultyLevel * 2;

		// Add time to the round timer.
		roundTimer = roundTimer + Time.deltaTime;

		// If the microgame has not started and the microgame is not over:
		if (microgameStarted == false && microgameOver == false) {
			// Spawn the eggs.
			startEggGame (literalEggsToSpawn);
			// The microgame has now started.
			microgameStarted = true;
		}

		// If the microgame has been started and microgame is not over, check the win state of the game.
		if (microgameStarted == true && microgameOver == false) {
			// Run the function which checks the egg game.
			eggFallingGameInternal (literalEggsToSpawn);
		}

		// If the microgame is completed,
		if (microgameOver) {
			
			// measure how much time has gone by since the microgame is over,
			timeSinceMicrogameOver += Time.deltaTime;

			if (inDefeatRoom == false && microGameVictory == false && timeSinceMicrogameOver >= 1f) {
				// Teleport the player to the defeat position.
				player.position = defeatTeleportPoint.position;

				// Set up and play the defeat SFX.
				eggGamePlayerSFX.clip = eggGameDefeat;

				// Play the defeat SFX
				eggGamePlayerSFX.Play ();

				// Close the gate:
				inDefeatRoom = true;

			} else if (inVictoryRoom == false && microGameVictory == true && timeSinceMicrogameOver >= 1f) {
				// Teleport the player to the win position.
				player.position = victoryTeleportPoint.position;

				// Set up and play the victory SFX.
				eggGamePlayerSFX.clip = eggGameVictory;

				// Play the victory SFX:
				eggGamePlayerSFX.Play ();

				// Close the game:
				inVictoryRoom = true;
			}

		

			// OBSOLETE:
//			// If the player has lost and they are not in the defeat room.
//			if (microGameVictory == false && inDefeatRoom == false) {
//				
//
//			}

			// Recognize microgame over:
			//Debug.Log("Microgame Over");
			Debug.Log (roundTimer);

			// If time is up for the game,
			if (roundTimer >= eggTimer) {
				
				// OBSOLETE:
				// Adding the timer reset so that it doesn't continue to set microgameDone to true even after gameoverlord to sets it to false.
				//timeSinceMicrogameOver = 0f;

				// Set the round timer to zero this IF statement does not double dip.
				roundTimer = 0f;

				Debug.Log ("In the end game.");
				// If the player won the microgame:
				if (microGameVictory) {
					// Set GameOverlord bools to appropriate levels:
					// Set didPlayer Win to True:
					gameOverlordScript.didPlayerWin = true;
					// If the player lost the microgame,
				} else if (!microGameVictory) {
					// Set GameOverlord bools to appropriate levels:
					// Set didPlayer Win to false:
					gameOverlordScript.didPlayerWin = false;
				}
				// Tell GameOverLord to compute the next step.
				gameOverlordScript.microgameDone = true;
				// GameOverlord should handle the rest.
			}
		}
	}


	// This function checks the whether the game is over or not.
	void eggFallingGameInternal (int literalEggsNum) {
		// Put all the live eggs into an array.
		yolkArray = GameObject.FindGameObjectsWithTag ("yolk");
		// Count the number of live eggs in the array.
		yolkNum = yolkArray.Length;


		// If the player has bounced all the eggs.
		if (eggsBounced >= literalEggsToSpawn) {
			// You win!

			// Set MicroGame victory to true:
			microGameVictory = true;

			// Set the microgameOver variable to true.
			microgameOver = true;

			// If there are less eggs than there were at the start of the game (one has cracked),
		} else if (yolkNum > 0 && inDefeatRoom == false) {
			// You lose!

			// Set MicroGame victory to false:
			microGameVictory = false;

			//Set the microgame over variable to true.
			microgameOver = true;
		
		} 
	}



	// Spawn the number of eggs.
	void startEggGame (int numOfEggs) {

		float thisDifficulty = (numOfEggs / 2);


		// Spawn the number of eggs.
		for (int i = 0; i < numOfEggs; i++) {

			// If we are on level three or below,
			if (thisDifficulty <= 4) {
				// Make the new eggs spawn closer to the ground depending on the level.
				intialEggHeight -= (thisDifficulty * .3f);
				// If we are beyond level three,
			} else {
				// Start from the same very low point each time. 
				intialEggHeight -= (3 * .2f);
			}

		
			// The height for this egg.
			float thisEggHeight;
			// This sets the height for the current egg depending on which egg this is and the intial egg height.
			thisEggHeight = intialEggHeight + (i * 2);

			// Add in randomization.

			eggXPosValue = Random.Range(-.65f, 3.2f);
			eggZPosValue = Random.Range (2.4f, 5.7f);
			// Spawn the eggs at this preset location.

			Vector3 posOne = new Vector3 (eggXPosValue, thisEggHeight, eggZPosValue);

			// Spawn eggs. Assign the new Egg to NewSpawnedEgg.
			newSpawnedEgg = Instantiate (eggPrefab, posOne, Quaternion.identity);
			// Get the RigidBody of the Egg and then get the Drag Value.
			newSpawnedEggDrag = newSpawnedEgg.GetComponent<Rigidbody> ().drag;
			// If Difficulty is less than three:
			// Calculate a new drag by subtracting the current value by the difficulty times by .5f;
			if (thisDifficulty <= 4) {
				newSpawnedEggDrag -= (thisDifficulty * .5f);
				// If the difficulty is greater than three,
				// Default to the lowest possible drag (Fastest eggs)
			} else {
				newSpawnedEggDrag -= (3 * .5f);
			}
			// Set the new drag for each egg.
			newSpawnedEgg.GetComponent<Rigidbody> ().drag = newSpawnedEggDrag;
		}
	}


}
