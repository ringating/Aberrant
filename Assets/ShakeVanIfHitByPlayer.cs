using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HittableByPlayer))]
public class ShakeVanIfHitByPlayer : MonoBehaviour
{
    HittableByPlayer hittableByPlayer;

	private void Awake()
	{
		hittableByPlayer = GetComponent<HittableByPlayer>();
	}

	private void OnEnable()
	{
		hittableByPlayer.OnHitByPlayer += ShakeVan;
	}

	private void OnDisable()
	{
		hittableByPlayer.OnHitByPlayer -= ShakeVan;
	}

	private void ShakeVan() 
	{
		Impacter.instance.RandomImpact();
	}
}
