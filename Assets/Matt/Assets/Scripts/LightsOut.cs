using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOut : MonoBehaviour {

	public Light lightbulb;
	public BreakObject parentObject;

	bool putOut;

	// Use this for initialization
	void Start () {
		putOut = false; 
	}
	
	// Update is called once per frame
	void Update () {
		if (!(putOut) && parentObject.brokenFlag) {
			putOut = true;
			lightbulb.enabled = false;
		}
	}
}
