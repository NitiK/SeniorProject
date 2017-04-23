using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("GameOver").GetComponent<ButtonMenu> ().GameOver ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(){
		print ("StartGame");
		float fadeTime = GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);
		/*yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel("Gun");*/
		Invoke ("LoadAnotherLevel", fadeTime);
	}

	void LoadAnotherLevel(){
		Application.LoadLevel("Gun_502");
	}
}
