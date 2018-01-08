using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wristWatchCountdown : MonoBehaviour {

	// Get the watch's value for the scene from the GameOverlord.
	float totalWatchTime;

	// Variable that counts down the amount of time in the scene.
	float currentTime;

	// This is the variable that will convert the amount of time left to a percentage the image
	// can use.
	float percentageOfTime;

	// This is the actual watchface.
	public Image watchFace;

	// SFX Player for Watch Buzzes.
	public AudioSource watcherSFXPlayer;

	public AudioSource watchTickingSFXPlayer;

	// SFX which plays when player is almost out of time.
	public AudioClip timeOutBuzzer;

	bool buzzerPlayed;
	// Use this for initialization

	void Awake () {
		
	}

	void Start () {
		// Set the watch time to the public static variable in GameOverLord's wrist watch time variable.
		totalWatchTime = gameOverlordScript.wristWatchTime;

		// Set the current time to TotalWatchTime.
		currentTime = totalWatchTime;

	}
	
	// Update is called once per frame
	void Update () {
		// Count down the the current time.
		currentTime = currentTime - Time.deltaTime;
		// Calculate the percentage of time left.
		percentageOfTime = currentTime / totalWatchTime;
		// Change the watch face accordingly.
		watchFace.fillAmount = percentageOfTime;
		// Change the color of the watch depending on how much time you have.
		watchFace.color = Color.Lerp (Color.red, Color.green, percentageOfTime);

		if (percentageOfTime <= .1 && !buzzerPlayed) {
			// Mute the ticking.
			watchTickingSFXPlayer.mute = true;
			// Assign proper SFX.
			watcherSFXPlayer.clip = timeOutBuzzer;
			// Play time out buzzer.
			watcherSFXPlayer.Play();
			// Turn off buzzer.
			buzzerPlayed = true;
		}

	}
}
