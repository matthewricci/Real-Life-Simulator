using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDetector : MonoBehaviour {

	public GameObject LeftHand;
	public GameObject RightHand;
	public GameObject Fallback;
	public Number LeftHandNumber;
	public Number RightHandNumber;
	public Number FallbackHandNumber;
	public WinDetection winDetection;

	void Update(){
		LeftHandNumber = LeftHand.GetComponentInChildren<Number>();
		RightHandNumber = RightHand.GetComponentInChildren<Number>();
		FallbackHandNumber = Fallback.GetComponentInChildren<Number>();

		if (LeftHandNumber != null && RightHandNumber != null) {
			if (LeftHandNumber.correctNumber == true && RightHandNumber.correctNumber == true) {
				winDetection.won = true;
			}
		}

		if (FallbackHandNumber != null) {
			if (FallbackHandNumber.correctNumber == true) {
				winDetection.won = true;
			}
		}
	}
}