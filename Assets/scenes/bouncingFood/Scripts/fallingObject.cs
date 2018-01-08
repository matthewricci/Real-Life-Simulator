using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class fallingObject : MonoBehaviour {


	// SFX for various sections of the scene.
	public AudioSource eggSFXPlayer;
	public AudioClip eggYellingHelpSFX;
	public AudioClip eggBreakingSFX;
	public AudioClip eggBouncingSFX;
	public AudioClip nestCatchSFX;

	public GameObject eggYolk;

	Rigidbody thisEggRigidBody;

	ConstantForce thisEggConstantForce;

	CapsuleCollider thisEggCollider;

	MeshRenderer thisEggMeshRenderer;

	GameObject bouncingObjectGameAgent;

	void Start () {
		thisEggRigidBody = GetComponent<Rigidbody> ();
		thisEggConstantForce = GetComponent<ConstantForce> ();
		thisEggCollider = GetComponent<CapsuleCollider> ();
		thisEggMeshRenderer = GetComponent<MeshRenderer> ();

		bouncingObjectGameAgent = GameObject.Find ("gameAgent");	


	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Floor") {
			Debug.Log ("Egg hits floor.");
			// Assign the proper audio clip.
			eggSFXPlayer.clip = eggBreakingSFX;
			// Play victory sound FX.
			eggSFXPlayer.Play ();
			// 
			Vector3 yolkPosition = new Vector3(this.transform.position.x, (this.transform.position.y - 0.35f), this.transform.position.z); 
			// Spawn the yolk.
			Instantiate (eggYolk, yolkPosition, Quaternion.identity);
			// Make the egg's Mesh Renderer disappear.
			thisEggMeshRenderer.enabled = false;
			// Make the egg's collider disappear.
			thisEggCollider.enabled = false;




		}

		if (other.gameObject.tag == "racket") {

			thisEggConstantForce.force = new Vector3 (0, .005f, 0);

			// Assign the proper audio clip.
			eggSFXPlayer.clip = eggBouncingSFX;
			// Play victory sound FX.
			eggSFXPlayer.Play ();
			// Turn off the egg's collider so it does not hit other eggs.
			thisEggCollider.enabled = false;
			// Turn off the egg's shadowss:
			GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			// Turn BouncedEggs up one.
			bouncingObjectGameAgent.GetComponent<bouncingObjectGameAgent>().eggsBounced += 1;

		}

		if (other.gameObject.tag == "hitZone") {
			//thisEggRigidBody.drag = 1f;
		}

		if (other.gameObject.tag == "nest") {
			// Assign the proper audio clip.
			eggSFXPlayer.clip = nestCatchSFX;
			// Play victory sound FX.
			eggSFXPlayer.Play ();
			// Remove the egg.
			//Destroy(this.gameObject);


		}


	}




}
