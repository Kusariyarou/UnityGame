using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

	Image healhtbar;
	float maxHealth = 100f;
	public static float health;

	// Use this for initialization
	void Start () {
		healhtbar = GetComponent<Image> ();
		health = maxHealth;
		
	}
	
	// Update is called once per frame
	void Update () {
		healhtbar.fillAmount = health / maxHealth;
		
	}
}
