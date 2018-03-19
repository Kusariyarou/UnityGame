using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFire : MonoBehaviour {

	private Animator skullAnimator;

	float timer;

	public float waitingTime = 5f; 

	private bool facingdown;

	[SerializeField]
	private Transform fireballPos;

	[SerializeField]
	private GameObject fireballPrefab;

	// Use this for initialization
	void Start () {
		 
		facingdown = true;

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

	public void FireBallThrow (int value)
	{
		

		if (facingdown) {
			GameObject tmp = (GameObject)Instantiate (fireballPrefab, fireballPos.position, Quaternion.Euler (new Vector2 (0, 0)));
			tmp.GetComponent<FireBall> ().Initialize (Vector2.down);  
		} 

	}







}
