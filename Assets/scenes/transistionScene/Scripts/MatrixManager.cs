using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatrixManager : MonoBehaviour {

	// used by the ChangeCanvas script to know when to start displaying Win/Loss outcome
	public static bool startDisplayingOutcome = false;

	// flag that determines whether or not to play one of Fred's comments
	public static bool fredShouldTalk = false;

	//UI Text Objects for Matrix
	public Text surface1;
	public Text surface2;
	public Text surface3;
	public Text surface4;

	// each is a collection of 4 hearts on an individual canvas
	//public GameObject heartsCollection1;
	//public GameObject heartsCollection2;
	//public GameObject heartsCollection3;
	//public GameObject heartsCollection4;

	// creates lists containing all hearts existing on a given canvas for later animation
	public List<Renderer> heartsList1 = new List<Renderer>{};
	public List<Renderer> heartsList2 = new List<Renderer>{};
	public List<Renderer> heartsList3 = new List<Renderer>{};
	public List<Renderer> heartsList4 = new List<Renderer>{};

	// creates lists containing all sound effects to play during the transition scene
	public GameObject positiveAudio;
	public GameObject negativeAudio;
	List<AudioSource> positive = new List<AudioSource>{};
	List<AudioSource> negative = new List<AudioSource>{};
	int audioIndex;


	//Wall Color Changing Variables
	public List<Renderer> wallRenderers = new List<Renderer>{};

	public Material whiteWall;
	public Material redWall;
	public Material greenWall;
	public Material lightBlueWall;
	public Material mediumBlueWall;
	public Material darkBlueWall;

	//timer
	float timer=2.5f;

	// group of flags that determine which information to display to the player during this instance of the transition room
	bool showWinOrLoss;		
	bool showDifficulty;
	bool showLives;
	bool showNumCompletedTasks;
	bool showInstructions;

	// flag reporting whether or not the four hearts representing "lives" have been enabled on-screen
	bool heartsActivated;

	bool subtractLife;
	int lostHeartIndex = 0;

	// Determine what transitions will need to be done this time
	void Start () {

		// flag determining whether or not to perform the subtracting-a-life animation
		subtractLife = !(gameOverlordScript.didPlayerWin);  // we subtract a life if the player did not win

		// always show lives and instructions
		showWinOrLoss = true;
		showLives = true;
		showNumCompletedTasks = true;
		showInstructions = true;

		// only show numCompletedTasks and whether or not player won or lost AFTER the first task has been completed
		if (gameOverlordScript.numTasksCompleted > 0) {
			showNumCompletedTasks = true;
			showWinOrLoss = true;
		} else {
			showNumCompletedTasks = false;
			showWinOrLoss = false;
		}

		// fred should only give feedback every OTHER scene, AFTER we have started displaying win/loss
		if (showWinOrLoss) {
			fredShouldTalk = !fredShouldTalk;	// flip fred's flag so he only talks every other scene
			//Debug.Log("fred: " + fredShouldTalk);
		}

//		// only show difficulty if it has changed since last microgame (current difficulty and past difficulty stored as public static vars in gameOverlordScript)
//		if (gameOverlordScript.pastDifficulty != gameOverlordScript.difficulty) {
//			showDifficulty = true;
//			gameOverlordScript.pastDifficulty = gameOverlordScript.difficulty;
//		} else {
//			showDifficulty = false;
//		}

		//heartsActivated = false;

		// hide hearts in-game until we're ready for them, fill heartLists with hearts on given canvases
		//heartsCollection1.SetActive (false);
		//heartsList1 = new List<Renderer>( heartsCollection1.GetComponentsInChildren<Renderer>() );
		//heartsCollection2.SetActive (false);
		//heartsList2 = new List<Renderer>( heartsCollection2.GetComponentsInChildren<Renderer>() );
		//heartsCollection3.SetActive (false);
		//heartsList3 = new List<Renderer>( heartsCollection3.GetComponentsInChildren<Renderer>() );
		//heartsCollection4.SetActive (false);
		//heartsList4 = new List<Renderer>( heartsCollection4.GetComponentsInChildren<Renderer>() );

		// color all hearts on all canvases according to number of lives left
		setupHearts ();

		positive = new List<AudioSource> (positiveAudio.GetComponentsInChildren<AudioSource> ());
		negative = new List<AudioSource> (negativeAudio.GetComponentsInChildren<AudioSource> ());
		audioIndex = Random.Range (0, 3);

		if (showWinOrLoss) {
			if (fredShouldTalk) {
				if (gameOverlordScript.didPlayerWin) {
					if (!(positive [audioIndex].isPlaying)) {
						positive [audioIndex].Play ();
					}
				} else {
					if (!(negative [audioIndex].isPlaying)) {
						positive [audioIndex].Play ();
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		// constantly update timer
		timer -= Time.deltaTime;

		// hacky solution for difficulty problem. waits for difficulty to be calculated
		// in gameOverlordScript (indicated by difficultyCalculated static flag)
		// this should only ever trigger once
		// HOPEFULLY this happens before showDifficulty is needed below
		if (gameOverlordScript.difficultyCalculated) {
			// only show difficulty if it has changed since last microgame (current difficulty and past difficulty stored as public static vars in gameOverlordScript)
			if (gameOverlordScript.pastDifficulty != gameOverlordScript.difficulty) {
				showDifficulty = true;
				gameOverlordScript.pastDifficulty = gameOverlordScript.difficulty;
			} else {
				showDifficulty = false;
			}
			gameOverlordScript.difficultyCalculated = false;
		}

		// if we are currently displaying win/loss
		if (showWinOrLoss){
			
			// constantly update the text
			if (gameOverlordScript.didPlayerWin) {
				for (int i = 0; i < 6; i++) {
					wallRenderers[i].material = greenWall;
				}
				surface1.text = "Win";
				surface2.text = "Win";	
				surface3.text = "Win";	
				surface4.text = "Win";	

//				if (fredShouldTalk && (!( positive[audioIndex].isPlaying)) ){
//					positive[audioIndex].Play ();
//				}

			} else {
				for (int i = 0; i < 6; i++) {
					wallRenderers[i].material = redWall;
				}
				surface1.text = "Lost";
				surface2.text = "Lost";	
				surface3.text = "Lost";	
				surface4.text = "Lost";

				// perform any fading heart animations that still need to occur
				fadeHeart (timer);	// animation is based on timer

//				if (fredShouldTalk && (!( negative[audioIndex].isPlaying)) ){
//					negative[audioIndex].Play ();
//				}
			}


			// if we are done showing difficulty, set timer back to default 4s and move to next piece of info
			if (timer <= 0.0f) {
				showWinOrLoss = false;
				timer = 2.5f;

				//set wall to difficulty appropriate color
				if (gameOverlordScript.difficulty >= 4) {
					for (int i = 0; i < 6; i++) {
						wallRenderers[i].material = darkBlueWall;
					}
				}
				else if (gameOverlordScript.difficulty == 3) {
					for (int i = 0; i < 6; i++) {
						wallRenderers[i].material = mediumBlueWall;
					}
				}
				else if (gameOverlordScript.difficulty == 2) {
					for (int i = 0; i < 6; i++) {
						wallRenderers[i].material = lightBlueWall;
					}
				}
				else if (gameOverlordScript.difficulty <= 1) {
					for (int i = 0; i < 6; i++) {
						wallRenderers[i].material = whiteWall;
					}
				}
			}
		}
		// if we are currently displaying the difficulty
		else if (showDifficulty) {

			// constantly update the text
			surface1.text = "Difficulty: " + gameOverlordScript.difficulty;
			surface2.text = "Difficulty: " + gameOverlordScript.difficulty;
			surface3.text = "Difficulty: " + gameOverlordScript.difficulty;
			surface4.text = "Difficulty: " + gameOverlordScript.difficulty;

			// if we are done showing difficulty, set timer back to default 4s and move to next piece of info
			if (timer <= 0.0f) {
				showDifficulty = false;
				timer = 2.5f;
			}
		}
		// if we are currently displaying the lives
		else if (showLives){

			// constantly update the text
			surface1.text = "Lives: " + gameOverlordScript.lives;
			surface2.text = "Lives: " + gameOverlordScript.lives;
			surface3.text = "Lives: " + gameOverlordScript.lives;
			surface4.text = "Lives: " + gameOverlordScript.lives;

			// if we haven't yet activated hearts, activate them
			//if ( ! (heartsActivated) ){

				//heartsCollection1.SetActive (true);
				//heartsCollection2.SetActive (true);
				//heartsCollection3.SetActive (true);
				//heartsCollection4.SetActive (true);

				//heartsActivated = true;
			//}

			// if we are done showing lives, move to next piece of info
			if (timer <= 0.0f) {
				showLives = false;
				//heartsCollection1.SetActive (false);
				//heartsCollection2.SetActive (false);
				//heartsCollection3.SetActive (false);
				//heartsCollection4.SetActive (false);
				timer = 2.5f;
			}
		}
		// if we are currently displaying number of completed tasks
		else if (showNumCompletedTasks){
			
			// constantly update the text
			surface1.text = "Tasks completed: " + gameOverlordScript.numTasksCompleted;
			surface2.text = "Tasks completed: " + gameOverlordScript.numTasksCompleted;
			surface3.text = "Tasks completed: " + gameOverlordScript.numTasksCompleted;
			surface4.text = "Tasks completed: " + gameOverlordScript.numTasksCompleted;

			// if we are done showing lives, move to next piece of info
			if (timer <= 0.0f) {
				showNumCompletedTasks = false;
				timer = 2.5f;
			}
		}
		// if we are currently displaying instructions
		else if (showInstructions){
			
			// constantly update the text
			surface1.text = gameOverlordScript.nextMGInstructions;
			surface2.text = gameOverlordScript.nextMGInstructions;
			surface3.text = gameOverlordScript.nextMGInstructions;
			surface4.text = gameOverlordScript.nextMGInstructions;

			// if we are done showing lives, move to next piece of info
			if (timer <= 0.0f) {
				showInstructions = false;
				timer = 4.0f;	// is this necessary?
				SceneManager.LoadScene (gameOverlordScript.nextMGBuildIndex);
			}
		}

	}

	// runs through logic that colors all hearts on all canvases that need to be colored red (default is faded white)
	void setupHearts(){
		for (int i = 0; i < heartsList1.Count; i++) {	// Count is the same across all four lists
			if (i < gameOverlordScript.lives) {
				Debug.Log (heartsList1 [i].material.color);
				heartsList1 [i].material.color = Color.red;
				heartsList2 [i].material.color = Color.red;
				heartsList3 [i].material.color = Color.red;
				heartsList4 [i].material.color = Color.red;
			}
		}
		if (subtractLife && gameOverlordScript.lives < heartsList1.Count) {	// Count is the same across all fours lists
			lostHeartIndex = gameOverlordScript.lives;	// if there are X lives left and the player lost, the heart at index X must fade to white
			heartsList1 [lostHeartIndex].material.color = Color.red;	// set this heart as red, will fade later in Update()
			heartsList2 [lostHeartIndex].material.color = Color.red;
			heartsList3 [lostHeartIndex].material.color = Color.red;
			heartsList4 [lostHeartIndex].material.color = Color.red;
		}
	}

	// if a life is being subtracted in this instance, this performs the fading animation across all canvases
	void fadeHeart(float timer){
		if (subtractLife && gameOverlordScript.lives < heartsList1.Count) {
			Debug.Log ("lost hearts index: " + lostHeartIndex);
			heartsList1[lostHeartIndex].material.color = Color.Lerp (Color.white, Color.red, (timer - 1.0f) );
			heartsList2[lostHeartIndex].material.color = Color.Lerp (Color.white, Color.red, (timer - 1.0f) );
			heartsList3[lostHeartIndex].material.color = Color.Lerp (Color.white, Color.red, (timer - 1.0f) );
			heartsList4[lostHeartIndex].material.color = Color.Lerp (Color.white, Color.red, (timer - 1.0f) );
		}
	}
}
