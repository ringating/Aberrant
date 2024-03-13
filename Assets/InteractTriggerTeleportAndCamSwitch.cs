using UnityEngine;

public class InteractTriggerTeleportAndCamSwitch : MonoBehaviour
{
    public InteractableTrigger it;
	[Header("use either these")]
	public Camera nextCamera;
	public Transform whereToTeleportPlayer;
	[Header("or this")]
	public Door door;

	public delegate void TeleportedAction();
	public event TeleportedAction OnTeleported;

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
		if (door)
		{
			CRTCameraSwitcher.instance.SwitchTo(door.myRoom.myCamera, door.playerTeleportPoint);
			OnTeleported?.Invoke();
		}
		else if (nextCamera && whereToTeleportPlayer)
		{
			CRTCameraSwitcher.instance.SwitchTo(nextCamera, whereToTeleportPlayer);
			OnTeleported?.Invoke();
		}
		else 
		{
			Debug.LogWarning("can't call CRTCameraSwitcher.SwitchTo without either a door or nextCamera + whereToTeleportPlayer");
		}
		
	}
}
