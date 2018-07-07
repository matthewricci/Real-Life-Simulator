using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class realAlManager : MonoBehaviour {

	// This contains all of the real Al Audio sources. It is populated at each update frame.
	ArrayList listOfRealAlAS = new ArrayList();

	// Update is called once per frame
	void Update () {
		// This list is updated with every audiosource on the children of this object and the object itself.
		listOfRealAlAS.AddRange (this.gameObject.GetComponentsInChildren<AudioSource> ());
		listOfRealAlAS.AddRange (this.gameObject.GetComponents<AudioSource> ());

		// If sfxOn is true, let the sfx play. Otherwise, mute it.
		if (realAlToggle.realAlCommentaryOn) {
			foreach (AudioSource AS in listOfRealAlAS) {
				AS.mute = false;
			}
		} else {
			foreach (AudioSource AS in listOfRealAlAS) {
				AS.mute = true;
			}
		}
	}
}
