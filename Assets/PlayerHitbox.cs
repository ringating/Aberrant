using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
	public Player player;

	private void OnTriggerEnter(Collider other)
	{
		HittableByPlayer hittableByPlayer = other.GetComponent<HittableByPlayer>();

		if (hittableByPlayer)
		{
			hittableByPlayer.Hit();
		}
	}
}
