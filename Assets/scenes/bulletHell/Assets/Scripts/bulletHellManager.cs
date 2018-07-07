using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class bulletHellManager : MonoBehaviour {

	public float timer = 7.0f;			// player gets limited amount of time
	public float totalTimeForWatch;

	int difficulty;

	public bool gameOver;			// flag representing if the endgame requirements have been met
	public bool gameWon;			// flag representing if the win condition has been met
	public bool postgameInitiated;	// flag representing whether post-game functions have been called
	public bool hasBeenHit;

	void Awake() {
		timer = 7.0f;
		totalTimeForWatch = timer;
		gameOverlordScript.wristWatchTime = totalTimeForWatch;
	}

	// Use this for initialization
	void Start () {
		// this is a timed microgame, so gameWon is set to true by default. this will get switched
		// to false if at any point an "enemy" touches the player's "sphere"; occurs in seekPlayer script
		gameWon = true;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		// if game has finished
		if (gameOver) {
			if (timer < 0.0f) {
				timer = 99.0f; // hacky fix to keep the transitioning working once the microgame ends
				// if player won and win/loss response has played, set gameOverlord values to true
				if (gameWon) {
					gameOverlordScript.didPlayerWin = true;
				} else {
					gameOverlordScript.didPlayerWin = false;
				}

				gameOverlordScript.microgameDone = true;
			}
		}
		// else continue to check if player has been hit, or if player has survived the full length
		else {
			checkIfHit ();
		}
	}

	// every frame, check if sphere has been hit. If so, transition to losing feedback
	void checkIfHit(){

		// if seekPlayer has reported the sphere has been hit
		if (hasBeenHit){
			timer = 3.0f;
			gameOver = true;
			gameWon = false;
		}
		// else check if the timer has ran out without the sphere being hit by an enemy
		else if (timer <= 0.0f){
			timer = 3.0f;
			gameOver = true;
			// gameWon is defaulted to true, no need to set it
		}
	}

}
