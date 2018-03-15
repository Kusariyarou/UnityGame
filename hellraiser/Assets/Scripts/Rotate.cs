using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float degrePerSec = 360f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float rotAmount = degrePerSec * Time.deltaTime;
		float curRot = transform.localRotation.eulerAngles.z;
		transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, curRot + rotAmount));
	}
}
