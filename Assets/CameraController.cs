using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject prefab;
	// Use this for initialization
	void Start () {
	}
	


	void FixedUpdate()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit))
			print("Found an object - distance: " + hit.distance);

		if (Input.GetMouseButtonDown (0)) {
			AddBox ();
		}
	}

	public void ResetPosition() {
		this.transform.position= new Vector3(20.6f,8f,20f);
		this.transform.rotation = new Quaternion(0.0f, -180f, 0.0f,0.0f);

	}

	public void AddBox(){
		Instantiate(prefab, transform.forward, Quaternion.identity);
	}
}
