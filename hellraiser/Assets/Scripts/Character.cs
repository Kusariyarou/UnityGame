using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {



	public Animator enemyAnimator;

	[SerializeField]
	protected Transform bowPos;

	[SerializeField]
	protected float movementSpeed;

	protected bool facingRight;

	[SerializeField]
	private GameObject greenarrowPrefab;


	private bool bowattack;

	// Use this for initialization
	void Start () {

		facingRight = true;

		enemyAnimator = GetComponent<Animator>();





	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeDirection ()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
	}

	public virtual void ThrowBow(int value)
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
