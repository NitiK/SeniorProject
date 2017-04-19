using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, Weapon{

	public void Hit(GameObject target, Vector3 point){
		Debug.Log ("Test Hit");
		//target.transform.localScale = target.transform.localScale * 0.5f;
		//GameObject.Instantiate (target);
        if (target.GetComponent<Enemy>() != null)
		    target.GetComponent<Enemy>().takeDamage(10f, point);

        if (target.GetComponent<Cube>() != null)
            target.GetComponent<Cube>().takeDamage(10f, point);
        //Destroy (target);
    }
}
