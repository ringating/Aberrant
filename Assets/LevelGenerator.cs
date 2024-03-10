using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] roomPrefabs;

	public static LevelGenerator instance;

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
    }

	// returns the root transform of the spawned level (so you can destroy it later)
	public Transform SpawnLevel(LevelSettings levelSettings)
	{
        Transform levelRoot = new GameObject().transform;
        // TODO: actually generate the level

        SpawnPrePowerEnemies(levelSettings, levelRoot);

        return levelRoot;
	}

    void SpawnPrePowerEnemies(LevelSettings levelSettings, Transform levelRoot)
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
    }
}

