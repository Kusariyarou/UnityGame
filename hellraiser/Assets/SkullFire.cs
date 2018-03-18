using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFire : MonoBehaviour {

	private Animator skullAnimator;

	float timer;

	int waitingTime = 5; 

	// Use this for initialization
	void Start () {
		 
		skullAnimator = GetComponent<Animator>();
	}
	
	void Update()
	{
		timer += Time.deltaTime;
		if (timer > waitingTime) 
		{
			skullAnimator.SetTrigger ("FireBall");
			timer = 0;
		}
		
	}




}
