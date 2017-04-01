using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour {

	public bool clickMove;
	public Vector3 target;
	// Use this for initialization
	void Start () {
		this.clickMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.clickMove) {
			float speed = 0.1f;
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, this.target, step);
		}

	}

	public void MoveRight(){
		
	}

	public void MoveLeft(){
		
	}

	public void MoveForward(){
		this.target = transform.position + (transform.forward);
		print (transform.position);
		if (this.clickMove) {
			this.clickMove = false;
		}
		else {
			this.clickMove = true;
		}
		//print (transform.position + (transform.forward * 500000f));
		//transform.position += transform.forward * Time.deltaTime * 0.1f;
		/*float speed = 0.01f;
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, speed);*/
	}

	public void MoveBackward(){
		transform.position -= transform.forward * Time.deltaTime * 0.1f;
	}
} 
