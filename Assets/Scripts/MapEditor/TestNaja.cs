using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNaja : MonoBehaviour {
	GameObject map;
	// Use this for initialization
	void Start () {
		map = GameObject.FindGameObjectWithTag ("Map");
		for(int i=0;i<transform.childCount;i++)
		{
			TranslatePosition(transform.GetChild(i).gameObject);
			Debug.Log ("TestNaja child : " +i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void TranslatePosition(GameObject input){
		Vector3 pos = input.transform.position;
		Vector3 center = new Vector3(0,0,0); 

		Quaternion rot = Quaternion.AngleAxis (map.transform.eulerAngles.y, Vector3.up); // get the desired rotation
		Quaternion rot2 = Quaternion.AngleAxis (map.transform.eulerAngles.z,Vector3.forward);
		Quaternion rot3 = Quaternion.AngleAxis (map.transform.eulerAngles.x, Vector3.right);
		Vector3 dir = pos - center; // find current direction relative to center
		dir = rot*rot2*rot3 * dir; // rotate the direction
		input.transform.position = center + dir; // define new position
//
//		Quaternion rot = Quaternion.AngleAxis (map.transform.eulerAngles.y, Vector3.up); // get the desired rotation
////		Quaternion rot2 = Quaternion.AngleAxis (map.transform.eulerAngles.z,Vector3.forward);
////		Quaternion rot3 = Quaternion.AngleAxis (map.transform.eulerAngles.x, Vector3.right);
//		Vector3 dir = pos - center; // find current direction relative to center
//		dir = rot* dir; // rotate the direction
//		input.transform.position = center + dir; // define new position
//
//		pos = input.transform.position;
//		rot = Quaternion.AngleAxis(map.transform.eulerAngles.z,Vector3.forward); // get the desired rotation
//		dir = pos - center; // find current direction relative to center
//		dir = rot * dir; // rotate the direction
//		input.transform.position = center + dir; // define new position
//
////		rot = Quaternion.AngleAxis (map.transform.eulerAngles.x, Vector3.right); // get the desired rotation
////		dir = pos - center; // find current direction relative to center
////		dir = rot * dir; // rotate the direction
//		input.transform.position = center + dir; // define new position
//		Debug.Log(input.transform.position);

	}
}
