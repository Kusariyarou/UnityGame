using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStateDemon 
{

	void Execute ();
	void Enter (EnemyDemon enemy);
	void Exit ();
	void OnTriggerEnter2D(Collider2D other);

}
