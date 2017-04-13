using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText;
    public int gameLifeTime;

    protected float remainingTime;
    protected bool isGameRunning = true;

	// Use this for initialization
	void Start () {
        remainingTime = gameLifeTime;

    }

    // Update is called once per frame
    void Update() {
        if (isGameRunning)
        {
            remainingTime -= Time.deltaTime;
        }

        int minute = (int)remainingTime / 60;
        int second = (int)remainingTime % 60;
        timerText.text = string.Format("{0} : {1}",minute, second);
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
