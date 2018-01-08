using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour
{

	public GameObject thisCameraParent;
	public Camera thisCamera;
	public Text thisText;

	void Start ()
	{
		thisCamera = thisCameraParent.GetComponent<Camera> ();
		thisText = this.gameObject.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update ()
	{
	
		// STEP 1: is this object / button within the vision cone of the Main Camera?

		// first, get vector from camera toward the button
		Vector3 fromCameraToButton = transform.position - thisCamera.transform.position;
		// get the angle between my look direction vs. vector toward the button
		float distanceDegrees = Vector3.Angle (thisCamera.transform.forward, fromCameraToButton.normalized);
		// is that angle within our range? if so, then do stuff
		if (distanceDegrees < 20f) {

			// STEP 2: fire a raycast to see if there is anything between the camera and this button
			// ("is anything occluding the field of view?")

			// construct Ray object
			Ray ray = new Ray (thisCamera.transform.position, fromCameraToButton.normalized);

			// determine how far the raycast should go
			float maxRayDistance = 50f;

			// construct a RaycastHit object
			RaycastHit rayHit = new RaycastHit ();

			// debug: visualize the raycast in the Scene view
			Debug.DrawRay (ray.origin, ray.direction * maxRayDistance, Color.yellow);

			// actually shoot the raycast now
			if (Physics.Raycast (ray, out rayHit, maxRayDistance)) {
				// did this raycast actually hit the object?
				if (rayHit.collider==this.gameObject.GetComponent<Collider>()) {
					// if so, change text color
					thisText.color = Color.cyan;
				}
			} // end Physics.Raycast

		} else {
		thisText.color = Color.black;
		}

	}
	// end Update
}