using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState {

	private Enemy enemy;

	private float idleTimer;

	private float idleDuration;

	public void Enter(Enemy enemy)
	{
		this.enemy = enemy;

		idleDuration = UnityEngine.Random.Range (1, 10);
	}

	public void Execute()
	{
		Debug.Log ("I'm Idling");

		Idle ();

		if (enemy.Target != null) 
		{
			enemy.ChangeState (new RangedState ());


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
			enemy.ChangeState (new PatrolState ());
		}

	}
}
