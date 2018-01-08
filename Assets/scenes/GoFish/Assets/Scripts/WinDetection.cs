using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class WinDetection : MonoBehaviour {

	public float timer;
	public bool won = false;
	public bool lost = false;
	public bool firstTimeThroughWin = true;
	public bool firstTimeThroughLost = true;
	public GameObject Player;
	public Rigidbody signRigidbody;
	public AudioSource slotWin;
	public AudioSource slotWin1;
	public AudioSource evilLaugh;
	public Light sun;

	// This variable keeps track of how long this scene should be and communicates that to the watch:
	float totalTimeForWatch;

	void Awake() {
		timer = 10f;
		// This variable represents the total number of time it takes for the scene to pass:
		totalTimeForWatch = timer;
		// We change the public static variable for wristwatchTime to the duration for this scene.
		gameOverlordScript.wristWatchTime = totalTimeForWatch;
	}

	void Start(){
		
	}

	void Update(){

		if (won == true) {
			if (firstTimeThroughWin == true) {
				sun.intensity = 0.8f;
				slotWin.Play ();
				slotWin1.Play ();
				Player.transform.position = new Vector3 (0f, 0f, -11.32f);
				firstTimeThroughWin = false;
			}
			gameOverlordScript.didPlayerWin = true;
		} else if (timer <= 2f) {
			if (firstTimeThroughLost == true) {
				lost = true;
				signRigidbody.useGravity = true;
				sun.intensity = 0.8f;
				Player.transform.position = new Vector3 (71.4f, 0f, -113f);
				Player.transform.eulerAngles = new Vector3 (Player.transform.rotation.eulerAngles.x, 180f ,Player.transform.rotation.eulerAngles.z);
				evilLaugh.Play ();
				firstTimeThroughLost = false;
			}
			gameOverlordScript.didPlayerWin = false;
		}

		if (timer <= 0f) {
			timer = 10f;
			gameOverlordScript.microgameDone = true;
		}

		timer -= Time.deltaTime;
	}
}