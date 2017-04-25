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
	const int attacklaow = 6;

	public float startingHealth = 100f;
	public float currentHealth;
	public float movespeed = 1.5f;
	public int scoreValue = 10;
	public float wanderRadius = 2f;
	public float detectRange = 5f;
	public float attackInterval = 2f;
	public float attackRange = 1f;
	public float collideTime;
	public float AtkDamage;
	public float creepUpTime;
	public int state = portal;
	public bool isDead;
	public Animator anime;

	public Transform player;
	public NavMeshAgent agent;
	public BarScript hpBar;
	public MonsterCounter monKill;

    public GameObject hitEffect;
    public float destoryHitEffectTime;
    // Use this for initialization
    void Awake () {
		player =  GameObject.FindGameObjectWithTag ("Player").transform;
		currentHealth = startingHealth;
		isDead = false;
		anime = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = movespeed;
		state = spawning;
		monKill =  GameObject.FindGameObjectWithTag ("CanvasManager").GetComponent<MonsterCounter>();
		hpBar = GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<BarScript>();

	}

	// Update is called once per frame
	void Update () {
//		Debug.Log (state);
		anime.SetInteger ("state",state);
		switch (state) {

		case spawning:
			Invoke ("AfterSpawn", creepUpTime);
			state = attacklaow;
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
			if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= 1) {
				state = portal;
			}
			NavMeshPath path = new NavMeshPath();
			agent.CalculatePath(agent.destination, path);
			if (path.status == NavMeshPathStatus.PathPartial)
			{
				state = portal;
			}

			break;

		case die:
			break;

		}
		if (state != die&&state!=attacklaow&&state != attack) {
			var relativePoint = transform.InverseTransformPoint(player.transform.position);
			if (Vector3.Distance (transform.position, player.transform.position) <= attackRange && state == chase&&Mathf.Abs(relativePoint.x)<0.25f){

				state = attack;
				agent.Stop ();
			} else if (Vector3.Distance (transform.position, player.transform.position) <= detectRange ) {
				state = chase;
			}
		}
	}

	public void takeDamage(float dmg, Vector3 point){
		if(isDead){
			return;
		}
		currentHealth -= dmg;
		if (state != attacklaow) {
			state = chase;
			CancelInvoke ();
			if (currentHealth > 0) {
				anime.SetBool ("IsDamage", true);
			}
		}

//		if (currentHealth > 0) {
//			anime.SetBool ("IsDamage", true);
//		}
		agent.Stop ();

        GameObject instantiatedObj = Instantiate(hitEffect, point, Quaternion.identity, gameObject.transform) as GameObject;
        Destroy(instantiatedObj, destoryHitEffectTime);

		if (currentHealth <= 0) {
			CancelInvoke();
			dead ();
		} else {
			if (state != attacklaow) {
				Invoke ("Reset", 0.25f);
				Invoke ("ResetIsDamage", 0.1f);
			}
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
		anime.Play("atack02");
		agent.Stop();
		Invoke ("Reset", attackInterval);
		state = attacklaow;
	}
	void dead(){
		agent.Stop ();
		isDead = true;
		state = die;
		this.monKill.AddKillMonster (1);
		Invoke ("destroyZombie",10f);
		anime.SetInteger ("deadState", Random.Range(1,3));
		anime.SetBool("isDead",true);
		agent.enabled = false;
		GetComponent<Collider> ().enabled = false;
//		transform.position += Vector3.down*0.2f;
//		GetComponent<Enemy> ().enabled = false;
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
		agent.Resume ();
	}
	void AfterSpawn (){
		Debug.Log ("After Spawn");
		GetComponent<Collider> ().enabled = true;
		state = portal;
		agent.enabled = true;
	}
	void ResetIsDamage(){
		anime.SetBool("IsDamage",false);
	}
	void applyDamage(){
		//Damage to player
//		print("Damage to player");
		this.player.GetComponent<PlayerController> ().takeDamage (AtkDamage);
		this.hpBar.setFillAmount(this.player.GetComponent<PlayerController> ().getHP());
	}
	void destroyZombie(){
		//Damage to player
		//		print("Damage to player");
		Destroy(this.transform.gameObject);
	}
}
