using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour {

	public List<GameObject> weapon = new List<GameObject>();
	public int TotalSpawn;
	public float delay;
	int count=0;
	float timer=0;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > delay&& count < TotalSpawn) {
			int randomIndex = Random.Range( 0, weapon.Count );
			int ran = Random.Range (0, transform.childCount - 1);
			GameObject tmp = GameObject.Instantiate (weapon[randomIndex], transform.GetChild (ran).position, weapon[randomIndex].transform.localRotation);
			tmp.transform.localScale = tmp.transform.localScale * 2;
			tmp.GetComponent<Rigidbody> ().AddForce (4, 0, 0, ForceMode.Impulse);
			timer -= delay;
			count++;
		}
	}

}
