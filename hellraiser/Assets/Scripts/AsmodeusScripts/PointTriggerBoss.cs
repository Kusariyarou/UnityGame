using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTriggerBoss : MonoBehaviour {

	public BoxCollider2D death;



	public BossHealth bossScript;


	EnemyBoss enemyD;
	// Use this for initialization
	void Start () {

		Physics2D.IgnoreLayerCollision (4, 10);
		enemyD = GetComponent<EnemyBoss> ();

		death.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {



		if (bossScript.bossHealth <= 0f)

		{
			death.enabled = true;
		}




		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Destroy (bossScript.gameObject);
		}
	}
}
