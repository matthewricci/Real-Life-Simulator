using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMover : MonoBehaviour {

	public float scrollSpeed = 0.1f;
	public Renderer rend;

	void Start() {
		rend = this.GetComponent<Renderer>();
		scrollSpeed = 0.1f;
	}
	void Update() {
		float offset = Time.time * scrollSpeed;
		rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}