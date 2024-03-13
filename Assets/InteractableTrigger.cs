using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
	public string interactText = "press E to interact";

	public delegate void PlayerEnteredTriggerAction();
	public event PlayerEnteredTriggerAction OnPlayerEnteredTrigger;

	public delegate void PlayerExitedTriggerAction();
	public event PlayerExitedTriggerAction OnPlayerExitedTrigger;

	public delegate void InteractAction();
	public event InteractAction OnInteract;

	List<Collider> overlappingColliders;

	public static InteractableTrigger active;

	private void Awake()
	{
		overlappingColliders = new List<Collider>();
		active = null;
	}

	private void OnTriggerEnter(Collider other)
	{
		overlappingColliders.Add(other);

		Player player = other.GetComponent<Player>();
		if (player)
		{
			InteractText.instance.tmp.text = interactText;
			active = this;
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
			InteractText.instance.tmp.text = "";
			active = null;
			OnPlayerExitedTrigger?.Invoke();
		}
	}

	public void Interact()
	{
		OnInteract?.Invoke();
	}
}
