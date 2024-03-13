using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HittableByPlayer))]
public class PlaySoundOnHitByPlayer : MonoBehaviour
{
    HittableByPlayer hittableByPlayer;

	public AudioClip audioClip;
	public float volume = 0.5f;

	private void Awake()
	{
        hittableByPlayer = GetComponent<HittableByPlayer>();
    }

	private void OnEnable()
	{
		hittableByPlayer.OnHitByPlayer += PlaySound;
	}

	private void OnDisable()
	{
		hittableByPlayer.OnHitByPlayer -= PlaySound;
	}

	void PlaySound()
    {
		Audio2DSingleton.instance.audioSource.PlayOneShot(audioClip, volume);
    }
}
