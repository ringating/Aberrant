using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
	public delegate void PlayerEnteredTriggerAction();
	public event PlayerEnteredTriggerAction OnPlayerEnteredTrigger;

	public delegate void PlayerExitedTriggerAction();
	public event PlayerExitedTriggerAction OnPlayerExitedTrigger;

	public delegate void InteractAction();
	public event InteractAction OnInteract;

	List<Collider> overlappingColliders;

	private void Awake()
	{
		overlappingColliders = new List<Collider>();
	}

	private void OnTriggerEnter(Collider other)
	{
		overlappingColliders.Add(other);

		Player player = other.GetComponent<Player>();
		if (player)
		{
			OnPlayerEnteredTrigger?.Invoke();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		bool existed = overlappingColliders.Remove(other);

		if (!existed) 
		{
			Debug.LogWarning($"collider \"{other.name}\" left my trigger without ever entering it! wow!");
		}

		Player player = other.GetComponent<Player>();
		if (player)
		{
			OnPlayerExitedTrigger?.Invoke();
		}
	}

	public void Interact()
	{
		OnInteract?.Invoke();
	}
}
