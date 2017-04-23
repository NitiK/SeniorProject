using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour {

	Gyroscope temp;
	public Transform rot;
	public Transform temps;

	float x,y,z;
	// Use this for initialization
	void Start () {
		temp = Input.gyro;
		temp.updateInterval = 0.01F;
		temp.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		//		transform.localRotation = temp.attitude;
//		Quaternion temp2 = new Quaternion(-temp.attitude.x,temp.attitude.y,-temp.attitude.z,temp.attitude.w);
		Quaternion temp2 = new Quaternion(temp.attitude.x,temp.attitude.y,temp.attitude.z,temp.attitude.w);
	
		x = temp2.eulerAngles.x;
		y = temp2.eulerAngles.y;
		z = temp2.eulerAngles.z;

//		prefab.RotateAround(sphere.position, Vector3.down,  temp.rotationRateUnbiased.y + temp.rotationRateUnbiased.z - temp.rotationRateUnbiased.x);
		rot.Rotate(temp.rotationRateUnbiased);
//		rot.rotation = temp2;
		Vector3 ro = rot.right * 10;
		temps.LookAt (new Vector3(temps.position.x+ro.x,temps.position.y,temps.position.z+ro.z));
		transform.rotation = Quaternion.Euler (90f,0f,temps.eulerAngles.y);


	}
}
