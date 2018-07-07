using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour {

	// This contains all of the music sources. It is populated at each update frame.
	ArrayList listOfSFXAS = new ArrayList();

	// Update is called once per frame
	void Update () {
		// This list is updated with every audiosource on the children of this object and the object itself.
		listOfSFXAS.AddRange (this.gameObject.GetComponentsInChildren<AudioSource> ());
		listOfSFXAS.AddRange (this.gameObject.GetComponents<AudioSource> ());

		// If sfxOn is true, let the sfx play. Otherwise, mute it.
		if (sfxToggle.sfxOn) {
			foreach (AudioSource AS in listOfSFXAS) {
				AS.mute = false;
			}
		} else {
			foreach (AudioSource AS in listOfSFXAS) {
				AS.mute = true;
			}
		}
	}
}
