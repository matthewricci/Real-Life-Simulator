/* 
This script should be on every gameOverlord prefab, which needs to be included in EVERY scene.
Microgame managing behavior:
	Initially, allMicrogameIndexes is filled with indexes of every microgame build index.
	As MGs are randomly chosen, their indexes are added to usedMicrogameIndexes; this list is
		checked every time we load a new MG to make sure we aren't repeating.
	Once all MGs have been played, empty this list and repeat this process.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class gameOverlordScript : MonoBehaviour {

	// these are set during compile time(?) and are not reset from scene to scene
	public static int lives = 4;				// player lives
	public static int numTasksCompleted = 0;	// number of games completed by the player
	// WON or Completed?
	public static int totalTasks = 4;			// total unique microgames to play
	public static int difficulty = 1;			// difficulty of the games, start at 1
	public static int difficultyModifier = 0;   // additional difficulty boost determined at start
	public static int pastDifficulty = 1;		// used by transition scene to know when difficulty has been changes
	public static bool difficultyCalculated = false;	// used by MatrixManager to determine when to check if the difficulty is different
	public static int numNonMGScenes = 3;		// number of scenes we have that are NOT microgames, this is also the build index of the first MG

	public static bool microgameDone = false;	// flag to determine if player is playing

	public static bool didPlayerWin = false;	// flag that tells gameOverlord whether or not the player won the last microgame. only check this AFTER microgameDone is set to true

	// these Lists must be of type 'int' to be serializable (to appear in Unity editor)
	public static List<int> allMicrogameIndexes = new List<int> {};  	// list containing build indexes of all built microgames
	public static List<int> usedMicrogameIndexes = new List<int> {};	// list containing build indexes of MG scenes already played

	// Dictionary containing a scene name as a key and a microgame instruction as a value
	public static Dictionary<int, string> microgameInstructions = new Dictionary<int, string> {};

	// flag used to determine whether this is the first time the player has accessed the Start Menu
	public static bool firstStartup = false;

	// holds the build index of the next microgame to load, not necessary for it to be static
	public static int nextMGBuildIndex;
	public static int pastMGBuildIndex;

	public static string nextMGInstructions;

	// This static float is assigned a value in the start function of each scene. The wristwatch prefab then takes it.
	public static float wristWatchTime = 20;

	// This bool determines whether you should add your high score when you enter the Start Menu.
	public static bool addHighScore;

	// Ran on the first frame of EVERY scene, because an instance of GameOverlord goes in every scene
	void Start () {

	

	//	Debug.Log ("active scene: " + SceneManager.GetActiveScene ().name);

		if (!(firstStartup)) {

			// this loop should only be run once per startup
			firstStartup = true;

			// initialize the allMicrogames list on the first frame so we can reference them
			initializeMicrogameList ();

			// initialize dictionary with instructions
			initializeMicrogameInstructions ();


		}

		// if we have loaded the start menu scene
		if (SceneManager.GetActiveScene ().name == "Start Menu") {
			
		} 
		// else if we have loaded a transition scene, perform maintenance tasks
		else if (SceneManager.GetActiveScene ().name == "Matrix") {

			//re-calculate the difficulty based on new number of tasks completed
			calculateDifficulty ();
			// get build index of next microgame to load
			nextMGBuildIndex = chooseRandomMicrogameIndex ();
			pastMGBuildIndex = nextMGBuildIndex;

			//get instructions based on nextMGBuildIndex 
			nextMGInstructions = retrieveNextMGInstructions();

		}
		// TO BE FLESHED OUT..JUST SETTING UP THE FRAMEWORK FOR IT
		else if (SceneManager.GetActiveScene ().name == "gameOverScene") {

			// empty the list of used microgames before the game restarts
			usedMicrogameIndexes.Clear ();

			// set outcome flag back to false for next round
			MatrixManager.startDisplayingOutcome = false;

		}
		// else we are in one of the microgame scenes (FLESH THIS OUT TOO)
		else {

		}
	}

	// Ran on every frame of every scene
	void Update () {
		// if the build index of the active scene is greater than 2, we are in a microgame
		if (SceneManager.GetActiveScene().buildIndex > 2) {
			if (microgameDone) {		// has the microgame finished yet?
				microgameDone = false;
				numTasksCompleted += 1;

				// if the player didn't win, remove a life
				if (!(didPlayerWin)) {
					lives -= 1;
				}

				// logic to check what scene to load next now that the microgame has ended
				if (lives <= 0) {	// if we are out of lives, game is over
					SceneManager.LoadScene ("gameOverScene");
				} else {			// otherwise load the transition scene to get ready for the next microgame
					SceneManager.LoadScene ("Matrix");
				}

			}
		}

		// DEBUGGING: hit button associated with build index to go to specific scene
		if (Input.anyKeyDown) {
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				SceneManager.LoadScene (3);
			} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				SceneManager.LoadScene (4);
			} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
				SceneManager.LoadScene (5);
			} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
				SceneManager.LoadScene (6);
			} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
				SceneManager.LoadScene (7);
			} else if (Input.GetKeyDown (KeyCode.Equals)) {
				difficulty += 1;
				Debug.Log ("Added 1 to difficulty. New Difficulty: " + difficulty);
			} else if (Input.GetKeyDown (KeyCode.Minus)) {
				if (difficulty > 1) {// can be no less than 1
					difficulty -= 1;
					Debug.Log ("Subtracted 1 from difficulty. New Difficulty: " + difficulty);
				} 
				else
					Debug.Log ("Already at lowest difficulty.");
			}
		}
	}

	// fill index list with MG build indexes starting from first microgame build index
	void initializeMicrogameList(){
		// we can use numNonMGScenes here without adding +1 because build indexes start at '0'
		for (int i = numNonMGScenes; i <= SceneManager.sceneCountInBuildSettings; i++) {  
			allMicrogameIndexes.Add (i);
		}
	}

	// fill a dictionary with instructions for every microgame for easy access
	// dictionary's key/value pairs set up as <sceneName, instructions>
	void initializeMicrogameInstructions(){
		microgameInstructions.Add (0, "Get ready!");
		microgameInstructions.Add (3, "Bag the Valuables!");
		microgameInstructions.Add (4, "Grab the Numbers!");
		microgameInstructions.Add (5, "Bounce the Eggs!");
		microgameInstructions.Add (6, "Illuminate the Ghost!");
	}

	// difficulty must be re-calculated during the start of every transition scene
	void calculateDifficulty(){
		// difficulty should be at least 1 at all times and derivce from a ratio between completed games and total unique games
		difficulty = (numTasksCompleted / totalTasks) + 1 +difficultyModifier;
		difficultyCalculated = true;
	}

	// choose build index of next microgame scene to load, add it to usedMicrogameIndexes, return it for further use
	int chooseRandomMicrogameIndex(){

		// if we have added all unique microgame indexes
		if (usedMicrogameIndexes.Count >= totalTasks) {
			// then empty the list and start fresh
			usedMicrogameIndexes.Clear ();
		}
		// randomly define a new index between the first MG build index and the last
		int index = Random.Range (numNonMGScenes, SceneManager.sceneCountInBuildSettings);

		// While we can find the randomly-chosen index already in the list of previously used indexes, choose a new one
		// EXPLANATION: C# Lists require "Lambda functions", represented by =>, meaning 'go-to'
		// boils down to "Declare existingMGIndex variable - does it exist when it is equal to index variable?"
		while (usedMicrogameIndexes.Contains(index) || index == pastMGBuildIndex) {
			index = Random.Range (numNonMGScenes, SceneManager.sceneCountInBuildSettings);
		}

		// Now that we have a unique MG index that has NOT already been used, add it to the usedMicrogameIndexes list
		usedMicrogameIndexes.Add (index);

		Debug.Log ("index: " + index + " ~ next scene: " + SceneManager.GetSceneByBuildIndex(index).buildIndex + " ~ total scenes: " + SceneManager.sceneCountInBuildSettings);
		return index;
	}

	// retrieve instructions for the next microgame from the dictionary it is stored in
	string retrieveNextMGInstructions(){

		string instructions;

		// get name of next scene as a string
//		string nextSceneName = SceneManager.GetSceneByBuildIndex (nextMGBuildIndex).name;
		int nextSceneIndex = 0;
		nextSceneIndex = nextMGBuildIndex;
		// instructions don't work unless the scene is actually built!! ^^^

		// try to retrieve the instructions, if that fails (will return false) then get default instructions
		if (! (microgameInstructions.TryGetValue( nextSceneIndex, out instructions) ) ){
			microgameInstructions.TryGetValue (0, out instructions);
		}
		return instructions;
	}
}
