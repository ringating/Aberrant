using UnityEngine;

public class Interact : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (InteractableTrigger.active)
            {
                InteractableTrigger.active.Interact();
                InteractableTrigger.active = null;
            }
        }
    }
}
