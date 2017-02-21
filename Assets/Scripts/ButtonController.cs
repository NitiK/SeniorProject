using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonController : MonoBehaviour {
	public Camera cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text>().text = cam.fieldOfView + "";
	}
	public void IncFOV(){
		cam.fieldOfView += 1;
	}
	public void DecFOV(){
		cam.fieldOfView -= 1;
	}
}
