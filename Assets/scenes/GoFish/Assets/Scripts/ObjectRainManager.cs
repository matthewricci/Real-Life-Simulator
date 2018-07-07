using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRainManager : MonoBehaviour {

	// prefab to instantiate
	public List<GameObject> myNumberPrefabs = new List<GameObject>();

	public GameObject myRain;

	public WinDetection winDetection;

	public List<GameObject> myClouds = new List<GameObject>();

	Vector3 RandomUnitSphere;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (winDetection.won == true) {
			RandomUnitSphere = new Vector3 (Random.insideUnitSphere.x, Random.insideUnitSphere.y + 4.5f, Random.insideUnitSphere.z);
			Instantiate (myNumberPrefabs [Random.Range (0, 9)], RandomUnitSphere, Random.rotation);
			foreach (GameObject currentCloud in myClouds) {
				currentCloud.SetActive (true);
			}
		} else if (winDetection.lost == true) {
			for (int i = 0; i <= 5; i++) {
				RandomUnitSphere = new Vector3 (Random.insideUnitSphere.x*1.5f, Random.insideUnitSphere.y + 4.5f, Random.insideUnitSphere.z*1.5f);
				Instantiate (myRain, RandomUnitSphere, Random.rotation);
			}
			foreach (GameObject currentCloud in myClouds) {
				currentCloud.SetActive (true);
			}
		}
	}
}