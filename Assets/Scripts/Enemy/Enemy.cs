using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float startingHealth = 100f;
	public float currentHealth;
	public float movespeed = 1.0f;
	public int scoreValue = 10;
	bool isDead;
	Animator anime;
	// Use this for initialization
	void Awake () {
		currentHealth = startingHealth;
		isDead = false;
		anime = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage(float dmg){
		if(isDead){
			return;
		}
		currentHealth -= dmg;
		anime.SetBool("isDamage",true);
		if(currentHealth<=0){
			dead();
		}
	}

	void dead(){
		isDead = true;
		anime.SetInteger ("deadState", Random.Range(1,3));
		anime.SetBool("isDead",true);
	}
}
