using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInfo : MonoBehaviour {
	public Vector3 P1, P2;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frames
	void Update () {
		
	}
	public void Init(Vector3 Point1,Vector3 Point2){
		P1 = Point1;
		P2 = Point2;
	}
	public string GetInfo(){
		string point1 = "(" + P1.x + "," + P1.y + "," + P1.z + ")";
		string point2 = "(" + P2.x + "," + P2.y + "," + P2.z + ")";
		string tran = "(" + transform.position.x + "," + transform.position.y + "," + transform.position.z + ")";
		string ro = "(" + transform.rotation.x + "," + transform.rotation.y + "," + transform.rotation.z+ "," + transform.rotation.w + ")";
		string scale = "(" + transform.localScale.x + "," + transform.localScale.y + "," + transform.localScale.z+ ")";
		return point1 + " : " + point2 + " : " + tran + " : " + ro+" : "+scale;
	}

}
