using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverSceneManager : MonoBehaviour {


	// Set up transforms for room's walls, ceiling, and floor.
	public Transform floor;
	public Transform ceiling;
	public Transform rightWall;
	public Transform leftWall;
	public Transform backWall;
	public Transform frontWall;

	// Set up audiosources and SFX.
	public AudioSource gameOverAgentSFXPlayer;
	public AudioClip intialGameOverSound;
	public AudioClip tasksCompletedSFX;
	public AudioClip finalGameOverSFX;

	// Set up Materials.
	public Material firstGameOverMat;
	public Material tasksCompletedMat;
	public Material PyramidMat;

	// Set up renderers.
	Renderer floorRend;
	Renderer ceilingRend;
	Renderer rightWallRend;
	Renderer leftWallRend;
	Renderer backWallRend;
	Renderer frontWallRend;

	// Set up a variable to keep track of how much time has gone by in the scene.
	float sceneTimer = 0;


	// Set up the float variables which designate which phase of the game over scene we are in.
	public float secondsUntilTasksPhase = 5;
	public float secondsUntilSecondGameOverPhase = 10;
	// This is the longest value.
	public float secondsUntilSendToStartMenu = 10;

	// This is the variable which contains the total amount of time it takes for this scene to pass.
	float totalTimeForWatch;





	// Gatekeeper variables for various phases in GameOverScene.
	bool taskCompletedPhaseDone = false;
	bool secondGameOverPhaseDone = false;

	// Canvas which has all of our tasksCompleted text objects.
	public Canvas tasksCompletedCanvas;
	public GameObject myCanvas;

	void Awake () {
		// This variable represents the total number of time it takes for the scene to pass:
		totalTimeForWatch = secondsUntilSendToStartMenu;
		// We change the public static variable for wristwatchTime to the duration fo this scene.
		gameOverlordScript.wristWatchTime = totalTimeForWatch;
	}


	// Use this for initialization
	void Start () {
		// Set the watch time to whatever the total time of the scene is.
		totalTimeForWatch = secondsUntilSendToStartMenu;

		// Get all renderers.
		rightWallRend = rightWall.GetComponent<Renderer> ();
		leftWallRend = leftWall.GetComponent<Renderer> ();
		frontWallRend = frontWall.GetComponent<Renderer> ();
		backWallRend = backWall.GetComponent<Renderer> ();
		floorRend = floor.GetComponent<Renderer> ();
		ceilingRend = ceiling.GetComponent<Renderer> ();

		// Turn renderers on.
		rightWallRend.enabled = true;
		leftWallRend.enabled = true;
		frontWallRend.enabled = true;
		backWallRend.enabled = true;
		floorRend.enabled = true;
		ceilingRend.enabled = true;


		// Paint the walls. 
		PaintVerticalWalls (firstGameOverMat, firstGameOverMat);
		// Set up the gameOverAgent sound.
		gameOverAgentSFXPlayer.clip = intialGameOverSound;
		// Play the gameOverAgent sound.
		gameOverAgentSFXPlayer.Play();
		// The tasks completed canvas is not active to begin with.
		tasksCompletedCanvas.enabled = false;
		myCanvas.SetActive (false);



	}




	
	// Update is called once per frame
	void Update () {
		// Keep track of how much time has gone by in the scene.
		sceneTimer += Time.deltaTime;


		// THIS SEGMENT OF CODE EXECUTES DEPENDING ON HOW MUCH TIME HAS GONE BY. The last phases are listed first and proceed backwards from there.

		// If enough time has gone by that we are in the last phase,
		if (sceneTimer >= secondsUntilSendToStartMenu) {
			// Reset the public statics:
			gameOverlordScript.lives = 4;
			gameOverlordScript.difficulty = 0;
			//gameOverlordScript.numTasksCompleted = 0;


			// If time is up, send to Start Menu.
			SceneManager.LoadScene ("Start Menu");

			// If enough time has gone by that we are in the second to last phase,
		} else if (sceneTimer >= secondsUntilSecondGameOverPhase && secondGameOverPhaseDone == false) {
			
//			// Turn off canvas:
//			tasksCompletedCanvas.enabled = false;
//			// Play this SFX:
//			gameOverAgentSFXPlayer.PlayOneShot (finalGameOverSFX);
//			// Shut the gate on this section:
//			secondGameOverPhaseDone = true;

		} else if (sceneTimer >= secondsUntilTasksPhase && taskCompletedPhaseDone == false ) {
			// Turn on canvas:
			tasksCompletedCanvas.enabled = true;
			myCanvas.SetActive (true);
			// Paint the walls with tasksCompletedMat, 
			PaintVerticalWalls (tasksCompletedMat, PyramidMat);
			// Play the tasksCompletedSFX.
			gameOverAgentSFXPlayer.PlayOneShot (tasksCompletedSFX);
			// Play
			taskCompletedPhaseDone = true;


		}
		

	}

	// Use this function to paint the front, back, right, and left wall. 
	void PaintVerticalWalls (Material inputMaterial1, Material inputMaterial2) {
		rightWallRend.sharedMaterial = inputMaterial2;
		leftWallRend.sharedMaterial = inputMaterial2;
		frontWallRend.sharedMaterial = inputMaterial1;
		backWallRend.sharedMaterial = inputMaterial1;

	}
}
