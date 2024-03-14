
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    [Tooltip("in degrees")]
    public float maxTurnRate;
    
    void Update()
    {
        Vector3 toPlayer = (Player.instance.transform.position + (Vector3.up * 0.4f)) - transform.position;

        Vector3 newForward = Vector3.RotateTowards(transform.forward, toPlayer, Time.deltaTime * maxTurnRate * Mathf.Deg2Rad, 0);

        transform.rotation = Quaternion.LookRotation(newForward);
    }
}
