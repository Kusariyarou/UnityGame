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
	private Transform bowPos;

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

	private bool bowattack;

	[SerializeField]
	private GameObject greenarrowPrefab;



	// Use this for initialization
	void Start () {

		Physics2D.IgnoreLayerCollision (8, 9);

		facingRight = true;
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator>();
		playerCapsule = GetComponent<CapsuleCollider2D>();				//! capsule collider'ı ata
		playerBox = GetComponent<BoxCollider2D>();						//! box collider'ı ata
		tObjects = GameObject.FindGameObjectsWithTag("DownThrough");	//! içinden geçilcek objeleri bul, listeye at

	}

	void Update()
	{
		
		if ((grounded || !doubleJump) && Input.GetButtonDown ("Jump"))
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
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("WalkCharacter") ||
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
		if (fastattack && (myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Idle") || 
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Run") ||
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("WalkCharacter")))
		{
			myAnimator.SetTrigger ("lightattack");
			myRigidbody.velocity = Vector2.zero;
		}

		if (bowattack && (myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Idle") || 
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Run") ||
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("WalkCharacter")))
		{
			myAnimator.SetTrigger ("BowAttack");
			myRigidbody.velocity = Vector2.zero;
		}


	
	}


	private void HandleInput()
	{

		if (Input.GetButtonDown ("Attack2")) 
		{
			bowattack = true;
		}

		if (Input.GetButtonDown ("Attack")) 
		{
			fastattack = true;
		}
			

		if (Input.GetButtonDown ("Roll"))
		{
			roll = true;
		}
			

		if (Input.GetAxis ("Vertical") < 0 && canDown && Input.GetButtonDown ("Jump"))  //! aşağı ok butonunu al
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

		bowattack = false;




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



	public void ThrowGreenBow (int value)
	{
		if (!grounded && value == 1 || grounded && value == 0) 
		{

			if (facingRight) 
			{
				GameObject tmp = (GameObject)Instantiate (greenarrowPrefab, bowPos.position, Quaternion.Euler (new Vector2 (0, 0)));
				tmp.GetComponent<GreenArrow> ().Initialize (Vector2.right);
			} else 
			{
				GameObject tmp = Instantiate (greenarrowPrefab, bowPos.position, Quaternion.Euler (new Vector2 (0, 180)));
				tmp.GetComponent<GreenArrow> ().Initialize (Vector2.left);
			}
		}


	}



}
