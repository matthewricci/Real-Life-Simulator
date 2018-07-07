using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class BurglarManager : MonoBehaviour {

	// three tiers of difficulty, objects for each tier stored in these objects
	public GameObject difficulty1;
	public GameObject difficulty2;
	public GameObject difficulty3;

	// List container for all stealable items that will be randomized
	public List<stealable> front_items = new List<stealable>{};
	public List<stealable> left_items = new List<stealable>{};
	public List<stealable> right_items = new List<stealable>{};

	// don't randomize the items on the first playthrough
	public static bool firstPlay=true;

	//public Canvas angerCanvas;
	public GameObject fallback;
	public GameObject fallbackHand;
	//public Image panel;
//	public int numberItemsStolen;	// track how many items stolen
	public int totalValueStolen;
//	public int newBrokenItems;

	public float timer = 20;			// player gets limited amount of time
	public float totalTimeForWatch;
	//public float angerLevel; 		// track current anger level
	int difficulty;

	public bool gameOver;			// flag representing if the endgame requirements have been met
	public bool gameWon;			// flag representing if the win condition has been met
	public bool postgameInitiated;	// flag representing whether post-game functions have been called

	public AudioSource neutralMusicAS; // The music that plays during the game.


	void Awake() {
		//timer = 7.0f;
		totalTimeForWatch = timer;
		gameOverlordScript.wristWatchTime = totalTimeForWatch;
		if (!(neutralMusicAS.isPlaying)) {	// if not already sighing
			neutralMusicAS.Play ();			// start to sigh
		}
	}

	// Use this for initialization
	void Start () {
//		timer = 7.0f;
		//angerLevel = 0.00f;
		//numberItemsStolen = 0;
		totalValueStolen = 0;
//		newBrokenItems = 0;
		difficulty = gameOverlordScript.difficulty;
		if (difficulty == 1) {					// logic for scaling the difficulty
			difficulty1.SetActive (true);
			timer = 15f;
		} else if (difficulty == 2) {
			difficulty2.SetActive (true);
		} else {
			difficulty3.SetActive (true);
		}
//		if (difficulty >= 2) {							// logic for scaling the difficulty 
//			difficulty2.SetActive (true);				// if difficulty is 2 or higher, activate second tier of items
//		}
//		if (difficulty >= 3) {							// if difficulty is 3 or higher, activate third tier of items
//			difficulty3.SetActive (true);
//		}

		gameOver = false;
		gameWon = false;
		postgameInitiated = false;

		front_items = new List<stealable> (difficulty1.GetComponentsInChildren<stealable> ());
		left_items = new List<stealable> (difficulty2.GetComponentsInChildren<stealable> ());
		right_items = new List<stealable> (difficulty3.GetComponentsInChildren<stealable> ());

		// if we are in VR fallbac mode
		if (fallback.activeSelf) {
			Debug.Log ("fallback activated");
			//angerCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

			//angerCanvas.render
		}

		//randomizeItems ();


//		// initialize the alpha value of the panel to 0
//		panel.color = new Color (
//			panel.color.r, 
//			panel.color.g, 
//			panel.color.b, 
//			//angerLevel				// initialized to 0.03f
//		);

	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		// if game has finished
		if (gameOver) {
			neutralMusicAS.mute = true; // Turn off the neutral music.
			if (timer < 0.0f) {
				timer = 99.0f;	// hacky fix to keep the transitioning working once the microgame ends
				// if player won and win/loss repsonse has played, set gameOverlord values to true
				if (gameWon) {
					gameOverlordScript.didPlayerWin = true;
				} else {
					gameOverlordScript.didPlayerWin = false;
				}
				if (firstPlay) {
					firstPlay = false;
				}
				gameOverlordScript.microgameDone = true;
			}
		} 
		//	else check if game should end and continue updating alpha
		else {

			// every frame, set panel color based on number of items that were hit by player, maximum of 50% alpha
			//setPanel( getAngryAlpha () );  // pass in alpha based on anger level

			// every frame, check if player has calmed down enough
			checkIfWon ();
		}
	}

	// calculates and returns new alpha based on angerLevel
//	float getAngryAlpha(){
//		//return Mathf.Min (1.0f, (0.007f * numberItemsBroken));
//
////		float relaxation = 0.1f * newBrokenItems / Mathf.Max(1, difficulty); // relaxation is based on number of new things broken, higher difficulty makes it harder to relax
////		newBrokenItems = 0;		// reset newBrokenItems as each new item can only affect anger level once
//
////		angerLevel += ( 0.001f - relaxation );  // player has the chance to "relax" the anger by smashing things
//
//
//		//angerLevel = numberItemsBroken * 0.02f;
//		//return angerLevel;
//	}
//
	// set the alpha of the panel overlaying the player's screen to the passed-in value
//	void setPanel(float newAlpha){
//		panel.color = new Color (
//			panel.color.r, 
//			panel.color.g, 
//			panel.color.b, 
//			newAlpha
//		);
//	}


	// checks each Update() if the game should finish itself up
	void checkIfWon(){
		
		//if (totalValueStolen >= Mathf.Min(500, 100 + (100 * gameOverlordScript.difficulty)) ) {	// WIN CONDITION: Player must steal a minimum of either
		if (totalValueStolen > 500){
			timer = 3.0f;													// 10 items, or 2 + (2*difficulty) items; whichever is lower
			gameOver = true;												// this limits the difficulty to something feasible while
			gameWon = true;													// still being scalable
		} 
			
		else if( timer <= 0.0f) {
			timer = 3.0f;
			gameOver = true;

		}
	}

	void randomizeItems(){
		for (int i = 0; i < front_items.Count; i++) {
			int swappingIndex = Random.Range (0, front_items.Count);
			Vector3 temp = front_items [i].transform.position;
			Debug.Log (temp);
			front_items [i].transform.position = front_items [swappingIndex].transform.position;
			front_items [swappingIndex].transform.position = temp;
		}
	}
}
