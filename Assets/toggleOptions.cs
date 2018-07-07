using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is used to control the options menu.
public class toggleOptions : MonoBehaviour {


	//public GameObject[] optionsButtons;
	public GameObject optionButtonGroup;
	public GameObject openOptionsButton;
	public GameObject instructionsCanvas;


	bool optionsOpen = false;

	void Update () {
		if (optionsOpen) {
//			foreach (GameObject optionsGO in optionsButtons) {
//				optionsGO.SetActive (true);
//			}
			openOptionsButton.SetActive (false);
			optionButtonGroup.SetActive (true);
			instructionsCanvas.SetActive (false);
		} else if (!optionsOpen) {
//			foreach (GameObject optionsGO in optionsButtons) {
//				optionsGO.SetActive (false);
//			}
			openOptionsButton.SetActive (true);
			optionButtonGroup.SetActive (false);
			instructionsCanvas.SetActive (true);
		}
	}

	public void toggleOptionsMethod () {
		optionsOpen = !optionsOpen;
	}

}


