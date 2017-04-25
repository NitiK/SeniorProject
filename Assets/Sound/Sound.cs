using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

	AudioSource audio;//for holding the audio source
	public AudioClip[] audioClip;
	void Start () { 
		audio = GetComponent<AudioSource> ();
	}//put this in start. This gets the audio source. }
	public void PlaySound(int i){
		audio.PlayOneShot (audioClip [i], 1);
	}
}
