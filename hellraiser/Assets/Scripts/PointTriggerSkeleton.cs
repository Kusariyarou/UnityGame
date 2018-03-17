using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTriggerSkeleton : MonoBehaviour {

	public BoxCollider2D death;

	public SketelonHealth enemyscript;



	Enemy enemyC;

	// Use this for initialization
	void Start () {



		enemyC = GetComponent<Enemy> ();

		death.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (enemyscript.skeletonHealth <= 0f)
			
		{
			death.enabled = true;
		}






		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Destroy (enemyscript.gameObject);
		}
	}
}
