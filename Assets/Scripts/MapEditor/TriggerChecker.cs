using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour {
	public GameObject trigger = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other) {
		trigger = other.gameObject;
		Debug.Log ("Trigger In");
	}
	void OnTriggerExit(Collider other) {
		trigger = null;
		Debug.Log ("Trigger Out");
	}
}
