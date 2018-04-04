using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketelonHealth : Character {


	public GameObject blood;

	public AudioClip damageSound;
	  
	CapsuleCollider2D skeletonCapsule;

	public float skeletonHealth = 50f;

	private SpriteRenderer playerSprite;
	public Material[] material;
	Renderer rend;

	private Enemy enemy; 

	  

	private bool flashActive;
	public float flashLength;
	private float flashCounter;


	// Use this for initialization
	void Start () {


		Physics2D.IgnoreLayerCollision (9, 13, false);
		Physics2D.IgnoreLayerCollision (9, 16, false);


		playerSprite = GetComponent<SpriteRenderer> ();

		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		rend.sharedMaterial = material [0];

	}
	
	// Update is called once per frame
	void Update () {

		if (flashActive) 
		{
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
			}

			flashCounter -= Time.deltaTime;


		}

		if (skeletonHealth <= 0f) 
		{
			
			enemyAnimator.SetTrigger ("die");

			Physics2D.IgnoreLayerCollision (9, 13);
			Physics2D.IgnoreLayerCollision (9, 16);

		}
		if (skeletonHealth > 0f) 
		{
			Physics2D.IgnoreLayerCollision (9, 13, false);
			Physics2D.IgnoreLayerCollision (9, 16, false);

		}

	}





	void OnTriggerEnter2D(Collider2D other)
	{



		if (other.gameObject.tag == "Arrow") {

			Instantiate (blood, transform.position, Quaternion.identity);
			skeletonHealth -= greenarrowscript.greenarrowDamage;
			flashActive = true;
			flashCounter = flashLength;
			SoundManager.instance3.PlaySingleEnemy (damageSound);

		}

		if (other.gameObject.tag == "MeleeAttack") {
			Instantiate (blood, transform.position, Quaternion.identity);
			skeletonHealth -= playerScript.fastattackDamage;
			flashActive = true;
			flashCounter = flashLength;
			SoundManager.instance3.PlaySingleEnemy (damageSound);
		}








	}
}
