using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStateDemon : IEnemyStateDemon {

	private EnemyDemon enemy;

	private float attackTimer;

	private float attackCoolDown = 3;

	private bool canAttack; 

	public void Enter(EnemyDemon enemy)
	{

		this.enemy = enemy;

	}

	public void Execute()
	{

		DemonMeleeAttack ();
		if (enemy.Target != null) {
			 


		} else 
		{
			enemy.ChangeState (new PatrolStateDemon ());
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
		enemy.enemyAnimator.SetFloat ("speed", 0);
		attackTimer += Time.deltaTime; 

		if (attackTimer >= attackCoolDown)  
		{
			canAttack = true;
			attackTimer = 0f;  
		}

		if (canAttack)  
		{
			canAttack = false; 
			enemy.enemyAnimator.SetTrigger ("demonattack"); 
	}

}
}
