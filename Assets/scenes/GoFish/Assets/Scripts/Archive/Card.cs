using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO LIST:
// X make it stop vibrating when it reaches its destination
// X visualize a debug line to show where the card is going

public class Card : MonoBehaviour {

	public CardManager cardManager;
	public float speed = 5f;
	public Vector3 destination; // the card will always swim towards its destination
	public bool correctCard1=false;
	public bool correctCard2=false;
	public bool held=false;
	public bool gameLost = false;
	public bool FirstRunTexture = true;
	public Rigidbody myRigidbody;
	public Renderer cardRenderer;
	public Material AceHearts;
	public Material AceSpades;
	public int cardFaceListNumber;

	public List<Material> cardFaces = new List<Material>{};

	void Start(){
		cardRenderer = GetComponent<Renderer> ();
		myRigidbody = this.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if (correctCard1 == true && FirstRunTexture == true) {
			cardRenderer.material = AceHearts;
			FirstRunTexture = false;
		} else if (correctCard2 == true && FirstRunTexture == true) {
			cardRenderer.material = AceSpades;
			FirstRunTexture = false;
		}
		else if (correctCard1!=true && correctCard2!=true && FirstRunTexture == true){
			cardRenderer.material = cardFaces[cardFaceListNumber];
		}
		if (held == true) {
		} else if (gameLost==true){
			myRigidbody.velocity = Vector3.forward*5f;
		}else {
			// make the card always swim towards its destination
			Vector3 moveDir = destination - transform.position; // vector from A to B = B - A
			Debug.DrawLine (transform.position, destination, Color.yellow); // visualize path in Scene tab

			// normalize the vector so that the card doesn't teleport
			if (moveDir.magnitude > 1f) { // but only normalize if it's far from its destination!
				//moveDir = moveDir.normalized; 
			}
			// moveDir = Vector3.Normalize( moveDir ); // this does the same thing too

			// actually move the card now!
			myRigidbody.velocity = moveDir * speed * Time.deltaTime * 5f;

			// smoothly rotate the card
			myRigidbody.AddTorque(Vector3.Normalize(moveDir * speed));
		}
	}
}