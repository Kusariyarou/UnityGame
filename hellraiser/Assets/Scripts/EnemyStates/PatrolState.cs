﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState {

	private Enemy enemy;

	private float patrolTimer;

	private float patrolDuration= 5f;

	public void Enter(Enemy enemy)
	{
		this.enemy = enemy;
	}

	public void Execute()
	{
		if (enemy.Target != null) {
			enemy.ChangeState (new RangedState ());
		} else {
			Patrol ();
		}
	}

	public void Exit()
	{

	}

	public void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "Edge") 
		{
			enemy.ChangeDirection ();
		}

	}

	private void Patrol()
	{
		enemy.Move ();
		patrolTimer += Time.deltaTime;

		if (patrolTimer >= patrolDuration) 
		{
			enemy.ChangeState (new IdleState ());
		}

	}
}
