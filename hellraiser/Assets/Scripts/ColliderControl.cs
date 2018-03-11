using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour {

	public BoxCollider2D stand;
	public BoxCollider2D crouch;
	public CircleCollider2D circle;

	Player playerC;  // player script
	// Use this for initialization
	void Start () {

		playerC = GetComponent<Player> ();
		stand.enabled = true;
		crouch.enabled = false;
		circle.enabled = true;


	
	}
	
	// Update is called once per frame
	void Update () {

		if (playerC.groundCheck == false) 
		{
			stand.enabled = true;
			crouch.enabled = false;
			circle.enabled = false;
		}

		else
		{
			if (playerC.crouching == true) 
			{
				
				stand.enabled = false;
				crouch.enabled = true;
				circle.enabled = true;

			} 
			else 
			{
				stand.enabled = true;
				crouch.enabled = false;
				circle.enabled = true;
			}
		
	}
}
}
