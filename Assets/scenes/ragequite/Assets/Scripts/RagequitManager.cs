using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class RagequitManager : MonoBehaviour {

	public Canvas angerCanvas;
	public GameObject fallback;
	public GameObject fallbackHand;
	public Image panel;
	public int numberItemsBroken;	// track how many items broken
//	public int newBrokenItems;

	public float timer = 7.0f;			// player gets limited amount of time
	public float totalTimeForWatch;
	public float angerLevel; 		// track current anger level
	int difficulty;

	public bool gameOver;			// flag representing if the endgame requirements have been met
	public bool gameWon;			// flag representing if the win condition has been met
	public bool postgameInitiated;	// flag representing whether post-game functions have been called


	void Awake() {
		timer = 7.0f;
		totalTimeForWatch = timer;
		gameOverlordScript.wristWatchTime = totalTimeForWatch;
	}

	// Use this for initialization
	void Start () {
//		timer = 7.0f;
		angerLevel = 0.00f;
		numberItemsBroken = 0;
//		newBrokenItems = 0;
		difficulty = gameOverlordScript.difficulty;
		gameOver = false;
		gameWon = false;
		postgameInitiated = false;

		// if we are in VR fallbac mode
		if (fallback.activeSelf) {
			Debug.Log ("fallback activated");
			angerCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

			//angerCanvas.render
		}

		// initialize the alpha value of the panel to 0
		panel.color = new Color (
			panel.color.r, 
			panel.color.g, 
			panel.color.b, 
			angerLevel				// initialized to 0.03f
		);

	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		// if game has finished
		if (gameOver) {
			if (timer < 0.0f) {
				timer = 99.0f;	// hacky fix to keep the transitioning working once the microgame ends
				// if player won and win/loss repsonse has played, set gameOverlord values to true
				if (gameWon) {
					gameOverlordScript.didPlayerWin = true;
				} else {
					gameOverlordScript.didPlayerWin = false;
				}

				gameOverlordScript.microgameDone = true;
			}
		} 
		//	else check if game should end and continue updating alpha
		else {

			// every frame, set panel color based on number of items that were hit by player, maximum of 50% alpha
			setPanel( getAngryAlpha () );  // pass in alpha based on anger level

			// every frame, check if player has calmed down enough
			checkIfWon ();
		}
	}

	// calculates and returns new alpha based on angerLevel
	float getAngryAlpha(){
		//return Mathf.Min (1.0f, (0.007f * numberItemsBroken));

//		float relaxation = 0.1f * newBrokenItems / Mathf.Max(1, difficulty); // relaxation is based on number of new things broken, higher difficulty makes it harder to relax
//		newBrokenItems = 0;		// reset newBrokenItems as each new item can only affect anger level once

//		angerLevel += ( 0.001f - relaxation );  // player has the chance to "relax" the anger by smashing things


		angerLevel = numberItemsBroken * 0.02f;
		return angerLevel;
	}

	// set the alpha of the panel overlaying the player's screen to the passed-in value
	void setPanel(float newAlpha){
		panel.color = new Color (
			panel.color.r, 
			panel.color.g, 
			panel.color.b, 
			newAlpha
		);
	}


	// checks each Update() if the game should finish itself up
	void checkIfWon(){
		
		// if player has relaxed fully
		if (numberItemsBroken >= 4 + (2 * difficulty) ) {
			timer = 3.0f;
			gameOver = true;
			gameWon = true;
		} 

		// else if anger has overtaken the player
		//else if (angerLevel >= 0.5f) {
		else if( timer <= 0.0f) {
			timer = 3.0f;
			gameOver = true;
			setPanel (0.0f);	// player lost, set alpha back to 0 for the rest of the microgame cycle

		}
	}
}
