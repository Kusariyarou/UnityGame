using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {


	public Player playerScript; 

	public CanvasGroup myCanvas;

	public Text deadtext;

	public Text deadpoint;

	public Image sulfur;

	public Button restart;

	public Button menu;



	// Use this for initialization
	void Start () {
		
		deadpoint.enabled = false;

		deadtext.enabled = false;

		sulfur.enabled = false;

		menu.gameObject.SetActive (false);

		restart.gameObject.SetActive (false);




	}
	
	// Update is called once per frame
	void Update () {


		if (playerScript.currentHeahlt <= 0) {

			StartCoroutine ("FadeIn");
			GameObject.Find ("Canvas").GetComponent<PauseMenu> ().enabled = false;
		}



	} 

	IEnumerator FadeIn()
	{

		while (myCanvas.alpha < 1) {
			myCanvas.alpha += Time.deltaTime / 5;
			yield return new WaitForSeconds (5f);
			deadpoint.enabled = true;

			deadtext.enabled = true;

			sulfur.enabled = true;

			menu.gameObject.SetActive (true);

			restart.gameObject.SetActive (true);




			yield return null;
		}




	}






}
