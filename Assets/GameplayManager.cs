using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
	public static GameplayManager instance;

	public enum GameState 
	{
		aboveGround,
		elevator,
		floorPrePower,
		floorPostPower
	}

	[HideInInspector] public GameState currentGameState;

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
	}

	public void Door(Door doorToEnter)
	{
		// teleport player to other door/room
		// TODO

		//play the transition animation for the camera
		// TODO

		//switch active cameras
		// TODO
	}
}
