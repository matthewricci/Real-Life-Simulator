using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingGhostCode : MonoBehaviour {

	// This is the time that the player needs to keep the ghost in their cone of light.
	public float timeNeededForGhostToZap = 2f;
	// This timer keeps track of how long the ghost is under your light cone.
	float timeFlashlightOnGhost = 0f;

	// This is the ghost's SFX Player.
	public AudioSource ghostSFXPlayer;
	// This is the ghost SFX player.
	public AudioClip ghostYellingSFX;

	// This is the ghost's Mesh Renderer. It will be turned off when the ghost is zapped.
	MeshRenderer ghostMesh;
	// This is the ghost's Sphere Collider. It will be turned off when the ghost is zapped.
	SphereCollider ghostSphereCollider;

	// This keeps track of whether the ghost has been zapped.
	bool hasGhostBeenZapped = false;

	GameObject ghostGameAgentGameObject;

	public bool spawnAgain = false;

	float randomXValue;
	float randomYValue;
	float randomZValue;

	public Animator ghostAnim;

	float timeAlive;

	// Use this for initialization
	void Start () {
		// Get the Ghost's Mesh Renderer.
		ghostMesh = this.GetComponent<MeshRenderer> ();
		// Get the Ghost's Sphere Collider.
		ghostSphereCollider = this.GetComponent<SphereCollider> ();
		// Get the GhostGameAgent GameObject.
		ghostGameAgentGameObject = GameObject.Find ("ghostGameAgentGameObject");

		ghostSFXPlayer.mute = true;
	}
	
	// Update is called once per frame
	void Update () {


		timeAlive += Time.deltaTime;

		if (timeAlive > .5f) {
			ghostSFXPlayer.mute = false;
		}



		// This is the ray in front of the ghost.
		Ray ghostRay = new Ray( this.transform.position, this.transform.forward );
		// This determines how close the ghost can get to the wall before turning.
		float maxDistanceFromGhostsFace = .4f;
		// Setup a blank "RaycastHit" variable for later
		RaycastHit ghostRayHit = new RaycastHit();


		// visualize the Raycast
		Debug.DrawRay(ghostRay.origin, ghostRay.direction * maxDistanceFromGhostsFace, Color.yellow );

		// let's actually shoot the Raycast!
		if( Physics.Raycast(ghostRay, out ghostRayHit, maxDistanceFromGhostsFace ) ) {
			// visualize where the Raycast Hit something
			//Debug.DrawRay( lookRayHit.point, lookRayHit.normal, Color.magenta ); 
			if (ghostRayHit.rigidbody.gameObject.tag == "dontSpawnHere") {

				randomXValue = Random.Range (-90f, 90f);


				transform.Rotate (randomXValue, 0f, 0f);
			}
		}

		// Move the ghost forward a bit.
		this.transform.position += (transform.forward * Time.deltaTime * .5f);

		//this.transform.

	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == "flashlightConeCollider") {
			// Turn on the flashlight animation.
			ghostAnim.SetBool ("Flashlight", true);
		}

		Debug.Log ("Entering the ghost.");
	}

	void OnTriggerStay (Collider other) {
		// If the Ghost is Touching the Flashlight's Cone Collider.
		if (other.gameObject.tag == "flashlightConeCollider") {
			// 
			Debug.Log ("Hitting ghost.");
			// Add time to the Flashlight Hit Timer.
			timeFlashlightOnGhost += Time.deltaTime;

			Debug.Log (timeFlashlightOnGhost);
			// If that time is greater than the Time Needed to Zap,

			if (timeFlashlightOnGhost >= timeNeededForGhostToZap) {
				// Set the GhostPlayerSFX to GhostYellingSFX.
				ghostSFXPlayer.clip = ghostYellingSFX;
				// Play the GhostSFX.
				ghostSFXPlayer.Play ();
				// Turn off the Ghost's Mesh.
				ghostMesh.enabled = false;
				// Turn off the Ghost's Sphere Collider.
				ghostSphereCollider.enabled = false;

				ghostSFXPlayer.loop = false;

				this.BroadcastMessage("turnOffGhostMethod");

				ghostGameAgentGameObject.GetComponent<ghostGameAgent>().ghostsToZap -= 1;

			}
		}
	}

	void OnTriggerExit (Collider other) {
		timeFlashlightOnGhost = 0;
		if (other.gameObject.tag == "flashlightConeCollider") {
			// Turn on the flashlight animation.
			ghostAnim.SetBool ("Flashlight", false);
		}

		Debug.Log ("Leaving the ghost.");
	}
}
