using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	//define state
	const int portal = 0;
	const int chase = 1;
	const int attack = 2;
	const int die = 3;

	public float startingHealth = 100f;
	public float currentHealth;
	public float movespeed = 1.0f;
	public int scoreValue = 10;
	public float wanderRadius = 20f;
	int state;
	bool isDead;
	Animator anime;

	Transform player;
	private NavMeshAgent agent;

	// Use this for initialization
	void Awake () {
		player =  GameObject.FindGameObjectWithTag ("Player").transform;
		currentHealth = startingHealth;
		isDead = false;
		anime = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		state = portal;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Vector3.Distance(player.position,transform.position));


		switch (state) {
		case portal:
			Vector3 newPos = RandomNavSphere (transform.position, wanderRadius, -1);
			agent.SetDestination (newPos);
			break;

		case chase:
			agent.SetDestination (player.position);
			break;

		case attack:
			
			break;

		case die:
			
			break;
		}
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
	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
		Vector3 randDirection = Random.insideUnitSphere * dist;

		randDirection += origin;

		NavMeshHit navHit;

		NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);

		return navHit.position;
	}
}
