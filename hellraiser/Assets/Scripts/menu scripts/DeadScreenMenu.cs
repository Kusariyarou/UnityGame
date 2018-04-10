using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreenMenu : MonoBehaviour {

	public void ReturnMainMenu()

	{
		SceneManager.LoadScene ("MainMenu");


	}

	public void Restart()
	{
		int index = Random.Range (1, 4);
		SceneManager.LoadScene (index);


	}


}
