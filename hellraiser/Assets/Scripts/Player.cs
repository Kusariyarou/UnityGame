using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	CapsuleCollider2D playerCapsule;			//! player collider
	BoxCollider2D playerBox;				//! crouch collider

	private bool canDown = false;

	private Rigidbody2D myRigidbody;

	private GameObject[] tObjects;		//! içinden geçilebilecek objelerin listesi

	private Animator myAnimator;

	[SerializeField]
	private float movementSpeed;

	private bool fastattack;

	private bool roll;

	private bool facingRight;

	bool grounded = false;

	public Transform groundCheck;

	float groundRadius = 0.2f;

	public float jumpForce = 700f;

	public LayerMask whatIsGround;

	bool doubleJump = false;

	public bool crouching = false;









	// Use this for initialization
	void Start () {

		facingRight = true;
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator>();
		playerCapsule = GetComponent<CapsuleCollider2D>();				//! capsule collider'ı ata
		playerBox = GetComponent<BoxCollider2D>();						//! box collider'ı ata
		tObjects = GameObject.FindGameObjectsWithTag("DownThrough");	//! içinden geçilcek objeleri bul, listeye at

	}

	void Update()
	{
		
		if ((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.Space))
		{
			myAnimator.SetBool ("Ground", false);

			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));

			if (!doubleJump && !grounded)
				doubleJump = true;
		}



		if ((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.JoystickButton0))
		{
			myAnimator.SetBool ("Ground", false);

			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));

			if (!doubleJump && !grounded)
				doubleJump = true;
		}




		HandleInput ();



	}

	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		myAnimator.SetBool ("Ground", grounded);

		if (grounded)
			doubleJump = false;

		myAnimator.SetFloat ("vSpeed", GetComponent<Rigidbody2D> ().velocity.y);

		float horizontal = Input.GetAxis ("Horizontal");



		HandleMovement (horizontal);



		Flip (horizontal);

		HandleAttacks ();

		ResetValues ();
	}


	private void HandleMovement (float horizontal)
	{
		if (!myAnimator.GetBool("roll") && (myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Run") ||
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName("JumpAndFall"))) 
		{
			
		
			myRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myRigidbody.velocity.y);
		}

		if (roll && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Roll")) {
			myAnimator.SetBool ("roll", true);
		} else if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Roll"))
		{
			myAnimator.SetBool ("roll", false);
		}


		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));

	

		
	}

	private void HandleAttacks()
	{
		if (fastattack && myAnimator.GetCurrentAnimatorStateInfo(0).IsTag ("Attack"))
		{
			myAnimator.SetTrigger ("lightattack");
			myRigidbody.velocity = Vector2.zero;
		}


	
	}


	private void HandleInput()
	{
		


		if (Input.GetKeyDown (KeyCode.Mouse0)) 
		{
			fastattack = true;
		}

		if (Input.GetKeyDown (KeyCode.JoystickButton2))
		{
			fastattack = true;
		}

		if (Input.GetKeyDown (KeyCode.LeftControl))
		{
			roll = true;
		}

		if (Input.GetKeyDown (KeyCode.JoystickButton1)) 
		{
			roll = true;
		}

		if (Input.GetAxis ("Vertical") < 0 && canDown && Input.GetKeyDown (KeyCode.Space))  //! aşağı ok butonunu al
		{
			StartCoroutine ("JumpDown");		//! JumpDown' çalıştır.
		}

		if (Input.GetAxis ("Vertical") < 0 && canDown && Input.GetKeyDown (KeyCode.JoystickButton0))  //! aşağı ok butonunu al
		{
			StartCoroutine ("JumpDown");		//! JumpDown' çalıştır.
		}
			

		if (Input.GetAxis ("Vertical") < 0)   // crouch true false yapma
			crouching = true;
		if (crouching && Input.GetAxis ("Vertical") > -1 )
			crouching = false;

		if (crouching && myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			myAnimator.SetBool ("crouching", true);
		} else if (!crouching && myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Crouch"))
		{
			myAnimator.SetBool ("crouching", false);
		}

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

	private void ResetValues()
	{

		fastattack = false;

		roll = false;


	}

	private IEnumerator JumpDown(){
		foreach (GameObject go in tObjects) {		//! İçinden geçilebilir objeler için
			Physics2D.IgnoreCollision (playerCapsule,  go.GetComponent<Collider2D> ());		//! box collider çarpışmasını yoksay
			Physics2D.IgnoreCollision (playerBox, go.GetComponent<Collider2D> ());	//! circle collider çarpışmasını yoksay
		}
		myRigidbody.velocity = new Vector3(0, -10, 0);		//! yere inme hızı ver
		yield return new WaitForSeconds (0.4f);			//!0.5 saniye bekle
		foreach (GameObject go in tObjects) {		//! collider çarpışmalarını eski haline getir
			Physics2D.IgnoreCollision (playerCapsule,  go.GetComponent<Collider2D> (), false);
			Physics2D.IgnoreCollision (playerBox, go.GetComponent<Collider2D> (), false);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "DownThrough")
			canDown= true;

	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "DownThrough")
			canDown= false;

	}
}
