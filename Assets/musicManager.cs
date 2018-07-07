using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is put on all GO's with AudioSources. It uses the static bool "musicOn" to turn itself on or off.
public class musicManager : MonoBehaviour {

	// This contains all of the music sources. It is populated at each update frame.
	ArrayList listOfMusicAS = new ArrayList();

	// Update is called once per frame
	void Update () {
		// This list is updated with every audiosource on the children of this object and the object itself.
		listOfMusicAS.AddRange (this.gameObject.GetComponentsInChildren<AudioSource> ());
		listOfMusicAS.AddRange (this.gameObject.GetComponents<AudioSource> ());

		// If musicOn is true, let the music play. Otherwise, mute it.
		if (musicToggle.musicOn) {
			foreach (AudioSource AS in listOfMusicAS) {
				AS.mute = false;
			}
		} else {
			foreach (AudioSource AS in listOfMusicAS) {
				AS.mute = true;
			}
		}
	}
	

}
