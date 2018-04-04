using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightBoss : MonoBehaviour {

	[SerializeField]
	private EnemyBoss enemy;

	void Start()
	{
		Physics2D.IgnoreLayerCollision (10, 11);
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "Player") 
		{
			enemy.Target = other.gameObject;

			 

		}




	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			enemy.Target = null;
		}
	}
}
