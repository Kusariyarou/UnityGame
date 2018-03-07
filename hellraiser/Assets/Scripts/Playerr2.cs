using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerr2 : MonoBehaviour {

	private Rigidbody2D myRigidbody;

	private Animator myAnimator;

	public float walkSpeed = 3.0f;

	public float runSpeed = 7.0f;

	private float currentMaxSpeed = 0;


	private bool facingRight;






	// Use this for initialization
	void Start () {

		facingRight = true;
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float horizontal = Input.GetAxis ("Horizontal");

		if (Input.GetKey (KeyCode.LeftShift)) {
			currentMaxSpeed = runSpeed;
		} else {
			currentMaxSpeed = walkSpeed;
		}


		float speed = currentMaxSpeed * Input.GetAxis ("Horizontal");
		myAnimator.SetFloat ("speed", Mathf.Abs (speed));
		transform.Translate (Vector2.right * speed * Time.deltaTime);


		HandleMovement (horizontal);



		Flip (horizontal);
	}


	private void HandleMovement (float horizontal)
	{
		

		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));

	}



	private void Flip (float horizontal)
	{

		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) 
		{
			facingRight = !facingRight;

			Vector3 theScale = transform.localScale;

			theScale.x *= -1;

			transform.localScale = theScale;
		}


	}
}
