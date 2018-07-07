using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This script goes on the music toggle in the start menu room.

public class musicToggle : MonoBehaviour {

	// This bool controls whether music is on.
	public static bool musicOn = true;

	public Text musicText;

	public void changeMusic () {
		musicOn = !musicOn;
		Debug.Log ("music changing");


		if (musicOn) {
			musicText.text = "Music: On";
		} else {
			musicText.text = "Music: Off";
		}

	}
}
