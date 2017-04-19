using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public GameObject hitEffect;
    public float hitEffectTime;

    public GameObject destoryEffect;
    public float destoryEffectTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void takeDamage(float dmg, Vector3 point)
    {
        GameObject instantiatedObj = Instantiate(hitEffect, point, Quaternion.identity, gameObject.transform) as GameObject;
        Destroy(instantiatedObj, hitEffectTime);

        Destroy(gameObject, destoryEffectTime);
        GameObject instantiatedObj2 = Instantiate(destoryEffect, point, Quaternion.identity) as GameObject;

        Destroy(instantiatedObj2, destoryEffectTime);
    }

}
