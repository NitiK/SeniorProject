using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {


	public float fillAmount;

	public Image content;

	private bool gameOver;
	// Use this for initialization
	void Start () {
		gameOver = false;
	}

	// Update is called once per frame
	void Update () {
		HandleBar ();
		if (fillAmount == 0 && !gameOver) {
			gameOver = true;
			//float fadeTime = GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);

			GameObject.FindGameObjectWithTag ("GameOver").GetComponent<ButtonMenu> ().GameOver ();
			Invoke ("FadetoMenu", 5f);
		}
	}

	private void HandleBar(){
		this.content.rectTransform.localScale = new Vector3 (this.fillAmount, 1, 1);
	}

	void FadetoMenu(){
		float fadeTime = GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);
		Invoke ("LoadAnotherLevel", 3f);
	}

	void LoadAnotherLevel(){
		//GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);
		Application.LoadLevel("Main menu");
	}

	public void setFillAmount(float num){
		this.fillAmount = num;
	}
}
