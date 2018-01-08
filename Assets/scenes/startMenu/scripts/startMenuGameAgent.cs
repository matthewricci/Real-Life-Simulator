using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startMenuGameAgent : MonoBehaviour {

	// This is the keyboard that you use to enter in your name if you get a high score.
	public GameObject keyBoard;

	// This is the text space that contains what you type up on the keyboard.
	public Text keyboardText;

	// This is the enter button that is used to enter your name.
	public GameObject enterButton;

	// These are the numbers that rank the players on the side.
	public GameObject numbers;

	// These are the titles that are part of the High Score Board.
	public GameObject scoreTitle;
	public GameObject nameTitle;

	// This is the GazeRaycaster you use to select the letters on the keyboard.
	public GameObject gazeRaycaster;

	public GameObject inputBackground;


	// These text objects contain the names of the top five players.
	public Text firstHighScoreNameText;
	public Text secondHighScoreNameText;
	public Text thirdHighScoreNameText;
	public Text fourthHighScoreNameText;
	public Text fifthHighScoreNameText;

	// These text objects contain the scores of the top five players.
	public Text firstHighScoreNumberText;
	public Text secondHighScoreNumberText;
	public Text thirdHighScoreNumberText;
	public Text fourthHighScoreNumberText;
	public Text fifthHighScoreNumberText;

	// OBSOLETE
	// This is the string that represents the High Score Names.
//	string firstHighScoreName;
//	string secondHighScoreName;
//	string thirdHighScoreName;
//	string fourthHighScoreName;
//	string fifthHighScoreName;

	// OBSOLETE
	// These are the ints that represent the High Scores.
//	int firstHighScoreNum;
//	int secondHighScoreNum;
//	int thirdHighScoreNum;
//	int fourthHighScoreNum;
//	int fifthHighScoreNum;
//
	// This int holds the High Score Int.
	int newHighScoreInt;
	// This string holds the Name of the New Winner.
	string newHighScoreNameString;

	// This string holds all the High Score Names. It is intialized using the Player Preferences.
	List<string> highScoreNames = new List<string>();
	// This string holds all the High Score ints.  It is intialized using the Player Preferences.
	List<int> highScoreNumbers = new List<int>();

	string stringStuff;

	string startingFirstHighScore;


	// Use this for initialization
	void Start () {

		//PlayerPrefs.DeleteAll ();

		// Set the values for the text objects in the high score:
		SetHighScoreTextValues();

		// Get the player's HighScoreInt value.
		newHighScoreInt = gameOverlordScript.numTasksCompleted;

		Debug.Log ("new High Score Int " + newHighScoreInt);

		// Reset the gameOverlordValue.
		gameOverlordScript.numTasksCompleted = 0;

		// Intialize the High Score names to the High Score Name List.

		highScoreNames.Add (PlayerPrefs.GetString ("firstHighScoreName", "Deckard"));
		highScoreNames.Add (PlayerPrefs.GetString ("secondHighScoreName", "Watson"));
		highScoreNames.Add (PlayerPrefs.GetString ("thirdHighScoreName", "GLaDOS"));
		highScoreNames.Add (PlayerPrefs.GetString ("fourthHighScoreName", "Hal"));
		highScoreNames.Add (PlayerPrefs.GetString ("fifthHighScoreName", "Ava"));


		// Intialize the High Score numbers to the High Score Number List.

		highScoreNumbers.Add (PlayerPrefs.GetInt ("firstHighScoreNumber", 25));
		highScoreNumbers.Add (PlayerPrefs.GetInt ("secondHighScoreNumber", 20));
		highScoreNumbers.Add (PlayerPrefs.GetInt ("thirdHighScoreNumber", 15));
		highScoreNumbers.Add (PlayerPrefs.GetInt ("fourthHighScoreNumber", 10));
		highScoreNumbers.Add (PlayerPrefs.GetInt ("fifthHighScoreNumber", 3));

	
		// If the player has gotten a high enough score to get into the scoreboard,
		if (newHighScoreInt > highScoreNumbers [4]) {

			Debug.Log ("Higher than lowest score.");

			// Turn off high score, turn on keyboards.
			FalseToAddHighScoreTrueToShowHS (false);

			// The player will add their high score and press the "Enter Button", which will call the function,
			// "Press the Enter Button."

			// If they input a proper name, the "PressEnterButton" function will call the "EnterHighScoreName."


			//PressEnterButton ();



		} else {
			
		}

	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.V)) {

			Debug.Log ("Pressing V.");
			PressEnterButton ();
		}

		if (Input.GetKeyDown(KeyCode.R)) {

			Debug.Log ("Pressing R.");
			PlayerPrefs.DeleteAll ();

		}

	}

	// This sets the intial high score text values.
	public void SetHighScoreTextValues () {
		// Set the text value for all the text objects.

		// Set the default value for High Score names.
		firstHighScoreNameText.text = PlayerPrefs.GetString ("firstHighScoreName", "Deckard");
		secondHighScoreNameText.text = PlayerPrefs.GetString ("secondHighScoreName", "Watson");
		thirdHighScoreNameText.text = PlayerPrefs.GetString ("thirdHighScoreName", "GLaDOS");
		fourthHighScoreNameText.text = PlayerPrefs.GetString ("fourthHighScoreName", "Hal");
		fifthHighScoreNameText.text = PlayerPrefs.GetString ("fifthHighScoreName", "Ava");


		// Set the default value for the High Score scores.
		firstHighScoreNumberText.text = PlayerPrefs.GetInt ("firstHighScoreNumber", 25).ToString();
		secondHighScoreNumberText.text = PlayerPrefs.GetInt ("secondHighScoreNumber", 20).ToString();
		thirdHighScoreNumberText.text = PlayerPrefs.GetInt ("thirdHighScoreNumber", 15).ToString();
		fourthHighScoreNumberText.text = PlayerPrefs.GetInt ("fourthHighScoreNumber", 10).ToString();
		fifthHighScoreNumberText.text = PlayerPrefs.GetInt ("fifthHighScoreNumber", 3).ToString();

	}

	// This what happens when you press the enter button.
	public void PressEnterButton() {
		Debug.Log ("Enter button pressed.");
		// When the player presses the "ENTER" button.
		// If the player did not enter any characters for a high score name.
		if (keyboardText.text.Length < 1 || keyboardText.text == "Enter at least one character.") {
			// Output this text.
			keyboardText.text = "Enter at least one character.";
			// Call the function again.


		} else {
			Debug.Log (keyboardText.text);

			// Return what the player entered as their name.
			EnterHighScoreInfo(keyboardText.text);
		}
	}
		

	// Change the High Score.
	public void EnterHighScoreInfo(string playerName) {

		Debug.Log ("playerName is :" + playerName);
		// Go through the values of HighScore numbers until your score is higher than one of the values.
		for (int i = 0; i < highScoreNumbers.Count; i++) {
			// If the player's current value is greater than the value in the high score.
			if (newHighScoreInt > highScoreNumbers [i]) {

				// Insert the high score number into the high score list.
				highScoreNumbers.Insert (i, newHighScoreInt);
				// Insert the high score name into the high score list.
				highScoreNames.Insert (i, playerName);
				// Leave the for loop.
				break;
			}
		}
			
		// Set the Player Preferences to the values of the current list.
		PlayerPrefs.SetString ("firstHighScoreName", highScoreNames [0]);
		PlayerPrefs.SetString ("secondHighScoreName", highScoreNames [1]);
		PlayerPrefs.SetString ("thirdHighScoreName", highScoreNames [2]);
		PlayerPrefs.SetString ("fourthHighScoreName", highScoreNames [3]);
		PlayerPrefs.SetString ("fifthHighScoreName", highScoreNames [4]);

		PlayerPrefs.SetInt ("firstHighScoreNumber", highScoreNumbers [0]);
		PlayerPrefs.SetInt ("secondHighScoreNumber", highScoreNumbers [1]);
		PlayerPrefs.SetInt ("thirdHighScoreNumber", highScoreNumbers [2]);
		PlayerPrefs.SetInt ("fourthHighScoreNumber", highScoreNumbers [3]);
		PlayerPrefs.SetInt ("fifthHighScoreNumber", highScoreNumbers [4]);

		// Save all changes to the disk.
		PlayerPrefs.Save ();

		// Reset addHighScore.
		gameOverlordScript.addHighScore = false;

		//
		SetHighScoreTextValues();

		// Turn off the keyboard and show the HS board.
		FalseToAddHighScoreTrueToShowHS (true);
	}



		// Turn on the keyboard and turn off the high scores.
		

	


	// FALSE: Keyboard On, High Scores Off
	// TRUE: Keyboard Off, High Scores On
	public void FalseToAddHighScoreTrueToShowHS (bool value) {



		// HIGH SCORE BOARD:

		// Set text of high score names to the value.
		firstHighScoreNameText.enabled = value;
		secondHighScoreNameText.enabled = value;
		thirdHighScoreNameText.enabled = value;
		fourthHighScoreNameText.enabled = value;
		fifthHighScoreNameText.enabled = value;

		// Set text of high score numbers to value.
		firstHighScoreNumberText.enabled = value;
		secondHighScoreNumberText.enabled = value;
		thirdHighScoreNumberText.enabled = value;
		fourthHighScoreNumberText.enabled = value;
		fifthHighScoreNumberText.enabled = value;

		// The numbers that are ranked.
		numbers.SetActive (value);

		// The titles that designate the score and name columns.
		scoreTitle.SetActive (value);
		nameTitle.SetActive (value);

		// NAME ENTRY:

		// KEYBOARD
		keyBoard.SetActive(!value);

		// Keyboard Text
		keyboardText.enabled = !value;

		// Enter Button.
		enterButton.SetActive(!value);

		// GazeRaycaster.
		gazeRaycaster.SetActive(!value);


	}

}
