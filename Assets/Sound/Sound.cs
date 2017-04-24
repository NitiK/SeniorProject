using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

	AudioSource audio;//for holding the audio source
	public AudioClip[] audioClip;
	void Start () { 
		audio = GetComponent<AudioSource> ();
	}//put this in start. This gets the audio source. }
	public void PlaySound0(){
		audio.PlayOneShot (audioClip [0], 1);
	}
	public void PlaySound1(){
		audio.PlayOneShot (audioClip [1], 1);
	}
	public void PlaySound2(){
		audio.PlayOneShot (audioClip [2], 1);
	}
	public void PlaySound3(){
		audio.PlayOneShot (audioClip [3], 1);
	}
	public void PlaySound4(){
		audio.PlayOneShot (audioClip [4], 1);
	}
}
