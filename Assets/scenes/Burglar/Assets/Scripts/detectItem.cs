using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Valve.VR.InteractionSystem;

public class detectItem : MonoBehaviour {

	public BurglarManager burglarManager;

	public Hand stealing_hand;

	public Canvas valueCanvas;	// canvas and text object for the ca-ching animation
	public Text value;			// that pops out of the bag

	public AudioSource cash;

	public Transform player;	// may not be necessary, used for LookAt() but can't get that to work

	Transform valueOrigin;
	float valueTimer;
	bool valueFlag; 

	// Use this for initialization
	void Start () {
		value.text = "$0";
		valueOrigin = valueCanvas.transform;
		valueTimer = 0.0f;
		valueFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
 		if (valueFlag) {
			animateValue ();
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "stealable") {
			stealable item = stealing_hand.GetComponentInChildren<stealable> (); // fill hand_children with all current children
			if (item != null && item.gameObject.activeSelf && !(item.hasBeenStolen) ) {
				Debug.Log (item);
				item.hasBeenStolen = true;
				//stealable x = item.GetComponent<stealable> ();
				//item.gameObject.SetActive (false);
				stealing_hand.DetachObject(item.gameObject);
				// Destroy (item.gameObject, 0.2f);
				item.gameObject.SetActive(false);
				cash.Play ();
				//burglarManager.numberItemsStolen += 1;
				valueFlag = true;
				burglarManager.totalValueStolen += item.value;
				value.text = "$" + item.value;
				valueCanvas.transform.LookAt (Camera.main.transform);
			}
		}
	}

	void animateValue(){
		valueTimer += Time.deltaTime;
		valueCanvas.transform.localPosition = new Vector3(valueCanvas.transform.localPosition.x, valueCanvas.transform.localPosition.y + (0.7f * Time.deltaTime), valueCanvas.transform.localPosition.z);
		if (valueTimer >= 0.75f) {
			valueTimer = 0.0f;
			valueFlag = false;
			valueCanvas.transform.localPosition = new Vector3(0.0f, 0.3f, 0.0f);
			valueCanvas.transform.localEulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
		}
	}
}
