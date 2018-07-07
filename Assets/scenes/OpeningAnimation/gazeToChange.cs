using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazeToChange : MonoBehaviour {

	Renderer videoTextureRenderer;

	public Material humansOnlyMaterial;

	public Camera VRCamera;

	void Start () {
		videoTextureRenderer = GetComponent<Renderer> ();

		videoTextureRenderer.enabled = true;
	}


	// Update is called once per frame
	void Update () {

		// first, get vector from camera toward the button
		Vector3 fromCameraToButton = transform.position - VRCamera.transform.position;

		// get the angle between my look direction vs. vector toward the button
		float distanceDegrees = Vector3.Angle( VRCamera.transform.forward, fromCameraToButton.normalized );

		// is that angle within our range? if so, then do stuff
		if( distanceDegrees < 20f ) {

			// STEP 2: fire a raycast to see if there is anything between the camera and this button
			// ("is anything occluding the field of view?")

			// construct Ray object
			Ray ray = new Ray( VRCamera.transform.position, fromCameraToButton.normalized );

			// determine how far the raycast should go
			float maxRayDistance = 50f;

			// construct a RaycastHit object
			RaycastHit rayHit = new RaycastHit();

			// debug: visualize the raycast in the Scene view
			Debug.DrawRay( ray.origin, ray.direction * maxRayDistance, Color.yellow );

			// actually shoot the raycast now
			if( Physics.Raycast( ray, out rayHit, maxRayDistance ) ) {
				// did this raycast actually hit the object?
				if( rayHit.transform == this.transform) {
					
				}
				videoTextureRenderer.sharedMaterial = humansOnlyMaterial;
				Debug.Log ("Raycast worked.");
			} // end Physics.Raycast

		} // end distanceDegrees check
	}
}
