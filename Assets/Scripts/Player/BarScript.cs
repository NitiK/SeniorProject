using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {


	public float fillAmount;

	public Image content;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	}

	private void HandleBar(){
		this.content.rectTransform.localScale = new Vector3 (this.fillAmount, 1, 1);
	}

	public void setFillAmount(float num){
		this.fillAmount = num;
	}
}
