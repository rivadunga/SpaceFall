using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour {

	//Handle the player audio
	public AudioClip audioMax;
	void playMaxSpeedAudio()
	{
		GetComponent<AudioSource>().PlayOneShot(audioMax,0.5f);
	}
}
