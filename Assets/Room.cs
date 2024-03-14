using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	[HideInInspector] public List<Door> allDoors { get; set; } // initially populated in Door's awake
	[HideInInspector] public List<Enemy> allEnemies; // initially populated in awake
	[HideInInspector] public Camera myCamera; // initially populated in awake

	private void Awake()
	{
		UpdateAllDoorsList();
		UpdateMyCameraAndDisableIt();
		UpdateAllEnemiesList();
	}

	public void UpdateAllEnemiesList()
	{
		allDoors = new List<Door>(GetComponentsInChildren<Door>());

		if (allDoors.Count == 0)
		{
			Debug.LogError("no possible enemies found for this room!");
		}
	}

	public List<Door> GetAllDisconnectedDoors()
	{
		List<Door> disconnectedDoors = new List<Door>();

		foreach (Door d in allDoors)
		{
			if (!d.connected)
			{
				disconnectedDoors.Add(d);
			}
		}

		return disconnectedDoors;
	}

	public Door GetRandomDisconnectedDoor()
	{
		List<Door> disconnectedDoors = GetAllDisconnectedDoors();

		if (disconnectedDoors.Count == 0)
		{
			Debug.LogError($"Can't GetRandomDisconnectedDoor() because this room has no disconnected doors! This shouldn't happen!");
			return null;
		}

		return disconnectedDoors[Random.Range(0, disconnectedDoors.Count)];
	}

	/*public List<Door> GetAllConnectedDoors()
	{
		// ...I don't think this will actually ever be needed.
	}*/

	public void DestroyAllDisconnectedDoors()
	{
		List<Door> toDestroy = GetAllDisconnectedDoors();

		foreach (Door d in toDestroy)
		{
			Destroy(d.gameObject);
			allDoors.Remove(d); // keep the allDoors list up to date (no stale references)
		}
	}

	private void UpdateAllDoorsList()
	{
		// get all my doors, and give them each a reference to me
		allDoors = new List<Door>(GetComponentsInChildren<Door>());
		foreach (Door d in allDoors)
		{
			d.myRoom = this;
		}

		if (allDoors.Count == 0) 
		{
			Debug.LogError("no doors found for this room!");
		}
	}

	private void UpdateMyCameraAndDisableIt()
	{
		myCamera = GetComponentInChildren<Camera>(true);

		if (myCamera == null)
		{
			Debug.LogError("no camera found for this room!");
			return;
		}

		myCamera.enabled = false;
	}

	public void SwitchToMyCamera(/* TODO */)
	{
		// TODO
	}
}
