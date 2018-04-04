using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour {
	public static bool GameIsPaused = false;

	public GameObject finishmenuUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		finishmenuUI.SetActive (true);
		Time.timeScale = 0f;
		AudioListener.pause = true;
		GameIsPaused = true;
	}

	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}

	public void NextLevel()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene ("level1-5");
	}
}
