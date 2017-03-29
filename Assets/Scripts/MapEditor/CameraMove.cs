using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveRight(){
		
	}

	public void MoveLeft(){
		
	}

	public void MoveForward(){
		transform.position += transform.forward * Time.deltaTime * 0.1f;
	}

	public void MoveBackward(){
		transform.position -= transform.forward * Time.deltaTime * 0.1f;
	}
} 
