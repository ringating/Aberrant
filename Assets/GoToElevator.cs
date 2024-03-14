using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToElevator : MonoBehaviour
{
	public static GoToElevator instance;

	public Camera elevatorCam;
	public Transform elevatorTeleport;

	private void Awake()
	{
		// singleton stuff
		if (instance)
		{
			Debug.LogError("An instance of GoToElevator already exists! Destroying this instance!");
			Destroy(this);
			return;
		}
		instance = this;
	}

	public void Go()
	{
		CRTCameraSwitcher.instance.SwitchTo(elevatorCam, elevatorTeleport);
	}
}
