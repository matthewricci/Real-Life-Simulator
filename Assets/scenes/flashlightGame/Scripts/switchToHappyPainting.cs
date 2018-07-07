using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchToHappyPainting : MonoBehaviour {

	Renderer myRenderer;

	public Material happyMaterial;

	// Use this for initialization
	void Start () {
		myRenderer = gameObject.GetComponent<Renderer> ();
		myRenderer.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (ghostGameAgent.switchToHappy == true) {
			myRenderer.sharedMaterial = happyMaterial;
		}
	}
}
