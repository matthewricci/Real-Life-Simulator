using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playRadio : MonoBehaviour {

	public BreakObject radioManager;

	public AudioSource radioSounds;

	// Use this for initialization
	void Start () {
		radioSounds.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (radioManager.brokenFlag) {
			radioSounds.Stop ();
		}
	}
}
