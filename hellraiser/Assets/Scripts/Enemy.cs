﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public SketelonHealth skeletonsc; 

	private IEnemyState currentState;

	public AudioClip bowstring;

	public GameObject Target {
		get;
		set;
	}


	// Use this for initialization
	public void Start () {


		Physics2D.IgnoreLayerCollision (9, 14);

		ChangeState (new IdleState ());
	}


	
	// Update is called once per frame
	void Update () {

		if (skeletonsc.skeletonHealth <= 0) 
		{
			movementSpeed = 0;
		}

			currentState.Execute ();

			LookAtTarget (); 

	}

	private void LookAtTarget()
	{
		if (Target != null) 
		{
			float xDir = Target.transform.position.x - transform.position.x;

			if (xDir < 0 && !facingRight || xDir > 0 && facingRight) 
			{
				ChangeDirection ();
			}
		}
	} 
	 
	public void ChangeState(IEnemyState newState)
	{

		if (currentState != null) 
		{

			currentState.Exit ();
		}

		currentState = newState;

		currentState.Enter (this);

	}

	public void Move()
	{
			enemyAnimator.SetFloat ("speed", 1);

			transform.Translate (GetDirection () * (movementSpeed * Time.deltaTime));
	}

	public void Stop()
	{

		enemyAnimator.SetFloat ("speed", 0);
		transform.Translate(GetDirection () * (movementSpeed * 0));
	}

	public Vector2 GetDirection()
	{
		return facingRight ? Vector2.left : Vector2.right;
	}
		

	void OnTriggerEnter2D(Collider2D other)
	{

		currentState.OnTriggerEnter2D (other);

	}
			


}



