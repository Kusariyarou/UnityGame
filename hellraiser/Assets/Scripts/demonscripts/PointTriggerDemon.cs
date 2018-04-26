using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTriggerDemon : MonoBehaviour {

	public BoxCollider2D death;



	public DemonHealth demonscript;


	EnemyDemon enemyD;
	// Use this for initialization
	void Start () {

		Physics2D.IgnoreLayerCollision (4, 10);
		enemyD = GetComponent<EnemyDemon> ();

		death.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {



		if (demonscript.demonHealth <= 0f)

		{
			death.enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Destroy (demonscript.gameObject);
		}
	}
}
