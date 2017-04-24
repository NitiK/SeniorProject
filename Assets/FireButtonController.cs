using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonController : MonoBehaviour {

	public static bool mouseDown;
	public PlayerShooting player;
	 
	void FixedUpdate(){
		if (mouseDown) {
			player.weaponShoot ();
			Debug.Log (Time.time);
		}
	}
	public void PointerDown(){
		mouseDown = true;
	}
	public void PointerUp(){
		mouseDown = false;
	}
		
		
}
