using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject zombie;
	public GameObject zombieBoss;
	public int TotalSpawn;
	int count=0;
	float timer=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 1 && count < TotalSpawn-1) {
			int ran = Random.Range (0, transform.childCount - 1);
			GameObject.Instantiate (zombie, transform.GetChild (ran).position+new Vector3(0f,-1f,0f), transform.GetChild (ran).rotation);
			timer -= 1;
			count++;
		}
		else if (timer > 1 && count < TotalSpawn) {
			int ran = Random.Range (0 , transform.childCount - 1);
			GameObject.Instantiate (zombieBoss, transform.GetChild (ran).position, transform.GetChild (ran).rotation);
			timer -= 1;
			count++;
		}
	}
}
