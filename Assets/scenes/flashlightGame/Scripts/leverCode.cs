using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverCode : MonoBehaviour {


	GameObject flashLightGameAgent;

	// This is the variable which holds the GreenLever object which will replace the red lever.
	public GameObject greenLever;

	// AUDIO:
	// SFX Player for Lever.
	public AudioSource leverSFXPlayer;
	// SFX for when the lever switches.
	public AudioClip leverSwitchSound;


	void Start () {
		// Get the Game Agent for this scene.
		flashLightGameAgent = GameObject.Find ("flashlightGameAgentObject");	
	}



	void OnTriggerEnter (Collider other) {
		// If the "Hand" object enters the trigger:
		if (other.gameObject.tag == "hand") {

			// Set the SFX Player to play the Lever switch sound.
			leverSFXPlayer.clip = leverSwitchSound;
			// Play the lever switch sound:
			leverSFXPlayer.Play();


			Vector3 flipUpsideDownRotationEuler = new Vector3 (0f, 180f, 0f);

			// Create a green lever object in the same position as the redLever
			GameObject newGreenLever = Instantiate (greenLever, this.transform.position, this.transform.rotation);

			// Destroy this redLever object.
			Destroy (this.gameObject);
			 
			// Flip the greenLeverUpside down.
			newGreenLever.transform.Rotate (flipUpsideDownRotationEuler);
			// Tell the Game Agent that you won.
			flashLightGameAgent.GetComponent<flashlightGameAgent>().gameWon = true;
		
		
		}

	}
}
