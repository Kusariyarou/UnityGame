using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStateBoss : IEnemyStateBoss {

	private EnemyBoss enemy;

	private float shieldTimer;

	private float ShieldDuration = 5f;



	 

	public void Enter(EnemyBoss enemy)
	{

		this.enemy = enemy;


	}

	public void Execute()
	{

		ShieldAttack ();



		
	}

	public void Exit()
	{
		
	}

	public void OnTriggerEnter2D(Collider2D other) 
	{
		
	}

	private  void ShieldAttack()
	{
		enemy.enemyAnimator.SetFloat ("speed", 0);
		enemy.enemyAnimator.SetBool ("Shield", true);
		shieldTimer += Time.deltaTime;

		if (shieldTimer >= ShieldDuration) 
		{
			enemy.enemyAnimator.SetBool ("Shield", false);

			enemy.ChangeState (new IdleStateBoss ());
		}


}
}
