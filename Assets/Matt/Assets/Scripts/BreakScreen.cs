using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class BreakScreen : MonoBehaviour {

	public Component screenGlass;
	public Light screenLight;
	public Rigidbody rb;
	public Material cracked;

	public Rigidbody base_rb;
	public Rigidbody support_rb;

	public RagequitManager gameManager;  // declare RagequitManager script

	public ParticleSystem flare;

	bool brokenFlag;

	public AudioSource clang;
	public AudioSource glass_break;

	// Use this for initialization
	void Start () {
		brokenFlag = false;
		glass_break.time = 0.7f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "weapon") {
			if (!(brokenFlag)) {
				// "break" the screen
				screenGlass.GetComponent<MeshRenderer> ().material = cracked;
				screenLight.enabled = false;
				brokenFlag = true;

				// set off flare
				flare.Play();

				// sound the weapon, sound the broken glass
				clang.Play ();
				glass_break.Play ();

				// item has been "broken", update the gamemanager
				gameManager.numberItemsBroken += 1;
				//gameManager.newBrokenItems += 1;

				// enable the rigidbody so screen falls
				rb.isKinematic = false;
				rb.useGravity = true;
				support_rb.isKinematic = false;
				base_rb.isKinematic = false;
				// add a little force *** THIS ISN'T WORKING
				Vector3 forcePosition = transform.position - other.transform.position;
				// rb.AddForceAtPosition (forcePosition, hand.transform.position, ForceMode.Impulse);  // i need a more accurate Vector3 for the force
				rb.AddForceAtPosition (forcePosition * 500f, other.transform.position);
			}
		}
	}

//	void HandHoverUpdate(Hand hand) {
//		// determine controller connect to hand
//		//SteamVR_Controller.Device myController = hand.controller;
//
//		// is the controller active?
//		//if (myController != null) {
//
//		// is a fist made? (is the grip button held down?)
//		//if (myController.GetPress (Valve.VR.EVRButtonId.k_EButton_Grip) && !(brokenFlag)) {
//		if (!(brokenFlag)) {
//			// "break" the screen
//			screenGlass.GetComponent<MeshRenderer> ().material = cracked;
//			screenLight.enabled = false;
//			brokenFlag = true;
//
//			// set off flare
//			flare.Play();
//
//			// item has been "broken", update the gamemanager
//			gameManager.numberItemsBroken += 1;
//			//gameManager.newBrokenItems += 1;
//
//			// enable the rigidbody so screen falls
//			rb.isKinematic = false;
//			rb.useGravity = true;
//			support_rb.isKinematic = false;
//			base_rb.isKinematic = false;
//			// add a little force *** THIS ISN'T WORKING
//			Vector3 forcePosition = transform.position - hand.transform.position;
//			// rb.AddForceAtPosition (forcePosition, hand.transform.position, ForceMode.Impulse);  // i need a more accurate Vector3 for the force
//			rb.AddForceAtPosition (forcePosition * 500f, hand.transform.position);
//		}
//
//	}
}
