using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class BreakObject : MonoBehaviour {

	public Rigidbody rb;

	public bool brokenFlag;

	public AudioSource clang;

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
				brokenFlag = true;
				rb.isKinematic = false;
				gameManager.numberItemsBroken += 1;
				clang.Play ();
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
