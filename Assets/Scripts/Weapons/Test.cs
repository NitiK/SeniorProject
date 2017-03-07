using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, Weapon{

	public void Hit(GameObject target){
		Debug.Log ("Test Hit");
//		target.transform.localScale = target.transform.localScale * 0.5f;
//		GameObject.Instantiate (target);
		//target.GetComponent<Enemy>().takeDamage(10f);
		Destroy (target);
	}
}
