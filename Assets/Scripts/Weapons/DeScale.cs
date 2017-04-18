using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeScale : MonoBehaviour,Weapon {

	public void Hit(GameObject target, Vector3 point){
		Debug.Log ("DeScale Hit");
		target.transform.localScale = target.transform.localScale * 0.95f;
	}
}
