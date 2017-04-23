using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeScale : MonoBehaviour,Weapon {

	public int bullet;
	public int magazine;
	public float timeBetweenBullet;

	void Awake ()
	{

	}

	public void Hit(GameObject target, Vector3 point){
		Debug.Log ("DeScale Hit");
		target.transform.localScale = target.transform.localScale * 0.95f;
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
