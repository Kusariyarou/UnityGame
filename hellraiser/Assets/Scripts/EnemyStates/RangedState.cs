using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState {

	private Enemy enemy;

	private float throwTimer;
	private float throwCoolDown = 4f;
	private bool canThrow = true;


	public void Enter(Enemy enemy)
	{
		this.enemy = enemy;
	}

	public void Execute()
	{
		

		if (enemy.Target != null) 
		{
			ThrowBow ();
		}
		else 
		{
			enemy.ChangeState (new IdleState ());
		}


	}

	public void Exit()
	{

	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		
			
	}

	private void ThrowBow()
	{
		enemy.Stop ();

		if (canThrow) {
			enemy.enemyAnimator.SetTrigger ("BowAttack");
			throwTimer = 0f;
			canThrow = false;
			enemy.ChangeState (new IdleState ());
		} else {
			throwTimer += Time.deltaTime; 
			if (throwTimer >= throwCoolDown)  
			{
				canThrow = true;
			}
			else enemy.ChangeState (new IdleState ());
		}
	}








		
}
