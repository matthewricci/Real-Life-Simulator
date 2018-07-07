using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class seekPlayer : MonoBehaviour {

	public GameObject playerHand;
	public GameObject fallback;

	public bulletHellManager manager;


	// Use this for initialization
	void Start () {
		if (fallback.activeSelf) {
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fallback.activeSelf) {
			transform.LookAt (fallback.transform);
			transform.position = Vector3.MoveTowards (transform.position, fallback.transform.position, 2.0f * Time.deltaTime);
		} else {
			transform.LookAt (playerHand.transform);
			transform.position = Vector3.MoveTowards (transform.position, playerHand.transform.position, 2.0f * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "bulletHellSphere") {
			manager.hasBeenHit = true;
		}
	}
}
