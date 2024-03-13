using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimEvents : MonoBehaviour
{
    public InteractableTrigger triggerText;

    public void MakeElevatorExitable()
    {
        triggerText.interactText = "press E to exit elevator";

        Collider trigger = triggerText.GetComponent<Collider>();
        trigger.enabled = false;
        trigger.enabled = true;
    }

    public void SetInitialElevatorText()
    {
        triggerText.interactText = "(waiting for elevator to stop)";
    }
}
