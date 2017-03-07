using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour {

	public GameObject cube;
	GameObject meteor;
	GameObject fireMeteor;
	float nextUsage;
	float delay=2;//two seconds delay.
	// Use this for initialization
	void Start () {
		/*int n = Random.Range (1, 7);
		meteor = Instantiate(cube, new Vector3(-35.8f+(11.9f*n), 4.4f, 282f), Quaternion.identity) as GameObject;
		meteor.GetComponent<Rigidbody> ().AddForce(transform.up*500000);
		meteor.GetComponent<Rigidbody> ().AddForce(-transform.forward*65000);*/
		nextUsage = Time.time + delay;
		//StartCoroutine(Example());

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextUsage) {
			nextUsage = Time.time + delay;
			int n = Random.Range (1, 7);
			print (n);
			meteor = Instantiate(cube, new Vector3(-35.8f+(11.9f*n), 4.4f, 282f), Quaternion.identity) as GameObject;
			meteor.GetComponent<Rigidbody> ().AddForce(transform.up*500000);
			meteor.GetComponent<Rigidbody> ().AddForce(-transform.forward*65000);
		}
	}

	IEnumerator Example() {
		/*int n = Random.Range (1, 7);
		print (n);
		meteor = Instantiate(cube, new Vector3(-35.8f+(11.9f*n), 4.4f, 282f), Quaternion.identity) as GameObject;
		meteor.GetComponent<Rigidbody> ().AddForce(transform.up*500000);
		meteor.GetComponent<Rigidbody> ().AddForce(-transform.forward*65000);*/
		print(Time.time);
		yield return new WaitForSeconds(5);
		print(Time.time);
	}
}
