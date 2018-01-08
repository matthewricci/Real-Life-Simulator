using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPulser : MonoBehaviour {

	public float timer = 0;
	public float amp;
	HoveringEffect hoveringEffect;
	Rigidbody thisRigidbody;
	Renderer thisRenderer;

	// Use this for initialization
	void Start () {
		thisRigidbody = this.gameObject.GetComponent<Rigidbody> ();
		hoveringEffect = this.gameObject.GetComponent<HoveringEffect> ();
		thisRenderer = this.gameObject.GetComponentInChildren<Renderer> ();
		thisRigidbody.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1f*Time.deltaTime;
		if (timer >= 0.75f) {
			timer = 0f;
		} else if (timer >= 0.375f) {
			this.gameObject.transform.localScale = new Vector3 (
				this.gameObject.transform.localScale.x - amp,
				this.gameObject.transform.localScale.y,
				this.gameObject.transform.localScale.z - amp
			);
		} else if (timer <= 0.375f) {
			this.gameObject.transform.localScale = new Vector3 (
				this.gameObject.transform.localScale.x + amp,
				this.gameObject.transform.localScale.y,
				this.gameObject.transform.localScale.z + amp
			);
		}

		if(thisRenderer.material.color == Color.white){
			hoveringEffect.enabled = false;
			thisRigidbody.useGravity = true;
		}
	}

	void OnCollisionEnter () {
		hoveringEffect.enabled = false;
		thisRigidbody.useGravity = true;
	}
}
