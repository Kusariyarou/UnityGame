using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	public GameObject enemy;
	public Transform enemypos;
	public Camera camera1;
	public Camera camera2;
	public Collider2D wall1;
	public Collider2D wall2;



	// Use this for initialization
	void Start () {

		enemy.SetActive(false);
		camera1.enabled = true;
		camera2.enabled = false;
		wall1.enabled = false;
		wall2.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			
			camera1.enabled = false;
			camera2.enabled = true;
			wall1.enabled = true;
			wall2.enabled = true;
			enemy.SetActive(true);

		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			Destroy (gameObject);
		}
	}


}
