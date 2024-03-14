using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
	public static GameplayManager instance;

	public enum GameState 
	{
		aboveGround,
		elevator,
		prePower,
		postPower
	}

	[HideInInspector] public GameState currentGameState;
	[HideInInspector] public Room currentRoom;
	//public Camera currentCamera { get { return currentRoom ? currentRoom.myCamera : null; } }
	[HideInInspector] public Transform currentLevelRoot;

	public LevelSettings[] floorSettings;

	int floorIndex;

	private void Awake()
	{
		// singleton stuff
		if (instance)
		{
			Debug.LogError("An instance of GameplayManager already exists! Destroying this instance!");
			Destroy(this);
			return;
		}
		instance = this;
	}

	private void Start()
	{
		currentGameState = GameState.aboveGround;

		floorIndex = -1;

		// TESTING GENERATION
		currentLevelRoot = LevelGenerator.instance.SpawnLevel(floorSettings[0]);
	}

	public void UseDoor(Door doorToEnter)
	{
		// teleport player to other door/room + transition
		CRTCameraSwitcher.instance.SwitchTo(doorToEnter.myRoom.myCamera, doorToEnter.playerTeleportPoint);
	}

	public Camera GetCameraOfCurrentRoom(out CRTCamera crtCam)
	{
		Camera cam = GetCameraOfCurrentRoom();
		crtCam = cam.gameObject.GetComponent<CRTCamera>();
		return cam;
	}

	public Camera GetCameraOfCurrentRoom()
	{
		return currentRoom ? currentRoom.myCamera : null;
	}

	public GameObject youWon;
	public bool GenerateNextFloor() // returns false if every floor has already been generated
	{
		floorIndex++;

		if (floorIndex < floorSettings.Length)
		{
			currentLevelRoot = LevelGenerator.instance.SpawnLevel(floorSettings[floorIndex]);
			return true;
		}
		else
		{
			youWon.SetActive(true);
			print("you won!");
			return false;
		}
	}
}
