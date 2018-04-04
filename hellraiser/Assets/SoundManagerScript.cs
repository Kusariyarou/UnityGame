using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	public static AudioClip playerwalkSound, jumpSound;
	static AudioSource audioSrc;

	// Use this for initialization
	void Start () {

		playerwalkSound = Resources.Load<AudioClip> ("Footstep1");
		jumpSound = Resources.Load<AudioClip> ("Jump");
		audioSrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void PlaySound (string clip)
	{
		switch (clip) {
		case "footstep1":
			audioSrc.PlayOneShot (playerwalkSound);
			break;
		case "Jump":
			audioSrc.PlayOneShot (jumpSound);
			break;
		}
	
	}
}
