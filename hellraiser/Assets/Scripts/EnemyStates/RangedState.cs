using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState {

	private Enemy enemy;

	private float throwTimer;
	private float throwCoolDown = 3;
	private bool canThrow;

	public void Enter(Enemy enemy)
	{
		this.enemy = enemy;
	}

	public void Execute()
	{
		ThrowBow ();

		if (enemy.Target != null) {

			enemy.Move ();

		} else 
		{
			enemy.ChangeState (new IdleState ());
		}
	}

	public void Exit()
	{

	}

	public void OnTriggerEnter(Collider2D other)
	{

	}

	private void ThrowBow()
	{

		throwTimer += Time.deltaTime;

		if (throwTimer >= throwCoolDown) 
		{
			canThrow = true;
			throwTimer = 0; 
		}

		if (canThrow) 
		{
			canThrow = false;
			enemy.enemyAnimator.SetTrigger ("BowAttack"); 
		
		}
	}
}
