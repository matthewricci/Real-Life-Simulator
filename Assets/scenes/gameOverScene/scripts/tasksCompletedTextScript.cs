using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tasksCompletedTextScript : MonoBehaviour {


	// String that holds numtask completed.
	string localNumTasksCompleted;

	public Text thisText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		localNumTasksCompleted = gameOverlordScript.numTasksCompleted.ToString ();
		thisText.text = localNumTasksCompleted;
	}
}
