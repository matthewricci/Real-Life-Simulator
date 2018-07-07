using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour {

	public AudioSource clang;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(){
		clang.Play ();
	}
}
