using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStateBoss : IEnemyStateBoss {

	private EnemyBoss enemy;

	private float attackTimer;

	private float attackCoolDown = 3f;

	private bool canAttack = true; 

	public void Enter(EnemyBoss enemy)
	{

		this.enemy = enemy;

	}

	public void Execute()
	{

		DemonMeleeAttack (); 

		if (enemy.Target != null) 
		{
			enemy.Move ();
		} 
		else 
		{
			enemy.ChangeState (new IdleStateBoss ());
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
