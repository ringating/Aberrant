using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
	public Enemy enemy;

	private void OnTriggerEnter(Collider other)
	{
		print($"enemy OnTriggerEnter by {other.name}");

		PlayerHitbox ph = other.GetComponent<PlayerHitbox>();

		if (ph)
		{
			enemy.HitAndShakeScreenAndFreeze(ph.player.damage);
		}
	}
}
