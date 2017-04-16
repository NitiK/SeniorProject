using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour {

	public List<GameObject> weapon = new List<GameObject>();
	public int TotalSpawn;
	int count=0;
	float timer=0;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 1 && count < TotalSpawn) {
			int randomIndex = Random.Range( 0, weapon.Count );
			int ran = Random.Range (0, transform.childCount - 1);
			GameObject.Instantiate (weapon[randomIndex], transform.GetChild (ran).position, transform.GetChild (ran).rotation);
			timer -= 1;
			count++;
		}
	}

}
