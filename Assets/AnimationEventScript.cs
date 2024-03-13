using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public void ReturnPlayerToIdleState()
    {
        Player.instance.ReturnPlayerToIdle();
    }

    public void SetPlayerFacingDirectionToInput()
    {
        Player.instance.SetFacingDirectionToInput();
    }

    public void SetPlayerPunchSpeed(float punchSpeed)
    {
        Player.instance.punchSpeed = punchSpeed;
    }

    public void EnablePlayerHitbox()
    {
        Player.instance.hitbox.SetActive(true);
    }
}
