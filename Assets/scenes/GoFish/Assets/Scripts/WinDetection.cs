using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class WinDetection : MonoBehaviour {

	public float timer;
	public float lightTimer;
	public bool won = false;
	public bool lost = false;
	public bool firstTimeThroughWin = true;
	public bool firstTimeThroughLost = true;

	public GameObject Player;

	public Light myLight;
	Color myColor;

	public AudioSource mainTheme;
	public AudioSource lossTheme;
	public AudioSource winTheme;

	public List<Animator> ticklesAnims = new List<Animator>();

	// This variable keeps track of how long this scene should be and communicates that to the watch:
	float totalTimeForWatch;

	void Awake() {
		timer = 15f;
		// This variable represents the total number of time it takes for the scene to pass:
		totalTimeForWatch = timer;
		// We change the public static variable for wristwatchTime to the duration for this scene.
		gameOverlordScript.wristWatchTime = totalTimeForWatch;
	}

	void Start(){
		lightTimer = 0.37f;
	}

	void Update(){

		if (won == true) {
			if (firstTimeThroughWin == true) {
				foreach (Animator currentAnimator in ticklesAnims) {
					currentAnimator.SetBool ("Won", true);
				}

				mainTheme.Stop ();
				winTheme.Play ();

				firstTimeThroughWin = false;
			}
			if (lightTimer <= 0f) {
				myColor = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
				myLight.color = myColor;
				lightTimer = 0.37f;
			}
			gameOverlordScript.didPlayerWin = true;
		} else if (timer <= 2f) {
			if (firstTimeThroughLost == true) {
				foreach (Animator currentAnimator in ticklesAnims) {
					currentAnimator.SetBool ("Lost", true);
				}

				mainTheme.Stop ();
				lossTheme.Play ();

				lost = true;

				myColor = new Color (0.2f,0.2f,0.8f);
				myLight.color = myColor;

				firstTimeThroughLost = false;
			}
			gameOverlordScript.didPlayerWin = false;
		}

		if (timer <= 0f) {
			timer = 10f;
			gameOverlordScript.microgameDone = true;
		}

		timer -= Time.deltaTime;
		lightTimer -= Time.deltaTime;
	}
}