using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonsterCounter : MonoBehaviour {

	public Spawner spawner;
    public int killedMonster;
    public Text monsterCounterText;
	private bool clear;
	private GameObject playerCamera;
	public GameObject gameContinueImage;

    public int KilledMonster
    {
        get
        {
            return killedMonster;
        }

        set
        {
            killedMonster = value;
        }
    }

    public int MaxMonster
    {
        get
        {
			return spawner.TotalSpawn;
        }

        set
        {
			spawner.TotalSpawn = value;
        }
    }


    // Use this for initialization
    void Start () {
		this.clear = false;
		playerCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		monsterCounterText.text = string.Format("Left {0} / {1}   ", killedMonster, spawner.TotalSpawn);
		if ((killedMonster == spawner.TotalSpawn) && !this.clear) {
			Invoke ("ToBeContinue", 5f);
			this.clear = true;
		}
    }

	public void AddKillMonster(int kill){
		this.killedMonster += kill;
	}

	void ToBeContinue(){
		gameContinueImage.GetComponent<ButtonMenu> ().GameContinue ();
		Invoke ("FadetoMenu", 5f);
	}

	void FadetoMenu(){
		
		float fadeTime = GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);
		Invoke ("LoadAnotherLevel", fadeTime);
	}

	void LoadAnotherLevel(){
		playerCamera.GetComponent<CamFeed>().closeUDP();
		Application.LoadLevel("Main menu");
	}


}
