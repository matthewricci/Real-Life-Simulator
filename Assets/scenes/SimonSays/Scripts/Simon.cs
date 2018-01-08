using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : MonoBehaviour {

	public int listCounter = 6;
	public int timer = 10;
	public float timeBetweenWords;
	public List<int> words = new List<int> ();

	// Use this for initialization
	void Start () {
		timeBetweenWords = (1f / gameOverlordScript.difficulty);

		while (listCounter > 0) {
			words.Add (Random.Range (0, 6));
			listCounter -= 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer -= 1;
		if (timeBetweenWords > 0f) {
		}


	}
}
