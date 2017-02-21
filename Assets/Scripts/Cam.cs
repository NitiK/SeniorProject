using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cam : MonoBehaviour {
	private WebCamTexture webCamTexture;
	// Use this for initialization
	void Start () {
		webCamTexture = new WebCamTexture();
		webCamTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<RawImage>().texture = webCamTexture;

	}
}
