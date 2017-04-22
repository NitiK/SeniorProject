using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float playerHP;
	// Use this for initialization
	void Start () {
		this.playerHP = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		/*RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
			print("Found an object " + hit.collider.gameObject.name + " : " + hit.distance);*/

	
			
	}

	public void takeDamage(float damage){
		if (this.playerHP > 0) {
			if (this.playerHP - damage < 0) {
				this.playerHP = 0;
			} else {
				this.playerHP -= damage;
			}
		} else {
			this.playerHP = 0;
		}
	}

	public float getHP(){
		return this.playerHP;
	}
}
