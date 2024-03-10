using UnityEngine;


// this component has to be on the root of the door prefab, or else it won't destroy itself properly
public class Door : MonoBehaviour
{
    public Door connectedTo;

    public Room myRoom;

    public bool connected { get { return connectedTo; } }

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
        
        GameplayManager.instance.Door(this);
    }
}
