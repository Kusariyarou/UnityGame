using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState {

	private Enemy enemy;

	private float idleTimer;

	private float idleDuration=3f;

	public void Enter(Enemy enemy)
	{
		this.enemy = enemy;
	}

	public void Execute()
	{
		if (enemy.Target != null) 
		{
			enemy.ChangeState (new RangedState ());
		}
		else Idle ();

	}

	public void Exit()
	{

	}

	public void OnTriggerEnter2D(Collider2D other)
	{

	}

	private void Idle()
	{

		enemy.Stop ();

		idleTimer += Time.deltaTime;

		if (idleTimer >= idleDuration) 
		{
			enemy.ChangeState (new PatrolState ());
		}

	}
}
