using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GreenArrow : MonoBehaviour {

	public float greenarrowDamage = 10f; 

	[SerializeField]
	private float speed;

	private Rigidbody2D myRigidbody;

	private Vector2 direction;

	// Use this for initialization
	void Start () {



		Physics2D.IgnoreLayerCollision (10, 13);
		Physics2D.IgnoreLayerCollision (13, 15);


		myRigidbody = GetComponent<Rigidbody2D> ();

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
		if (other.gameObject.tag == "Skeleton") 
		{
			Destroy (gameObject);
		}

		if (other.gameObject.tag == "Demon") 
		{
			Destroy (gameObject);
		}

		if (other.gameObject.tag == "Asmodeus") 
		{
			Destroy (gameObject);
		}
	}





}
