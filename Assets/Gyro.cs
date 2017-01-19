using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour {
	Gyroscope temp;
	// Use this for initialization
	void Start () {
		temp = Input.gyro;
		temp.enabled = true;
	}

	// Update is called once per frame
	void Update () {
//		transform.localRotation = temp.attitude;
		transform.rotation = new Quaternion(temp.attitude.y,temp.attitude.x,temp.attitude.z,temp.attitude.w);
//		transform.rotation = Quaternion.Euler((temp.attitude.eulerAngles.y)*-1+90f,temp.attitude.eulerAngles.x,(temp.attitude.eulerAngles.z-90f));
		Debug.Log ("x = "+temp.attitude.eulerAngles.x+", y = "+temp.attitude.eulerAngles.y+", z = "+temp.attitude.eulerAngles.z);
	}
}






