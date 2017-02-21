using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gyro : MonoBehaviour {
	public GameObject worldCam;
	public GameObject RealCam;
	Gyroscope temp;
//	public GameObject textX;
//	public GameObject textY;
//	public GameObject textZ;
	float x,y,z;
	// Use this for initialization
	void Start () {
		temp = Input.gyro;
		temp.enabled = true;
	}

	// Update is called once per frame
	void Update () {
//		transform.localRotation = temp.attitude;
		Quaternion temp2 = new Quaternion(-temp.attitude.x,-temp.attitude.y,temp.attitude.z,temp.attitude.w);
		x = temp2.eulerAngles.x;
		y = temp2.eulerAngles.y;
		z = temp2.eulerAngles.z;
		transform.localRotation = Quaternion.Euler (x,y,z);
		worldCam.transform.localRotation = Quaternion.Euler (x,y,z);
		RealCam.transform.localRotation = Quaternion.Euler (x,y,z);

//		transform.rotation = Quaternion.Euler((temp.attitude.eulerAngles.y)*-1+90f,temp.attitude.eulerAngles.x,(temp.attitude.eulerAngles.z-90f));
//		string printt = "x = "+x+", y = "+y+", z = "+z;
//		textX.GetComponent<Text>().text = "X = "+transform.position.x;
//		textY.GetComponent<Text>().text = "Y = "+transform.position.y;
//		textZ.GetComponent<Text>().text = "Z = "+transform.position.z;
	}
}






