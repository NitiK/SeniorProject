using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour {

	Gyroscope temp;
	public Transform direction;
	public Transform sphere;
	public Transform prefab;
	public Transform rot;
	public Text text;

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

		Quaternion temp2 = new Quaternion(-temp.attitude.x,-temp.attitude.y,temp.attitude.z,temp.attitude.w);
		x = temp2.eulerAngles.x;
		y = temp2.eulerAngles.y;
		z = temp2.eulerAngles.z;
		text.text = temp.rotationRateUnbiased.ToString ();
//		transform.localRotation = Quaternion.Euler (90, x, 0);
//		direction.r
//		prefab.localRotation = Quaternion.Euler (x, 0, 0);
//		prefab.RotateAround(sphere.position, Vector3.down,  temp.rotationRateUnbiased.y + temp.rotationRateUnbiased.z - temp.rotationRateUnbiased.x);
		rot.Rotate(temp.rotationRateUnbiased);
//		rot.rotation = temp2;
		Vector3 ro = rot.right * 10;
		prefab.LookAt (new Vector3(rot.position.x+ro.x,rot.position.y,rot.position.z+ro.z));
//		direction.position = new Vector3(rot.position.x+rot.right.x,rot.position.y,rot.position.z+rot.right.z);

//		Debug.Log (new Vector3(rot.right.x,rot.right.y,rot.right.z) );
		//		transform.rotation = Quaternion.Euler((temp.attitude.eulerAngles.y)*-1+90f,temp.attitude.eulerAngles.x,(temp.attitude.eulerAngles.z-90f));
		//		string printt = "x = "+x+", y = "+y+", z = "+z;
		//		textX.GetComponent<Text>().text = "X = "+transform.position.x;
		//		textY.GetComponent<Text>().text = "Y = "+transform.position.y;
		//		textZ.GetComponent<Text>().text = "Z = "+transform.position.z;
	}
}
