using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashPoliceLights : MonoBehaviour {

	public Light blueLight;
	public Light redLight;
	public float timer;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= 0.25f) {
			blueLight.enabled = !blueLight.enabled;
			redLight.enabled = !redLight.enabled;
			timer = 0.0f;
		}
	}
}
