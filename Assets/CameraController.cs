using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetPosition() {
		this.transform.position= new Vector3(20.6f,8f,20f);
		this.transform.rotation = new Quaternion(0.0f, -180f, 0.0f,0.0f);

	}
}
