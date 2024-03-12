using UnityEngine;

// this component has to be on the root of the door prefab, or else it won't destroy itself properly
public class Door : MonoBehaviour
{
    [HideInInspector] public Door connectedTo; // possibly assigned during level generation (if not, this door's gameobject will get destroyed by the level generator at some point)
    [HideInInspector] public Room myRoom; // assigned during Room's Awake

    public InteractableTrigger myTrigger;

    public bool connected { get { return connectedTo; } }

	private void OnEnable()
	{
        myTrigger.OnInteract += UseDoor;
	}

	private void OnDisable()
	{
        myTrigger.OnInteract -= UseDoor;
    }

	public void DestroyIfNotConnected()
    {
        if (!connected)
        {
            Destroy(gameObject);
        }
    }

    public void UseDoor()
    {
        if (!connected)
        {
            Debug.LogError("attempted to use a door that isn't connected to another door!");
            return;
        }
        
        GameplayManager.instance.UseDoor(this);
    }
}
