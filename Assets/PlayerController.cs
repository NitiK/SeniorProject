using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	bool weaponArea;
	GameObject weapon;
	GameObject activeWeapon;
	// Use this for initialization
	void Start () {
		this.weaponArea = false;
		foreach (Transform t in transform)
		{
			if(t.tag == "Myweapon")// Do something to child one
			{
				this.activeWeapon = t.gameObject;
			}

		}
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		/*RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
			print("Found an object " + hit.collider.gameObject.name + " : " + hit.distance);*/

		if (Input.GetKey ("e")) {
			//print (this.weaponArea);
			if (this.weaponArea) {
				print ("Good");
				this.activeWeapon.SetActive (false);
				this.weapon.transform.position = this.activeWeapon.transform.position;
				this.weapon.transform.rotation = this.activeWeapon.transform.rotation;
				this.weapon.transform.localScale = this.activeWeapon.transform.localScale;
				Destroy (this.weapon.transform.GetComponent<Rigidbody>());
				Destroy (this.weapon.transform.GetComponent<BoxCollider>());
				Destroy (this.weapon.transform.GetComponent<SphereCollider>());
				this.weapon.transform.parent = this.transform;
				Destroy (this.activeWeapon);

			}
		}
			
	}

	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);
		print(other.gameObject.tag);
		if (other.gameObject.tag == "weapon") {
			this.weaponArea = true;
			this.weapon = other.gameObject;
		} 
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "weapon")
		{
			this.weaponArea = false;
		}
	}
}
