using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour {

	public void MoveForward(){
		transform.position = new Vector3(transform.position.x-0.05f,transform.position.y,transform.position.z);
	}
	public void MoeBackward(){
		transform.position = new Vector3(transform.position.x+0.05f,transform.position.y,transform.position.z);
	}
}
