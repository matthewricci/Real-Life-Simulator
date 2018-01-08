using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringEffect : MonoBehaviour {

	float timer;
	float origin_y;
	float amplitude;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		origin_y = transform.position.y;
		amplitude = 0.02f;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		transform.position = new Vector3 (transform.position.x, origin_y + (amplitude * Mathf.Sin(Time.time * 8f)), transform.position.z);
	}
}
