using UnityEngine;

public class InteractTriggerTeleportAndCamSwitch : MonoBehaviour
{
    public InteractableTrigger it;
	public Camera nextCamera;
	public Transform whereToTeleportPlayer;

	private void OnEnable()
	{
		it.OnInteract += DoTransition;
	}

	private void OnDisable()
	{
		it.OnInteract -= DoTransition;
	}

	void DoTransition()
	{
		CRTCameraSwitcher.instance.SwitchTo(nextCamera, whereToTeleportPlayer);
	}
}
