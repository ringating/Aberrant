using UnityEngine;

public class ClearITTACSAfterItFires : MonoBehaviour
{
    public InteractTriggerTeleportAndCamSwitch ittacs;

	private void OnEnable()
	{
		ittacs.OnTeleported += Clear;
	}

	private void OnDisable()
	{
		ittacs.OnTeleported -= Clear;
	}

	void Clear()
	{
		ittacs.nextCamera = null;
		ittacs.whereToTeleportPlayer = null;
		ittacs.door = null;
	}
}
