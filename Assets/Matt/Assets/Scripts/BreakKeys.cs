using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class BreakKeys : MonoBehaviour {

	public Rigidbody rb;

	bool brokenFlag;

	public RagequitManager gameManager;  // declare RagequitManager script 

	// Use this for initialization
	void Start () {
		brokenFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "weapon") {
			if (! (brokenFlag) ){
				Debug.Log ("breaked");
				brokenFlag = true;
				rb.isKinematic = false;
				gameManager.numberItemsBroken += 1;
			}
		}
	}

//	void HandHoverUpdate(Hand hand) {
//
//		// enable the rigidbody so screen falls
//		if (!(brokenFlag)){
//			
//			// button has been broken
//			brokenFlag = true;
//
//			rb.isKinematic = false;
//			//rb.useGravity = true;
//
//			// item has been "broken", add one to the wincheck	
//			gameManager.numberItemsBroken += 1;
//			//gameManager.newBrokenItems += 1;
//		}
//			
//		rb.AddExplosionForce (50f, hand.transform.position, 2f);
//
//	}
}
