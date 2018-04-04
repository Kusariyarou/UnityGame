using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Asmodeus {


	public BossHealth Bossscript;

	private IEnemyStateBoss currentState;

	public GameObject Target {
		get;
		set;
	}
	[SerializeField]
	private float meleeRange;
	public bool InMeleeRange
	{
		get 
		{
			if (Target != null) 
			{
				return Vector2.Distance (transform.position, Target.transform.position) <= meleeRange;
			}

			return false;
		}
	}

	// Use this for initialization
	public void Start () {


		Physics2D.IgnoreLayerCollision (9, 14);





	

		ChangeState (new IdleStateBoss ());
	}


	
	// Update is called once per frame
	void Update () {

		if(Bossscript.bossHealth <= 0)
		{
			movementSpeed = 0; }




			currentState.Execute ();

			LookAtTarget (); 




			



	}

	private void LookAtTarget()
	{
		if (Target != null) 
		{
			float xDir = Target.transform.position.x - transform.position.x;

			if (xDir < 0 && facingRight || xDir > 0 && !facingRight) 
			{
				ChangeDirection ();
			}
		}
	} 
	 
	public void ChangeState(IEnemyStateBoss newState)
	{

		if (currentState != null) 
		{

			currentState.Exit ();
		}

		currentState = newState;

		currentState.Enter (this);

	}

	public void Move()
	{
		
		
		

			enemyAnimator.SetFloat ("speed", 1);

			transform.Translate (GetDirection () * (movementSpeed * Time.deltaTime));
		
	}





	public Vector2 GetDirection()
	{
		return facingRight ? Vector2.right : Vector2.left;
	}
		

	void OnTriggerEnter2D(Collider2D other)
	{

		currentState.OnTriggerEnter2D (other);




	}
			



	}



