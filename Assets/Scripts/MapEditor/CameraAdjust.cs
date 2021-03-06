﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAdjust : MonoBehaviour {
	public Text text;
	// Use this for initialization
	void Start () {
		text.text = transform.eulerAngles.y.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Plus(){
		//transform.Rotate (Vector3.back*10);
		print("Plus Angle");
		transform.Rotate (Vector3.forward);
		text.text = Mathf.RoundToInt(transform.eulerAngles.y).ToString();
	}
	public void Minus(){
		//transform.Rotate (Vector3.forward*10);
		print("Minus Angle");
		transform.Rotate (Vector3.back);
		text.text = Mathf.RoundToInt(transform.eulerAngles.y).ToString();
    }
	public void Plus10(){
		//transform.Rotate (Vector3.back*10);
		print("Plus Angle");
		transform.Rotate (Vector3.forward*10);
		text.text = Mathf.RoundToInt(transform.eulerAngles.y).ToString();
	}
	public void Minus10(){
		//transform.Rotate (Vector3.forward*10);
		print("Minus Angle");
		transform.Rotate (Vector3.back*10);
		text.text = Mathf.RoundToInt(transform.eulerAngles.y).ToString();
	}
}
