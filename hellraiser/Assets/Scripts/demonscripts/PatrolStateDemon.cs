using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateDemon : IEnemyStateDemon {

	private EnemyDemon enemy;

	private float patrolTimer;

	private float patrolDuration = 10f;

	public void Enter(EnemyDemon enemy)
	{
		this.enemy = enemy;
	}

	public void Execute()
	{

		if (enemy.Target != null) {
			enemy.ChangeState (new MeleeStateDemon ());

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
			enemy.ChangeState (new IdleStateDemon ());
		}

	}
}
