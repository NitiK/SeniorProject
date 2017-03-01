using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InScale : MonoBehaviour,Weapon {

	public void Hit(GameObject target){
		Debug.Log ("InScale Hit");
		target.transform.localScale = target.transform.localScale * 1.05f;
	}
}
