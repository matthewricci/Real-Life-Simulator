using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class BurglarPostgame : MonoBehaviour {

	public int x;

	public Transform player;
	public Transform convertible;

	public Hand left;
	public Hand right;

	private IEnumerator coroutine;

	SteamVR_Controller.Device leftController { get { return left.controller; } }
	SteamVR_Controller.Device rightController { get { return right.controller; } }


	bool engaged; 	// determines whether or not postgame effects have engaged
	bool driveFlag; // determines whether or not to start driving the convertible

	public BurglarManager gameManager;

	public AudioSource sigh;	// losing SFX

	public AudioSource sirens;

	public AudioSource celebrating;

	public AudioSource carAcceleration;

	public AudioSource victoryMusic;

	public AudioSource defeatMusic;

	// Use this for initialization
	void Start () {
		engaged = false;
		driveFlag = false;
	}

	// Update is called once per frame
	void Update () {
		if ( !(engaged) && gameManager.gameOver){
			engaged = true;

			if (!(sirens.isPlaying)) {
				sirens.Play ();
			}
		
		//	player.position = new Vector3 (18.70f, player.position.y, 1.14f);	// teleport player to position of loss scene

			if (gameManager.gameWon) {	// player won

				player.position = convertible.position;		// place player into the car
				driveFlag = true;							// start driving
				if (!(celebrating.isPlaying)) {
					celebrating.Play ();
				}
				if (!(carAcceleration.isPlaying)) {
					carAcceleration.Play ();
				} 
				if (!(victoryMusic.isPlaying)) {
					victoryMusic.Play();
				}

			} else {					// else player lost
				player.position = new Vector3 (18.70f, player.position.y, 1.14f);	// teleport player to position of loss scene
				// hide convertible so player doesn't see it when they lose
				convertible.gameObject.SetActive (false);

				if (!(sigh.isPlaying)) {	// if not already sighing
					sigh.Play ();			// start to sigh
				}

				if (!(defeatMusic.isPlaying)) {	// if not already sighing
					defeatMusic.Play ();			// start to sigh
				}
			}

			coroutine = LongVibration (700);
			StartCoroutine (coroutine);
		}

		driveCar (12.0f * Time.deltaTime);	// if the game ends and player won, start driving at the indicated speed
	}

	// coroutine for vibrating controller after win
	IEnumerator LongVibration(ushort length){
		for (float i = 0; i < length; i += Time.deltaTime) {
			leftController.TriggerHapticPulse (length);
			rightController.TriggerHapticPulse (length);
			yield return null;

		}
	}

	void driveCar(float distance){
		if (driveFlag) {
			convertible.position = new Vector3 (convertible.position.x + distance, convertible.position.y, convertible.position.z);
			player.position = new Vector3 (player.position.x + distance, player.position.y, player.position.z);
		}
	}
}
