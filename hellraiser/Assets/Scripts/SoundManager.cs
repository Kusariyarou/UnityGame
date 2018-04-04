using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource efxenemySource;
	public AudioSource efx2Source;
	public AudioSource efxSource;
	public AudioSource musicSource;
	public static SoundManager instance = null;
	public static SoundManager instance2 = null;
	public static SoundManager instance3 = null;
	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;

	// Use this for initialization
	void Awake () {

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		if (instance2 == null)
			instance2 = this;
		else if (instance2 != this)
			Destroy (gameObject);

		if (instance3 == null)
			instance3 = this;
		else if (instance3 != this)
			Destroy (gameObject);
		
	}

	public void PlaySingle (AudioClip clip)
	{
		efx2Source.clip = clip;
		efx2Source.Play ();

		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void RandomizeSfx (params AudioClip [] clips)
	{
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play ();

		efx2Source.pitch = randomPitch;
		efx2Source.clip = clips [randomIndex];
		efx2Source.Play ();
	}

	public void PlaySingle2 (AudioClip clip)
	{
		efx2Source.clip = clip;
		efx2Source.Play ();


	}
	public void PlaySingleEnemy (AudioClip clip)
	{
		efxenemySource.clip = clip;
		efxenemySource.Play ();


	}
	

}
