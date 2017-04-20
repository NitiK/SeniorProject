using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour {

	/*public Image playButton;
	// Use this for initialization
	void Start () {
		playButton.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
		playButton.CrossFadeAlpha(1f,0.1f,false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

	public float FadeRate;
	private Image image;
	private float targetAlpha;
	float nextStage;
	float delay=2.0f;

	private bool gameOver;

	// Use this for initialization
	void Start () {
		gameOver = false;
		this.image = this.GetComponent<Image>();
		//this.image.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		Color temp = this.image.color;
		temp.a = 0.0f;
		this.image.color = temp;

		nextStage = Time.time + delay;
		if(this.image==null)
		{
			Debug.LogError("Error: No image on "+this.name);
		}
		this.targetAlpha = this.image.color.a;
		//FadeOut ();


	}

	// Update is called once per frame
	void Update () {
		if (Time.time > nextStage && gameOver) {
			//nextStage = Time.time + 10f;
			//print("FadeIN");
			FadeIn ();
		}
		Color curColor = this.image.color;
		float alphaDiff = Mathf.Abs(curColor.a-this.targetAlpha);
		if (alphaDiff>0.0001f)
		{
			curColor.a = Mathf.Lerp(curColor.a,targetAlpha,this.FadeRate*Time.deltaTime);
			this.image.color = curColor;
		}
	}

	public void FadeOut()
	{
		this.targetAlpha = 0.0f;
	}

	public void FadeIn()
	{
		this.targetAlpha = 1.0f;
	}

	public void GameOver(){
		gameOver = true;
	}

	public void ResetGame(){
		gameOver = false;
	}
}
