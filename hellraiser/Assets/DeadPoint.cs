using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadPoint : MonoBehaviour {

	public Player player;
	public Text lostText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		lostText.text = "You Lost " + player.currentPoint;
		
	}
}
