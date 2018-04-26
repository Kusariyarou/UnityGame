﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	SoundManager smanager;

	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip bowstring;
	public AudioClip jumpSound;
	public AudioClip swordSound;
	public AudioClip rollsound;
	public AudioClip yellsound;
	public AudioClip hitsound;


	public GameObject blood;
	public GameObject playerJumpDust;

	[SerializeField]
	private float rollPower;

	[SerializeField]
	private Transform jumpDustPos; 


	public float fastattackDamage;

	public float maxFastAttackDamage = 10f;

	public EdgeCollider2D FastAttackCollider;

	CapsuleCollider2D playerCapsule;			//! player collider
	BoxCollider2D playerBox;				//! crouch collider


	private SpriteRenderer playerSprite;
	public Material[] material;
	Renderer rend;

	public string pointString;

	public float currentPoint = 0f; 

	public Text pointText;


	private bool canDown = false;

	private Rigidbody2D myRigidbody;

	private GameObject[] tObjects;		//! içinden geçilebilecek objelerin listesi

	private Animator myAnimator;

	[SerializeField]
	private Transform bowPos;

	[SerializeField]
	public float movementSpeed;

	private bool fastattack;

	private bool rolling;

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

	public int startingHealth = 100;

	public int currentHeahlt;

	public string healthString;

	public Text healthText;


	private bool flashActive;
	public float flashLength;
	private float flashCounter;


	// Use this for initialization
	void Start () {

		AudioListener.pause = false;

		InvokeRepeating ("PlaySound", 0.0f, 0.5f);

		currentHeahlt = startingHealth;

		fastattackDamage = maxFastAttackDamage;


		Physics2D.IgnoreLayerCollision (8, 10, false); 
		playerSprite = GetComponent<SpriteRenderer> ();

		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		rend.sharedMaterial = material [0];

		Physics2D.IgnoreLayerCollision (4, 14);
		Physics2D.IgnoreLayerCollision (4, 8);
		Physics2D.IgnoreLayerCollision (8, 9);
		Physics2D.IgnoreLayerCollision (8, 14);
		Physics2D.IgnoreLayerCollision (10, 16);

		facingRight = true;
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator>();
		playerCapsule = GetComponent<CapsuleCollider2D>();				//! capsule collider'ı ata
		playerBox = GetComponent<BoxCollider2D>();						//! box collider'ı ata
		tObjects = GameObject.FindGameObjectsWithTag("DownThrough");	//! içinden geçilcek objeleri bul, listeye at

	}

	void Update()
	{
		
		if (flashActive) 
		{
			Physics2D.IgnoreLayerCollision (8, 16);
			Physics2D.IgnoreLayerCollision (8, 15);

			if (flashCounter > flashLength * .66f) {

				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);

			} 
			else if (flashCounter > flashLength * .33f) {
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
				rend.sharedMaterial = material [1];
			} 
			else if (flashCounter > 0f) 
			{
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);

			}
			else  
			{
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);

			
				flashActive = false;
				rend.sharedMaterial = material [0];
				foreach (GameObject go in tObjects) 
				{
					Physics2D.IgnoreLayerCollision( 8, 16 , false); 
				
					Physics2D.IgnoreLayerCollision (8, 15 , false);

					
				}
			}

			flashCounter -= Time.deltaTime;

		
		}

		pointString = currentPoint.ToString ();
		pointText.text = pointString;

		healthString = currentHeahlt.ToString ();
		healthText.text = healthString;

		if (currentHeahlt <= 0) 
		{
			Physics2D.IgnoreLayerCollision (8, 10);
			myAnimator.SetTrigger ("die");
			jumpForce = 0;


		}


		
		if ((grounded || !doubleJump) && Input.GetButtonDown ("Jump"))
		{
			if (currentHeahlt > 0) 
			{
				SoundManager.instance.PlaySingle (jumpSound);
				Instantiate (playerJumpDust, jumpDustPos.position, Quaternion.identity);
			}
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
		
		if ((myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Run") ||
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("WalkCharacter") ||
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName("JumpAndFall"))) 

		{
			
			myRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myRigidbody.velocity.y);

		}

		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));
	
	}

	private void HandleAttacks()
	{
		if (rolling && (myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle") ||
			myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Run") ||
			myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("WalkCharacter"))) {
			SoundManager.instance.PlaySingle (rollsound);
			myAnimator.SetTrigger ("dodge");
			myRigidbody.velocity = Vector2.zero; 
			if (facingRight) {
				myRigidbody.AddForce (new Vector2 (1, 0) * rollPower);
			} else 
			{
				myRigidbody.AddForce (new Vector2(-1, 0) * rollPower);
			}

		}

		if (fastattack && (myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle") ||
		    myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Run") ||
		    myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("WalkCharacter"))) {
			SoundManager.instance.PlaySingle (swordSound);
			SoundManager.instance2.PlaySingle2 (yellsound);
			myAnimator.SetTrigger ("lightattack");
			myRigidbody.velocity = Vector2.zero;

		} 

		if (bowattack && (myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Idle") || 
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Run") ||
			myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("WalkCharacter")))
		{
			SoundManager.instance.PlaySingle (bowstring);
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
			
			rolling = true;
		}



		if (Input.GetAxis ("Vertical") < 0 && canDown && Input.GetButtonDown ("Jump"))  //! aşağı ok butonunu al
		{
			StartCoroutine ("JumpDown");		//! JumpDown' çalıştır.
		}
			
			

		if (Input.GetAxis ("Vertical") < 0) {   // crouch true false yapma
			if (myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				myAnimator.SetBool ("crouching", true);
				crouching = true;
			}
		} else if(crouching && Input.GetAxis ("Vertical") > -1){
			myAnimator.SetBool ("crouching", false);
			crouching = false;
		}



	}


	private void Flip (float horizontal)
	{
		if (currentHeahlt > 0) {
			if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
				facingRight = !facingRight;

				Vector3 theScale = transform.localScale;

				theScale.x *= -1;

				transform.localScale = theScale;
			}
		}
	}

	private void ResetValues()
	{

		fastattack = false;

		rolling = false;

		bowattack = false;

	}

	void PlaySound()
	{
		if (grounded && Input.GetAxis("Horizontal") != 0 && !(myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Crouch") ||
			myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Bow") ||
			myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Lightsword")))
			{
			
			SoundManager.instance.PlaySingle (moveSound1); 

			}
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

		if (coll.gameObject.tag == "Mace") 
		{
			HealthBarScript.health -= 13f;
			currentHeahlt = currentHeahlt - 13;
			flashActive = true;
			flashCounter = flashLength;
			Instantiate (blood, transform.position, Quaternion.identity);
			SoundManager.instance2.PlaySingle2 (hitsound);


		}

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

	public void FastAttack()
	{
		FastAttackCollider.enabled = !FastAttackCollider.enabled;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Pile") 
		{
			currentHeahlt = currentHeahlt - 10;
			flashActive = true;
			flashCounter = flashLength;
			Instantiate (blood, transform.position, Quaternion.identity);
			HealthBarScript.health -= 10f;
			SoundManager.instance2.PlaySingle2 (hitsound);

		}
		if (other.gameObject.tag == "EnemyArrow") 
		{
			currentHeahlt = currentHeahlt - 12;
			flashActive = true;
			flashCounter = flashLength;
			Instantiate (blood, transform.position, Quaternion.identity);
			HealthBarScript.health -= 12f;
			SoundManager.instance2.PlaySingle2 (hitsound);

		} 

		if (other.gameObject.tag == "PointTag") 
		{
			currentPoint = currentPoint + 7; 
		}

		if (other.gameObject.tag == "DemonAttack") 
		{
			currentHeahlt = currentHeahlt - 12;
			flashActive = true;
			flashCounter = flashLength;
			Instantiate (blood, transform.position, Quaternion.identity);
			HealthBarScript.health -= 12f;
			SoundManager.instance2.PlaySingle2 (hitsound);
		
		} 

		if (other.gameObject.tag == "AsmodeusAttack") 
		{
			currentHeahlt = currentHeahlt - 15;
			flashActive = true;
			flashCounter = flashLength;
			Instantiate (blood, transform.position, Quaternion.identity);
			HealthBarScript.health -= 15f;
			SoundManager.instance2.PlaySingle2 (hitsound);

		}
			

	}

}
