using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidSlider : MonoBehaviour {
	public int numTaskTest;
	// Use this for initialization
	void Start () {
		//gameOverlordScript.numTasksCompleted = numTaskTest;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOverlordScript.numTasksCompleted >= 20 && gameObject.transform.localScale.y >= 0) {
			gameObject.transform.localScale = new Vector3 (
				gameObject.transform.localScale.x,
				gameObject.transform.localScale.y - 0.0025f,
				gameObject.transform.localScale.z);
			gameObject.transform.position = new Vector3 (
				gameObject.transform.position.x,
				gameObject.transform.position.y + 0.0039f,
				gameObject.transform.position.z);
		} else if (gameOverlordScript.numTasksCompleted >= 15 && gameObject.transform.localScale.y >= 0.2763f) {
			gameObject.transform.localScale = new Vector3 (
				gameObject.transform.localScale.x,
				gameObject.transform.localScale.y - 0.0025f,
				gameObject.transform.localScale.z);
			gameObject.transform.position = new Vector3 (
				gameObject.transform.position.x,
				gameObject.transform.position.y + 0.0039f,
				gameObject.transform.position.z);
		} else if (gameOverlordScript.numTasksCompleted >= 10 && gameObject.transform.localScale.y >= 0.4431f) {
			gameObject.transform.localScale = new Vector3 (
				gameObject.transform.localScale.x,
				gameObject.transform.localScale.y - 0.0025f,
				gameObject.transform.localScale.z);
			gameObject.transform.position = new Vector3 (
				gameObject.transform.position.x,
				gameObject.transform.position.y + 0.0039f,
				gameObject.transform.position.z);
		} else if (gameOverlordScript.numTasksCompleted >= 5 && gameObject.transform.localScale.y >= 0.5855f) {
			gameObject.transform.localScale = new Vector3 (
				gameObject.transform.localScale.x,
				gameObject.transform.localScale.y - 0.0025f,
				gameObject.transform.localScale.z);
			gameObject.transform.position = new Vector3 (
				gameObject.transform.position.x,
				gameObject.transform.position.y + 0.0039f,
				gameObject.transform.position.z);
		} else if (gameOverlordScript.numTasksCompleted >= 0 && gameObject.transform.localScale.y >= 0.727f) {
			gameObject.transform.localScale = new Vector3 (
				gameObject.transform.localScale.x,
				gameObject.transform.localScale.y - 0.0025f,
				gameObject.transform.localScale.z);
			gameObject.transform.position = new Vector3 (
				gameObject.transform.position.x,
				gameObject.transform.position.y + 0.0039f,
				gameObject.transform.position.z);
		}
		//0.2502
		//0.4170
		//0.5594
		//0.7009
		//1

		//0.0261
	}
}
