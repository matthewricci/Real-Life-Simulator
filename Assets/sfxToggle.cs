using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sfxToggle : MonoBehaviour {

	// This bool controls whether music is on.
	public static bool sfxOn = true;

	public Text sfxText;

	public void changeSFX () {

		Debug.Log ("Changing SFX");
		sfxOn = !sfxOn;
		Debug.Log ("music changing");

		if (sfxOn) {
			sfxText.text = "SFX: On";
		} else {
			sfxText.text = "SFX: Off";
		}
	}
	

}
