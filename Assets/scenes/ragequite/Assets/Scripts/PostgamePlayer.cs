using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class PostgamePlayer : MonoBehaviour {

	public Hand left;
	public Hand right;

	private IEnumerator coroutine;

	SteamVR_Controller.Device leftController { get { return left.controller; } }
	SteamVR_Controller.Device rightController { get { return right.controller; } }


	bool engaged; 	// determines whether or not postgame effects have engaged

	public RagequitManager gameManager;

	public GameObject fire;		// fire particlesystem container

	public GameObject hose;
	public ParticleSystem water;

	public AudioSource yell;	// winning SFX

	public AudioSource sigh;	// losing SFX

	public AudioSource firetruck;

	public AudioSource smokeAlarm;

	// Use this for initialization
	void Start () {
		engaged = false;
		fire.SetActive(false);		// ensures you don't see any fire until game is over
		hose.SetActive(false);
		water.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if ( !(engaged) && gameManager.gameOver){
			if (gameManager.gameWon) {	// player won
				fire.SetActive (true);	// start rendering fire
				hose.SetActive (true);  // start rendering water hose


				engaged = true;
				enabled = true;
				if (!(yell.isPlaying)) {	// if not already yelling
					//yell.Play ();			// start to yell
				}
				if (!(firetruck.isPlaying)) {
					firetruck.Play (44100);	// delay playing by 44100Hz AKA one second
				}
				if (!(smokeAlarm.isPlaying)) {
					smokeAlarm.Play ();
				}
			} else {					// else player lost
				gameManager.angerLevel = 0.0f;		// remove redness from screen
				gameManager.numberItemsBroken = 0;	// make sure screen stays clear
				if (!(sigh.isPlaying)) {	// if not already sighing
					sigh.Play ();			// start to sigh
				}
			}
			coroutine = LongVibration (2000);
			StartCoroutine (coroutine);
		}
		water.Play ();
	}

	// coroutine for vibrating controller after win
	IEnumerator LongVibration(float length){
		for (float i = 0; i < length; i += Time.deltaTime) {
			leftController.TriggerHapticPulse (500);
			rightController.TriggerHapticPulse (2000);
			yield return null;

		}
	}
}
