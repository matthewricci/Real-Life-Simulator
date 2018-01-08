﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {	
			// ...then reload the current scene, by finding out the current scene and reloading it.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
