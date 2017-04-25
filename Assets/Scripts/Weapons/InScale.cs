using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InScale : MonoBehaviour,Weapon {

	public int bullet;
	public int magazine;
	public float timeBetweenBullet;

	void Awake ()
	{

	}

	public void Hit(GameObject target, Vector3 point){
		Debug.Log ("InScale Hit");
		target.transform.localScale = target.transform.localScale * 1.2f;
	}

	public int GetBullet(){
		return this.bullet;
	}

	public int GetMagazine(){
		return this.magazine;
	}
	public float GetTimeBetweenBullet(){
		return this.timeBetweenBullet;
	}
}
