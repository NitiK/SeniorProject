using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
	public GameObject zombie;
	public GameObject zombieBoss;
	public int TotalSpawn;
	public float delay;
	private GameObject ground;
	public CanvasRenderer warningText;
	int count=0;
	float timer=0;
	bool isZombieSpawning;
	// Use this for initialization
	void Start () {
		
		ground = GameObject.FindGameObjectWithTag ("Ground");
		isZombieSpawning = true;
		closeWarningText ();
		Invoke ("DecreaseDelay", 30);
	}

	void DecreaseDelay() {
		delay -= 5;
		if (delay < 5) {
			delay = 5;
		}
		Invoke ("DecreaseDelay", 30);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(timer+3f > delay){
			if (isZombieSpawning) {
				blinkWarningText ();
			}
		}
		//print (timer);
		if (timer > delay && count < TotalSpawn-1) {
			isZombieSpawning = false;
			closeWarningText ();
			int ran = Random.Range (0, ground.transform.childCount - 1);
			GameObject.Instantiate (zombie, ground.transform.GetChild (ran).position+new Vector3(0f,-2.2f,0f), ground.transform.GetChild (ran).rotation);
			count++;
			timer -= delay;
			isZombieSpawning = true;
		}
		else if (timer > delay && count < TotalSpawn) {
			isZombieSpawning = false;
			closeWarningText ();
			int ran = Random.Range (0 , ground.transform.childCount - 1);
			GameObject.Instantiate (zombieBoss, ground.transform.GetChild (ran).position, ground.transform.GetChild (ran).rotation);
			count++;
			timer -= delay;
			isZombieSpawning = true;
		}
	}

	void blinkWarningText()
	{
		warningText.SetAlpha(Mathf.Abs(Mathf.Cos(Time.fixedTime % 2 / 2 * Mathf.PI)));
	}

	void closeWarningText()
	{
		warningText.SetAlpha(0);
	}
}
