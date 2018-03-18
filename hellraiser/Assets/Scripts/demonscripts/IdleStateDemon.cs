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
		Debug.Log ("I'm Idling");

		Idle ();

		if (enemy.Target != null) 
		{
			enemy.ChangeState (new MeleeStateDemon ());


		}

	}

	public void Exit()
	{

	}

	public void OnTriggerEnter2D(Collider2D other)
	{

	}

	private void Idle()
	{


		enemy.enemyAnimator.SetFloat ("speed", 0);

		idleTimer += Time.deltaTime;

		if (idleTimer >= idleDuration) 
		{
			enemy.ChangeState (new PatrolStateDemon ());
		}

	}
}
