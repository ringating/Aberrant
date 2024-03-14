using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitchStuff : MonoBehaviour
{
    public InteractableTrigger it;

	public GameObject on;
	public GameObject off;

	public AudioClipWithVolume powerOnSound;

	public delegate void PowerActivatedAction();
	public static event PowerActivatedAction OnPowerActivated;

	private void OnEnable()
	{
		it.OnInteract += PowerOn;
	}

	private void OnDisable()
	{
		it.OnInteract += PowerOn;
	}

	void PowerOn() 
	{
		print("power switch activated!");

		off.SetActive(false);
		on.SetActive(true);

		Audio2DSingleton.instance.audioSource.PlayOneShot(powerOnSound.audioClip, powerOnSound.volume);

		OnPowerActivated?.Invoke();
	}
}
