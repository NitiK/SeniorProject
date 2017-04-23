using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, Weapon{

	public int bullet;
	public int magazine;
	public float timeBetweenBullet;

	void Awake ()
	{
		
	}

	public void Hit(GameObject target, Vector3 point){
		Debug.Log ("Test Hit");
		//target.transform.localScale = target.transform.localScale * 0.5f;
		//GameObject.Instantiate (target);
        if (target.GetComponent<Enemy>() != null)
		    target.GetComponent<Enemy>().takeDamage(20f, point);

        if (target.GetComponent<Cube>() != null)
            target.GetComponent<Cube>().takeDamage(20f, point);
        //Destroy (target);
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
