using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Asmodeus : MonoBehaviour {

	public Player playerScript; 

	public GreenArrow greenarrowscript;

	public Animator enemyAnimator;



	[SerializeField]
	protected float movementSpeed;

	protected bool facingRight;






	// Use this for initialization
	void Start () {

		facingRight = false;

		enemyAnimator = GetComponent<Animator>();





	}
	
	// Update is called once per frame
	void Update () {
		

		
	}

	public void ChangeDirection ()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
	}




}
