using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PauseMenu : MonoBehaviour {
	public static bool GameIsPaused = false;

	public GameObject pausemenuUI;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) 
		{
			if (GameIsPaused) {
				Resume ();
			} 
			else 
			{
				Pause ();
			}
		}
		
	}

	public void Resume()
	{
		pausemenuUI.SetActive (false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		AudioListener.pause = false;

	}

	void Pause()
	{
		pausemenuUI.SetActive (true);
		Time.timeScale = 0f;
		GameIsPaused = true;
		AudioListener.pause = true;
	}

	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}
}
