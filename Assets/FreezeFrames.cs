using UnityEngine;

public class FreezeFrames : MonoBehaviour
{
	public static FreezeFrames instance;
    int frozenFramesRemaining;

	private void Awake()
	{
		// singleton stuff
		if (instance)
		{
			Debug.LogError("An instance of FreezeFrames already exists! Destroying this instance!");
			Destroy(this);
			return;
		}
		instance = this;
	}

	private void Start()
	{
		frozenFramesRemaining = 0;
	}

	private void Update()
	{
		if (frozenFramesRemaining > 0)
		{
			frozenFramesRemaining--;
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void Freeze(int frameCount)
	{
		frozenFramesRemaining = frameCount;
	}
}
