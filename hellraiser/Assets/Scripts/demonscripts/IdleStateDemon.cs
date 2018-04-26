using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateDemon : IEnemyStateDemon {

	private EnemyDemon enemy;

	private float idleTimer;

	private float idleDuration = 5;

	public void Enter(EnemyDemon enemy)
	{
		this.enemy = enemy;
	}

	public void Execute()
	{
		if (enemy.Target != null) 
		{
			enemy.ChangeState (new MeleeStateDemon ());
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
			enemy.ChangeState (new PatrolStateDemon ());
		}

	}
}
