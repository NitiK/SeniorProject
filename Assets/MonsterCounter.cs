using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonsterCounter : MonoBehaviour {

    public int maxMonster;
    public int killedMonster;
    public Text monsterCounterText;
	private int clear = 0;

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
            return maxMonster;
        }

        set
        {
            maxMonster = value;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        monsterCounterText.text = string.Format("Left {0} / {1}   ", killedMonster, maxMonster);
		if (killedMonster == maxMonster) {
			Invoke ("loadAnotherLevel", 5f);
			this.clear = 1;
		}
    }

	public void AddKillMonster(int kill){
		this.killedMonster += kill;
	}

	void loadAnotherLevel(){
		Application.LoadLevel("Gun lv2");
	}


}
