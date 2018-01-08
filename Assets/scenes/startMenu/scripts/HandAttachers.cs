using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttachers : MonoBehaviour {

	public GameObject Hand;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Hand.transform.position;
		this.transform.rotation = Hand.transform.rotation;
	}
}
