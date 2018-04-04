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
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);


	}


}
