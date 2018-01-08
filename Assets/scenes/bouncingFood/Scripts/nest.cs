using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest : MonoBehaviour {

	// Audiosource for nest.
	public AudioSource nestSFXPlayer;
	// AudioClip for Egg Hitting Nest.
	public AudioClip eggHitsNestSFX;

	void OnCollisionEnter (Collision other) {
		

		if (other.gameObject.tag == "egg") {
			// Assign the proper audio clip.
			nestSFXPlayer.clip = eggHitsNestSFX;
			// Play victory sound FX.
			nestSFXPlayer.Play ();
			// Remove the egg.


		}


	}

}
