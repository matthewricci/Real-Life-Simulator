using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem; // we need this to talk to Hands (for SteamVR objects)
using UnityEngine.SceneManagement;

// LARGELY OBSOLETE, SWITCHED TO GAZE SYSTEM.

// put this script on a Hand; talk to the Hand to get more controller data
public class enterButtonSelection : MonoBehaviour {

	// Get access to your hand.
	Hand myHand;

	// Get your controller from the Steam object.
	SteamVR_Controller.Device myController { get { return myHand.controller; } }

	// Assign the startMenuGameAgent.
	public GameObject startMenuGameAgentObject;

	// This variable holds the startMenuGameAgent from the startMenuGameAgentObject.
	startMenuGameAgent theSelectedStartMenuGameAgent;

	// Use this for initialization
	void Start () {
		// Get the hand component.
		myHand = GetComponent<Hand>();
		// Get the startMenuGameAgent script from the startMenuGameAgentObject
		theSelectedStartMenuGameAgent = startMenuGameAgentObject.GetComponent<startMenuGameAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Create a new ray that shoots out from the front of this hand.
		Ray rayFromHand = new Ray( this.gameObject.transform.position, this.gameObject.transform.forward );
		// This variable holds what the ray hits.
		RaycastHit rayHit = new RaycastHit();
		// This is the length of the ray.
		float maxRayDistance = 3f;
		// This drays the ray so that it is visible to us.
		Debug.DrawRay( rayFromHand.origin, rayFromHand.direction * maxRayDistance, Color.magenta );

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("The mouse is being pressed.");
		}


		// If the raycast hits anything,
		if (Physics.Raycast (rayFromHand, out rayHit, maxRayDistance)) {

			// If the name of the gameObject we hit is "enterButton"
			if (rayHit.collider.gameObject.name == "enterButton") {
				Debug.Log ("Ray hitting.");

				// If the controller is connected
				if (myController != null) { 
					// And the button is pressed,
					if (myController.GetHairTriggerDown ()) {
						// Run the "Press Enter Button" function.
						theSelectedStartMenuGameAgent.PressEnterButton ();
					}
				} else if (Input.GetMouseButtonDown (0)) {
					// Run the "Press Enter Button" function.
					theSelectedStartMenuGameAgent.PressEnterButton ();
					Debug.Log ("Pressing Enter Button with Mouse.");
				}
			}

		}
			
	}


}
