using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireBall : MonoBehaviour {

	[SerializeField]
	private float speed;

	private Rigidbody2D myRigidbody;

	private Vector2 direction;

	public GameObject explosion; 

	// Use this for initialization
	void Start () {

		myRigidbody = GetComponent<Rigidbody2D> ();


		direction = Vector2.down;


		
	}

	void FixedUpdate()
	{

		myRigidbody.velocity = direction * speed;




	}
	
	public void Initialize(Vector2 direction)
	{
		this.direction = direction;


	}

	void OnBecameInvisible()
	{
		Destroy (gameObject);

	}

	void OnTriggerEnter2D(Collider2D other)
	{


		if (other.gameObject.tag == "tile") 
		{
			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}

		if (other.gameObject.tag == "Player") 
		{
			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}

	}




}
