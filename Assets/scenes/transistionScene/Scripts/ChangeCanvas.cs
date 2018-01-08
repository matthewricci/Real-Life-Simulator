using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeCanvas : MonoBehaviour {

	//UI Text Objects for Matrix
	public Text surface;	// text attached to the canvas

	// canvas should have four heart images placed in descending order in the hierarchy
	public Image heart1;
	public Image heart2;
	public Image heart3;
	public Image heart4;

	List<Image> hearts = new List<Image>{};		// list containing all hearts attached to this canvas

	//timer
	float timer=2.5f;

	// group of flags that determine which information to display to the player during this instance of the transition room
	bool showWinOrLoss;		
	bool showDifficulty;
	bool showLives;
	bool showNumCompletedTasks;
	bool showInstructions;

	// flag reporting whether or not the four hearts representing "lives" have been enabled on-screen
	bool livesEnabled;

	bool subtractLife;
	int lostHeartIndex = 0;

	// Determine what transitions will need to be done
	void Start () {

		// flag determining whether or not to perform the subtracting-a-life animation
		subtractLife = !(gameOverlordScript.didPlayerWin);  // we subtract a life if the player did not win

		hearts.Add (heart1);
		hearts.Add (heart2);
		hearts.Add (heart3);
		hearts.Add (heart4);

		for (int i = 0; i < hearts.Count; i++) {
			hearts [i].enabled = false;
			if (i < gameOverlordScript.lives) {
				hearts [i].color = Color.red;
			}
		}
		if (subtractLife && gameOverlordScript.lives < hearts.Count) {
			lostHeartIndex = gameOverlordScript.lives;	// if there are X lives left and the player lost, the heart at index X must fade to white
			hearts [lostHeartIndex].color = Color.red;
		}

		livesEnabled = false;

		heart1.enabled = false;
		heart2.enabled = false;
		heart3.enabled = false;
		heart4.enabled = false;

		// always show lives and instructions
		showLives = true;
		showInstructions = true;

		// only show Win/Loss outcome after first microgame is played (after first visit to transition room)
		if (MatrixManager.startDisplayingOutcome) {
			showWinOrLoss = true;
		} else {
			showWinOrLoss = false;
			MatrixManager.startDisplayingOutcome = true;	// set it true for the next time the transition scene is loaded (public static so it will retain its setting)
		}

		if (gameOverlordScript.numTasksCompleted > 0) {
			showNumCompletedTasks = true;
		} else {
			showNumCompletedTasks = false;
		}

		// only show difficulty if it has changed since last microgame (current difficulty and past difficulty stored as public static vars in gameOverlordScript)
		if (gameOverlordScript.pastDifficulty != gameOverlordScript.difficulty) {
			showDifficulty = true;
			gameOverlordScript.pastDifficulty = gameOverlordScript.difficulty;
		} else {
			showDifficulty = false;
		}

	}

	// Update is called once per frame
	void Update () {

		// constantly update timer
		timer -= Time.deltaTime;

		// if we are currently displaying win/loss
		if (showWinOrLoss){

			// constantly update the text
			if (gameOverlordScript.didPlayerWin) {
				surface.text = "Win";
			} else {
				surface.text = "Lose";
			}


			// if we are done showing difficulty, set timer back to default 4s and move to next piece of info
			if (timer <= 0.0f) {
				showWinOrLoss = false;
				timer = 2.5f;
			}
		}
		// if we are currently displaying the difficulty
		else if (showDifficulty) {

			// constantly update the text
			surface.text = "Difficulty: " + gameOverlordScript.difficulty;

			// if we are done showing difficulty, set timer back to default 4s and move to next piece of info
			if (timer <= 0.0f) {
				showDifficulty = false;
				timer = 2.5f;
			}
		}
		// if we are currently displaying the lives
		else if (showLives){
			
			// constantly update the text
			surface.text = "Lives: " + gameOverlordScript.lives;

			if ( ! (livesEnabled) ){

				for (int i = 0; i < hearts.Count; i++) {
					hearts [i].enabled = true;
				}

				livesEnabled = true;
			}

			if (subtractLife && gameOverlordScript.lives < hearts.Count) {
				hearts[lostHeartIndex].color = Color.Lerp (Color.white, Color.red, (timer - 1.0f) );
			}

			// if we are done showing lives, move to next piece of info
			if (timer <= 0.0f) {
				showLives = false;
				for (int i = 0; i < hearts.Count; i++) {
					hearts [i].enabled = false;
				}
				timer = 2.5f;
			}
		}
		// if we are currently displaying number of completed tasks
		else if (showNumCompletedTasks){

			// constantly update the text
			surface.text = "Tasks completed: " + gameOverlordScript.numTasksCompleted;

			// if we are done showing lives, move to next piece of info
			if (timer <= 0.0f) {
				showNumCompletedTasks = false;
				timer = 2.5f;
			}
		}
		// if we are currently displaying instructions
		else if (showInstructions){

			// constantly update the text
			surface.text = gameOverlordScript.nextMGInstructions;


			// if we are done showing lives, move to next piece of info
			if (timer <= 0.0f) {
				showInstructions = false;
				timer = 4.0f;	// is this necessary?
				SceneManager.LoadScene (gameOverlordScript.nextMGBuildIndex);
			}
		}

	}
}
