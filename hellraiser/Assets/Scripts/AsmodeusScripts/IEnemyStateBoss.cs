using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStateBoss
{

	void Execute ();
	void Enter (EnemyBoss enemy);
	void Exit ();
	void OnTriggerEnter2D(Collider2D other);

}
