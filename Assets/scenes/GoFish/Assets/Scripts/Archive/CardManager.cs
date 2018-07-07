using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

	// prefab to instantiate
	public Card myCardPrefab; // assign in Inspector; only objects with a Card script are allowed!

	public GameObject Player;

	public WinDetection winDetection;

	public int maxCardCount;
	List<Card> myCardList = new List<Card>(); // a list of all the cards we spawn

	float destinationResetTimer = 0f;

	public bool firstRun=true;

	Vector3 RandomUnitSphere;

	public int cardFaceListNumberManager=0;


	// Use this for initialization
	void Start () {
		maxCardCount = (gameOverlordScript.difficulty*3+1);
		int currentCardCount = 0;
		maxCardCount -= 1;
		while( currentCardCount <= maxCardCount) {
			// instantiate the card
			Card newCardClone = (Card)Instantiate( myCardPrefab, new Vector3(0f,1.7f,-0.5f), Random.rotation );
			myCardList.Add( newCardClone ); // remember the card in the list
			newCardClone.cardFaceListNumber = cardFaceListNumberManager; //assign this card's face number
			if (currentCardCount == maxCardCount-1) {
				newCardClone.correctCard1 = true;
			} else if (currentCardCount == maxCardCount) {
				newCardClone.correctCard2 = true;
			}
			else {
				newCardClone.correctCard1 = false;
				newCardClone.correctCard2 = false;
			}
			// increment card count
			currentCardCount++;
			if (cardFaceListNumberManager >= 10) {
				cardFaceListNumberManager = 0;
			} else {
				cardFaceListNumberManager += 1;
			}
		}

	}

	// Update is called once per frame
	void Update () {
		destinationResetTimer += Time.deltaTime;
		if(winDetection.lost==true){
			foreach( Card thisCard in myCardList ) { // "foreach" is good for lists / arrays
				thisCard.gameLost=true;
			}
		}
		if(winDetection.won==true){
			foreach( Card thisCard in myCardList ) { // "foreach" is good for lists / arrays
				if ((thisCard.correctCard1 != true)&&(thisCard.correctCard2 != true)) {
					thisCard.myRigidbody.useGravity = true;
				}
			}
		}

		// setup with destinationResetTimer
		if(destinationResetTimer>=3f || firstRun==true) {
			foreach( Card thisCard in myCardList ) { // "foreach" is good for lists / arrays
				RandomUnitSphere=new Vector3(Random.insideUnitSphere.x*1.2f,Random.insideUnitSphere.y*0.9f+2f,Random.insideUnitSphere.z*1.2f)+Player.transform.position;
				thisCard.destination = RandomUnitSphere;
			}
			destinationResetTimer = 0f;
			firstRun = false;
		}
	}
}