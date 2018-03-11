using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour {

	public CapsuleCollider2D stand;
	public BoxCollider2D crouch;

	Player playerC;  // player script
	// Use this for initialization
	void Start () {

		playerC = GetComponent<Player> ();
		stand.enabled = true;
		crouch.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (playerC.groundCheck == false) 
		{
			stand.enabled = true;
			crouch.enabled = false;
		}

		else
		{
			if (playerC.crouching == true) 
			{
				stand.enabled = false;
				crouch.enabled = true;
			} 
			else 
			{
				stand.enabled = true;
				crouch.enabled = false;
			}
		
	}
}
}
