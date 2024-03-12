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

	public LevelSettings[] floorSettings;

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

		// TESTING GENERATION
		LevelGenerator.instance.SpawnLevel(floorSettings[0]);
	}

	public void UseElevatorDoor()
	{

	}

	public void UseDoor(Door doorToEnter)
	{
		// teleport player to other door/room
		// TODO

		//play the transition animation for the camera
		// TODO

		//switch active cameras
		// TODO
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
}
