using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour {

	// prefab to instantiate
	public List<Number> myNumbers = new List<Number>(); // a list of all the numbers we spawn, assign in Inspector; only objects with a Number script are allowed!

	public GameObject Player;

	public WinDetection winDetection;

	public int maxNumberCount;
	List<Number> myNumberList = new List<Number>(); // a list of all the numbers we spawn

	float destinationResetTimer = 0f;

	public bool firstRun=true;

	Vector3 RandomUnitSphere;

	public int correctNumber;
	public bool correctNumberFlag=false;

	public List<Text> myText = new List<Text>();

	// Use this for initialization
	void Start () {
		correctNumber = Random.Range (1, 10);
		foreach (Text currentText in myText) {
			currentText.text = "Find the " + correctNumber + "'s";
		}
		maxNumberCount = (gameOverlordScript.difficulty*4);
		int currentNumberCount = 1;
		while( currentNumberCount <= maxNumberCount) {
			// instantiate the number object

			int arrayInt = Random.Range(0,9);
			//do not randomly spawn the correct number or a 6 with a 9
			if (arrayInt == (correctNumber-1)||(correctNumber==9&&arrayInt==5)) {
				arrayInt += 1;
			}
			if (correctNumber == 6 && arrayInt == 8) {
				arrayInt -= 1;
			}
			//instead spawn only two
			if (currentNumberCount >= maxNumberCount - 1) {
				arrayInt = correctNumber-1;
				correctNumberFlag = true;
			} else {
				correctNumberFlag = false;
			}

			Number newNumberClone = (Number)Instantiate( myNumbers[arrayInt], new Vector3(0f,1.7f,-0.5f), Random.rotation );
			myNumberList.Add( newNumberClone ); // remember the number in the list
			if (correctNumberFlag == true) {
				newNumberClone.correctNumber = true;
			} else {
				newNumberClone.correctNumber = false;
			}

			// increment number count
			currentNumberCount++;
		}

	}

	// Update is called once per frame
	void Update () {
		destinationResetTimer += Time.deltaTime;
		if(winDetection.lost==true){
			foreach( Number thisNumber in myNumberList ) {
				thisNumber.gameLost=true;
			}
		}
		if(winDetection.won==true){
			foreach( Number thisNumber in myNumberList ) {
				if (thisNumber.correctNumber != true) {
					thisNumber.myRigidbody.useGravity = true;
				}
			}
		}

		// setup with destinationResetTimer
		if(destinationResetTimer>=3f || firstRun==true) {
			foreach( Number thisNumber in myNumberList ) {
				RandomUnitSphere=new Vector3(Random.insideUnitSphere.x*1.2f,Random.insideUnitSphere.y*0.9f+2f,Random.insideUnitSphere.z*1.2f)+Player.transform.position;
				thisNumber.destination = RandomUnitSphere;
			}
			destinationResetTimer = 0f;
			firstRun = false;
		}
	}
}