using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffGhost : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void turnOffGhostMethod () {
		this.gameObject.SetActive(false);
	}


}
