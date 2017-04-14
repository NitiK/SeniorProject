using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	//define state
	const int spawning= 3;
	const int portal = 0;
	const int chase = 1;
	const int attack = 4;
	const int die = 5;
	const int portaling = 2;

	public float startingHealth = 100f;
	public float currentHealth;
	public float movespeed = 1.5f;
	public int scoreValue = 10;
	public float wanderRadius = 2f;
	public float detectRange = 5f;
	public float attackInterval = 2f;
	public float attackRange = 1f;
	public float collideTime;
	public int state = portal;
	public bool isDead;
	public Animator anime;

	public Transform player;
	public NavMeshAgent agent;
	public BarScript hpBar;

	// Use this for initialization
	void Awake () {
		player =  GameObject.FindGameObjectWithTag ("Player").transform;
		currentHealth = startingHealth;
		isDead = false;
		anime = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = movespeed;
		state = spawning;

	}

	// Update is called once per frame
	void Update () {
//		Debug.Log (transform.position);
		anime.SetInteger ("state",state);

		switch (state) {

		case spawning:
			Invoke ("AfterSpawn", 2.2f);
			break;

		case portal:
			Vector3 newPos = RandomNavSphere (transform.position, wanderRadius, -1);
			agent.SetDestination(newPos);
			state = portaling;
			break;

		case chase:
			agent.SetDestination (player.position);
			break;

		case attack:
			Attack ();
			break;

		case portaling:
			float dist = agent.remainingDistance; 
			if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
				state = portal;
			}
			break;

		case die:
			agent.SetDestination (transform.position);
			break;

		}
	
		if (Vector3.Distance (transform.position, player.transform.position) <= attackRange && state == chase) {
			state = attack;
		}
		else if (Vector3.Distance (transform.position, player.transform.position) <= detectRange) {
			state = chase;
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
//	void OnCollisionEnter (Collision col)
//	{
//		Debug.Log ("Zombie Collide!!");
//		if(col.gameObject.tag == "Player" && state == chase)
//		{
//			Attack ();
//		}
//	}
//	void OnCollisionStay(Collision col) {
//		if(col.gameObject.tag == "Player" && state == chase)
//		{
//			Attack ();
//		}
//	}
	void Attack(){
//		Debug.Log ("Zombie Attack!!");
		Invoke ("applyDamage",attackInterval/2.1f);
		Invoke ("Reset", attackInterval);
	}
	void dead(){
		isDead = true;
		state = die;
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
	void Reset (){
		state = chase;
	}
	void AfterSpawn (){
		state = portal;
	}
	void applyDamage(){
		//Damage to player
//		print("Damage to player");
		this.player.GetComponent<PlayerController> ().takeDamage (0.1f);
		this.hpBar.setFillAmount(this.player.GetComponent<PlayerController> ().getHP());
	}
}
