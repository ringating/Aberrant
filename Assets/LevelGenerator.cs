using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] elevatorRoomPrefabs;
    [SerializeField] GameObject[] roomPrefabs;

	public static LevelGenerator instance;

    List<Enemy> postPowerEnemiesToActive;

	private void Awake()
	{
        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of LevelGenerator already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;

        postPowerEnemiesToActive = null;
    }

	// returns the root transform of the spawned level (so you can destroy it later)
	public Transform SpawnLevel(LevelSettings levelSettings)
	{
        Transform levelRoot = new GameObject().transform;
        levelRoot.position = Vector3.up * 20f;

        // get the random level settings
        int branchCount = Random.Range(levelSettings.minBranchCount, levelSettings.maxBranchCount + 1);
        int powerDistance = Random.Range(levelSettings.minPowerDistance, levelSettings.maxPowerDistance + 1);
        int[] branchDistances = new int[branchCount];
        for (int i = 0; i < branchDistances.Length; ++i)
        {
            branchDistances[i] = Random.Range(levelSettings.minBranchDistance, levelSettings.maxBranchDistance + 1);
        }
        print($"spawning a level with a branchCount of {branchCount}");

        // instantiate the elevator room
        int elevatorRoomToSpawnIndex = Random.Range(0, elevatorRoomPrefabs.Length);
        GameObject elevatorRoomInstance = Instantiate(elevatorRoomPrefabs[elevatorRoomToSpawnIndex], levelRoot);
        elevatorRoomInstance.transform.localPosition = Vector3.zero;
        Room elevatorRoom = elevatorRoomInstance.GetComponent<Room>();

        // instantiate random rooms (according to branch length, connecting doors as you go)
        Door previousDoor = null;
        for (int branchIndex = 0; branchIndex < branchCount; ++branchIndex)
        {
            int currentBranchLength = branchDistances[branchIndex];
            
            for (int roomIndex = 0; roomIndex < currentBranchLength; ++roomIndex) 
            {
                if (roomIndex == 0)
                {
                    // first room of a new branch, so get a random disconnected door in the level to connect it to
                    previousDoor = GetRandomDisconnectedDoorInLevel(levelRoot);
                }

                // instantiate a room
                int roomToSpawnIndex = Random.Range(0, roomPrefabs.Length);
                GameObject roomInstance = Instantiate(roomPrefabs[roomToSpawnIndex], levelRoot);

                // offset the room so it doesn't overlap any other rooms (including the elevator room)
                roomInstance.transform.localPosition = new Vector3(roomIndex * 50f, (branchIndex+1) * 50f, 0);

                // pick one of its doors to connect to the previous door
                Room room = roomInstance.GetComponent<Room>();
                if (!room) { Debug.LogError("you probably forgot to give one of the room prefabs a Room component! NullReferenceException incoming!"); }
                Door roomEntrance = room.GetRandomDisconnectedDoor();
                roomEntrance.connectedTo = previousDoor;
                previousDoor.connectedTo = roomEntrance;

                // pick another one of its doors to act as the connecting door for the next room
                previousDoor = room.GetRandomDisconnectedDoor();
            }

            // branch complete?
        }

        // destroy unused doors
        DestroyAllDisconnectedDoorsInLevel(levelRoot);

        // cull all but 1 power switch
        List<PowerSwitchStuff> pssList = new List<PowerSwitchStuff>(levelRoot.GetComponentsInChildren<PowerSwitchStuff>());
        if (pssList.Count == 0) Debug.LogError("no power switches found!");
        int saveThisSwitch = Random.Range(0, pssList.Count);
        for (int i = 0; i < pssList.Count; ++i)
        {
            if (i != saveThisSwitch) Destroy(pssList[i].gameObject);
        }

        // dupe all enemies, store in two lists (pre and post power)

        // randomly cull both lists and stop according to power budget

        // deactivate all of the post-power enemies (to be reactivated by the power switch)

        return levelRoot;
	}

    /*void SpawnPrePowerEnemies(LevelSettings levelSettings, Transform levelRoot)
    {
        SpawnEnemiesAccordingToBudget(levelSettings.prePowerDangerBudget, levelSettings.enemyPrefabs, levelRoot);
    }

    public void SpawnPostPowerEnemies(LevelSettings levelSettings, Transform levelRoot)
    {
        SpawnEnemiesAccordingToBudget(levelSettings.postPowerDangerBudget, levelSettings.enemyPrefabs, levelRoot);
    }

    void SpawnEnemiesAccordingToBudget(float dangerBudget, GameObject[] enemyPrefabs, Transform levelRoot)
    {
        // TODO: actually spawn the enemies
    }*/



    List<Room> GetRoomsOfLevel(Transform levelRoot) // this should even get the elevator room
    {
        List<Room> allRooms = new List<Room>(levelRoot.gameObject.GetComponentsInChildren<Room>());

        if (allRooms.Count == 0)
        {
            Debug.LogError("there shouldn't be 0 rooms when calling this method!");
        }

        return allRooms;
    }

    List<Door> GetAllDisconnectedDoorsInLevel(Transform levelRoot)
    {
        List<Room> allRooms = GetRoomsOfLevel(levelRoot);

        List<Door> allDisconnectedDoors = new List<Door>();
        foreach (Room r in allRooms)
        {
            allDisconnectedDoors.AddRange(r.GetAllDisconnectedDoors());
        }

        if (allDisconnectedDoors.Count == 0)
        {
            Debug.LogError("there shouldn't be 0 disconnected doors when calling this method!");
        }

        return allDisconnectedDoors;
    }

    void DestroyAllDisconnectedDoorsInLevel(Transform levelRoot)
    {
        /*List<Door> allDisconnectedDoors = GetAllDisconnectedDoorsInLevel(levelRoot);

        foreach (Door d in allDisconnectedDoors)
        {
            Destroy(d.gameObject);
        }*/

        // the above doesn't allow the rooms to maintain their doors lists properly

        List<Room> allRooms = GetRoomsOfLevel(levelRoot);

        foreach (Room r in allRooms)
        {
            r.DestroyAllDisconnectedDoors();
        }
    }

    Door GetRandomDisconnectedDoorInLevel(Transform levelRoot)
    {
        List<Door> allDisconnectedDoors = GetAllDisconnectedDoorsInLevel(levelRoot);

        if (allDisconnectedDoors.Count == 0)
        {
            Debug.LogError("Can't GetRandomDisconnectedDoorInLevel because there are no disconnected doors in the level! This shouldn't happen!");
            return null;
        }

        return allDisconnectedDoors[Random.Range(0, allDisconnectedDoors.Count)];
    }
}

