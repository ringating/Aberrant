using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorRideTrigger : MonoBehaviour
{
	public Animation elevatorRideAnimation;

	private void OnTriggerEnter(Collider other)
	{
		Player p = other.GetComponent<Player>();

		if (p)
		{
			print("time to ride the elevator!");
			if (elevatorRideAnimation.isPlaying) elevatorRideAnimation.Stop();
			elevatorRideAnimation.Play();
		}
	}
}
