using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDust : MonoBehaviour {

	public GameObject playerJumpDust;
	[SerializeField]
	private Transform jumpDustPos; 

	// Use this for initialization
	void Start () {


	}
	

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag ==  "jumpdust") { 


			Instantiate (playerJumpDust, jumpDustPos.position, Quaternion.identity); 

		}
	}
}
