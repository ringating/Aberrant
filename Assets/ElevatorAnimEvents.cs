using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimEvents : MonoBehaviour
{
    public InteractableTrigger triggerText;
    public InteractTriggerTeleportAndCamSwitch ittacs;

    public void MakeElevatorExitable()
    {
        triggerText.interactText = "press E to exit elevator";

        Collider trigger = triggerText.GetComponent<Collider>();
        trigger.enabled = false;
        SetSwitcherData();
        trigger.enabled = true;
    }

    public void SetInitialElevatorText()
    {
        triggerText.interactText = "(waiting for elevator to stop)";
    }

    public void DestroyOldLevelIfItExists() 
    {
        if (GameplayManager.instance.currentLevelRoot)
        {
            Destroy(GameplayManager.instance.currentLevelRoot.gameObject);
        }
    }

    public void GenerateLevel()
    {
        lastLevelGenerationWasSuccessful = GameplayManager.instance.GenerateNextFloor();
    }

    bool lastLevelGenerationWasSuccessful;
    private void SetSwitcherData()
    {
        if (lastLevelGenerationWasSuccessful)
        {
            ElevatorRoom er = GameplayManager.instance.currentLevelRoot.GetComponentInChildren<ElevatorRoom>();

            ittacs.nextCamera = er.cam;
            ittacs.whereToTeleportPlayer = er.playerInitialTeleportPoint;
        }
        else 
        {
            // beat the game???
            // TODO
        }
    }
}
