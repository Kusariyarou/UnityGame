using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStateDemon : IEnemyStateDemon {

	private EnemyDemon enemy;

	private float attackTimer;

	private float attackCoolDown = 3f;

	private bool canAttack=true; 

	public void Enter(EnemyDemon enemy)
	{

		this.enemy = enemy;

	}

	public void Execute()
	{
		if (enemy.Target != null) {
			DemonMeleeAttack ();
		} else 
		{
			enemy.ChangeState (new IdleStateDemon ());
		}

		
	}

	public void Exit()
	{
		
	}

	public void OnTriggerEnter2D(Collider2D other) 
	{
		
	}


	private void DemonMeleeAttack()
	{
		enemy.Stop ();

		if (canAttack) {
			enemy.enemyAnimator.SetTrigger ("demonattack");
			attackTimer = 0f;
			canAttack = false;
			enemy.ChangeState (new IdleStateDemon ());
		} else {
			attackTimer += Time.deltaTime; 
			if (attackTimer >= attackCoolDown)  
			{
				canAttack = true;
			}
			else enemy.ChangeState (new IdleStateDemon ());
		}

	}

}
