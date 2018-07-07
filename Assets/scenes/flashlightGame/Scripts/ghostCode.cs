using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostCode : MonoBehaviour {

	// This is the time that the player needs to keep the ghost in their cone of light.
	public float timeNeededForGhostToZap = 2f;
	// This timer keeps track of how long the ghost is under your light cone.
	float timeFlashlightOnGhost = 0f;

	// This is the ghost's SFX Player.
	public AudioSource ghostSFXPlayer;
	// This is the ghost's sound effect.s
	public AudioClip ghostYellingSFX;

	// This is the ghost's Mesh Renderer. It will be turned off when the ghost is zapped.
	MeshRenderer ghostMesh;
	// This is the ghost's Sphere Collider. It will be turned off when the ghost is zapped.
	SphereCollider ghostSphereCollider;

	// This keeps track of whether the ghost has been zapped.
	bool hasGhostBeenZapped = false;

	// This is the game agent for the flashlightGame agent.
	GameObject ghostGameAgentGameObject;
	// Through this script, we can control:
	// - flashlightOnGhost.
	// - ghostsToZap.

	public Animator ghostAnim;

	public bool ghostsCanDie = true;

	// Should this ghost spawn again?
	public bool spawnAgain = false;

	public bool extraGhost = false;

	private float timeAlive = 0;

	void Start() {
		ghostSFXPlayer.mute = true;
		// Get the Ghost's Mesh Renderer.
		ghostMesh = this.GetComponent<MeshRenderer> ();
		// Get the Ghost's Sphere Collider.
		ghostSphereCollider = this.GetComponent<SphereCollider> ();
		// Get the GhostGameAgent GameObject.
		ghostGameAgentGameObject = GameObject.Find ("ghostGameAgentGameObject");

		ghostAnim = GetComponent<Animator> ();

	}

	void Update () {
		timeAlive += Time.deltaTime;

		if (timeAlive > .5f) {
			ghostSFXPlayer.mute = false;
		}
	}


//	void OnCollisionStay (Collider other) {
//		if (other.gameObject.tag == "dontSpawnHere") {
//			spawnAgain = true;
//			Debug.Log ("This egg spawned in the wrong area.");
//		}
//
//	}


	// When the ghost hits a trigger zone:
	void OnTriggerEnter (Collider other) {
		// If the ghost is in the flashlight trigger.
		if (other.gameObject.tag == "flashlightConeCollider" && ghostsCanDie) {
			// Turn on the bool that shows that flashlight is on the ghost.
			ghostGameAgentGameObject.GetComponent<ghostGameAgent> ().flashlightOnGhost = true;
			// Turn on the flashlight animation.
			ghostAnim.SetBool ("Flashlight", true);

			Debug.Log ("Starting to hit ghost with flashlight.");
		}


	}

	// While you are in the trigger,
	void OnTriggerStay (Collider other) {
		if (other.gameObject.tag == "dontSpawnHere") {
			spawnAgain = true;
			Debug.Log ("This ghost spawned in the wrong area.");
		}

		if (other.gameObject.tag == "dontSpawnHere" && extraGhost) {
			this.gameObject.SetActive (false);
		}
		// If the Ghost is Touching the Flashlight's Cone Collider.
		if (other.gameObject.tag == "flashlightConeCollider" && ghostsCanDie) {
			// 

			//ghostAnimation.Play ("HitByFlashlight");

			//Debug.Log ("The ghost is in the flashlight collider.");
			// Add time to the Flashlight Hit Timer.
			timeFlashlightOnGhost += Time.deltaTime;
			// If that time is greater than the Time Needed to Zap,
			if (timeFlashlightOnGhost >= timeNeededForGhostToZap) {
				// Set the GhostPlayerSFX to GhostYellingSFX.
				ghostSFXPlayer.clip = ghostYellingSFX;
				// Play the GhostSFX.
				ghostSFXPlayer.Play ();

				ghostSFXPlayer.loop = false;
				// Turn off the Ghost's Mesh.
				ghostMesh.enabled = false;
				// Turn off the Ghost's Sphere Collider.
				ghostSphereCollider.enabled = false;

				this.BroadcastMessage("turnOffGhostMethod");
				// Tell the ghostGameAgent that you need one last ghost to zap.
				ghostGameAgentGameObject.GetComponent<ghostGameAgent>().ghostsToZap -= 1;
			}
		}
	}

	// If you leave the trigger,
	void OnTriggerExit (Collider other) {
		// And that trigger is the flashlightConeCollider,
		if (other.gameObject.tag == "flashlightConeCollider") {
			// Set time on flashlight to zero.
			timeFlashlightOnGhost = 0;
			// Tell the ghostGameAgent that you are not shining the light on the ghost.
			ghostGameAgentGameObject.GetComponent<ghostGameAgent> ().flashlightOnGhost = false;
			// Turn off the flashlight animation.
			ghostAnim.SetBool ("Flashlight", false);
			Debug.Log ("No longer hit ghost with flashlight.");
		}
	}


}
