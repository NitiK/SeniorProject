using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject zombie;
	public GameObject zombieBoss;
	public int TotalSpawn;
	public float delay;
	private GameObject ground;
	int count=0;
	float timer=0;
	// Use this for initialization
	void Start () {
		ground = GameObject.FindGameObjectWithTag ("Ground");
		Invoke ("DecreaseDelay", 30);
	}

	void DecreaseDelay() {
		delay = delay / 2;
		Invoke ("DecreaseDelay", 30);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//print (timer);
		if (timer > delay && count < TotalSpawn-1) {
			int ran = Random.Range (0, ground.transform.childCount - 1);
			GameObject.Instantiate (zombie, ground.transform.GetChild (ran).position+new Vector3(0f,-2.2f,0f), ground.transform.GetChild (ran).rotation);
			count++;
			timer -= delay;
		}
		else if (timer > delay && count < TotalSpawn) {
			int ran = Random.Range (0 , ground.transform.childCount - 1);
			GameObject.Instantiate (zombieBoss, ground.transform.GetChild (ran).position, ground.transform.GetChild (ran).rotation);
			count++;
			timer -= delay;
		}
	}
}
