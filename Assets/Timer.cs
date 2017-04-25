using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText;
    public int gameLifeTime;

    protected float remainingTime;
    protected bool isGameRunning = true;
	private bool gameOver;
	public GameObject gameOverImage;
	private GameObject playerCamera;

	// Use this for initialization
	void Start () {
        remainingTime = gameLifeTime;
		gameOver = false;
		playerCamera = GameObject.FindGameObjectWithTag ("MainCamera");
    }

    // Update is called once per frame
    void Update() {
		if (isGameRunning && !gameOver)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
            {
				this.gameOver = true;
                remainingTime = 0;
				this.playerCamera.GetComponent<PlayerShooting> ().GameOver ();
				gameOverImage.GetComponent<ButtonMenu> ().GameOver ();
				Invoke ("FadetoMenu", 5f);
            }
        }

        int minute = (int)remainingTime / 60;
        int second = (int)remainingTime % 60;
        timerText.text = string.Format("{0} : {1}",minute, second);
    }

	void FadetoMenu(){
		float fadeTime = GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);
		Invoke ("LoadAnotherLevel", 3f);
	}

	void LoadAnotherLevel(){
		//GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);
		playerCamera.GetComponent<CamFeed>().closeUDP();
		Application.LoadLevel("Main menu");
	}

    public void StartTimer()
    {
        isGameRunning = true;
    }

    public void StopTimer()
    {
        isGameRunning = false;
    }
}
