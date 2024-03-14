using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public InteractableTrigger it;

	public string initialTriggerText;
	public string poweredTriggerText;

	public GameObject poweredOffLight;
	public GameObject poweredOnLight;

	private void Start()
	{
		it.interactText = initialTriggerText;
		powerIsOn = false;
	}

	private void OnEnable()
	{
		PowerSwitchStuff.OnPowerActivated += WhenPowered;
		it.OnInteract += OnInteract;
	}

	private void OnDisable()
	{
		PowerSwitchStuff.OnPowerActivated -= WhenPowered;
		it.OnInteract -= OnInteract;
	}

	private void OnDestroy()
	{
		PowerSwitchStuff.OnPowerActivated -= WhenPowered;
	}

	bool powerIsOn;
	void WhenPowered()
	{
		it.interactText = initialTriggerText;
		powerIsOn = true;

		poweredOffLight.SetActive(false);
		poweredOnLight.SetActive(true);
	}

	public void OnInteract()
	{
		if (powerIsOn)
		{
			GoToElevator.instance.Go();
		}
	}
}
