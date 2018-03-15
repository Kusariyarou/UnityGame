using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState {

	private Enemy enemy;

	private float throwTimer;
	private float throwCoolDown = 2;
	private bool canThrow;

	public void Enter(Enemy enemy)
	{
		this.enemy = enemy;
	}

	public void Execute()
	{
		ThrowBow ();

		if (enemy.Target != null) {



				



		} else 
		{
			enemy.ChangeState (new PatrolState ());
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

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			canThrow = true;

	}




		
}
