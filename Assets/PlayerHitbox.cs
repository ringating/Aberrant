using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		HittableByPlayer hittableByPlayer = other.GetComponent<HittableByPlayer>();

		if (hittableByPlayer)
		{
			hittableByPlayer.Hit();
		}
	}
}
