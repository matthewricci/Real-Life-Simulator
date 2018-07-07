using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class realAlToggle : MonoBehaviour {

	// This bool controls whether music is on.
	public static bool realAlCommentaryOn = true;

	public Text realAlToggleText;

	public void changeRealAlCommentary () {
		Debug.Log ("Changing Real Al");
		realAlCommentaryOn = !realAlCommentaryOn;


		if (realAlCommentaryOn) {
			realAlToggleText.text = "Real Al Commentary: On";
		} else {
			realAlToggleText.text = "Real Al Commentary: Off";
		}
	}
}
