using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour {

    private int runner;
    private int magic;
    // Use this for initialization
    void Start () {
        runner = 0;
        magic = Mathf.RoundToInt(Random.Range(-20.0f, 20.0f));
    }
	
	// Update is called once per frame
	void Update () {
        if (runner % 400 < 200)
        {
            gameObject.transform.position += new Vector3(0, magic * 0.0005f, 0);
        }
        else
        {
            gameObject.transform.position -= new Vector3(0, magic * 0.0005f, 0);
        }
        runner++;
    }
}
