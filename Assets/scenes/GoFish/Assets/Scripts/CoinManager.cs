using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

	// prefab to instantiate
	public GameObject myCoinPrefab; // assign in Inspector; only objects with a Coin script are allowed!

	public WinDetection winDetection;

	Vector3 RandomUnitSphere;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(winDetection.won==true){
				RandomUnitSphere = new Vector3 (Random.insideUnitSphere.x, Random.insideUnitSphere.y + 4.5f, Random.insideUnitSphere.z- 11f);
				Instantiate (myCoinPrefab, RandomUnitSphere, Random.rotation);
		}
	}
}