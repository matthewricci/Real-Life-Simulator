using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

using Valve.VR.InteractionSystem;

public class CardDetector : MonoBehaviour {

	public Hand LeftHand;
	public Hand RightHand;
	public Card LeftHandCard;
	public Card RightHandCard;
	public WinDetection winDetection;

	void Update(){
		LeftHandCard = LeftHand.GetComponentInChildren<Card>();
		RightHandCard = RightHand.GetComponentInChildren<Card>();
		if((LeftHandCard.correctCard1==true || LeftHandCard.correctCard2==true)&& (RightHandCard.correctCard1==true || RightHandCard.correctCard2==true)){
			winDetection.won = true;
		}
	}
}