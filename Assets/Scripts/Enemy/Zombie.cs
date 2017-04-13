using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy {

	// Use this for initialization
	void Awake () {
		player =  GameObject.FindGameObjectWithTag ("Player").transform;
		isDead = false;
		startingHealth = 100f;
		currentHealth = startingHealth;
		movespeed = 0.05f;
		scoreValue = 10;
		wanderRadius = 1f;
		detectRange = 20f;
		attackInterval = 1.1f;
		attackRange = 5f;
		anime = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
	}

}
