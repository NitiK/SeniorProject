using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour {

	public GameObject Cube,Sphere;
	Vector3 P1,P2,empty;
	GameObject lastPlace,Collection,sphere,selected;
	// Use this for initialization
	void Start () {
		Collection = GameObject.Find ("Collection");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Press ();
		}
		if (Input.GetMouseButtonDown (1)) {
			Select ();
			DestroySelected ();
		}
			
	}

	public void Press(){
		if (P1 == empty) {
			P1 = transform.position + transform.forward*2;
			sphere = GameObject.Instantiate (Sphere, P1, Sphere.transform.rotation);
		} else if (P2 == empty) {
			P2 = transform.position + transform.forward*2;
			P2 = new Vector3 (P2.x,P1.y,P2.z);
			Destroy (sphere);
			placeCube (P1,P2);
		} else {
			P1 = transform.position + transform.forward*2;
			P2 = empty;
		}
	}

	public void placeCube(Vector3 point1,Vector3 point2){
		Vector3 Between = point2 - point1;
		float distance = Between.magnitude;
		GameObject cube = GameObject.Instantiate (Cube,Collection.transform);
		cube.transform.localScale = new Vector3(0.1f,5,distance);
		cube.transform.position = point1 + (Between / 2.0f);
		cube.transform.LookAt(point2);
		cube.transform.position = new Vector3 (cube.transform.position.x,2.5f,cube.transform.position.z);
		lastPlace = cube;
	}
	public void IncScale(){
		lastPlace.transform.localScale *= 1.05f;
	}
	public void DecScale(){
		lastPlace.transform.localScale *= 0.95f;
	}
	public void Select(){
		selected = GetComponentInChildren<TriggerChecker> ().trigger;
	}
	public void DestroySelected(){
		Destroy (selected);
	}
}
