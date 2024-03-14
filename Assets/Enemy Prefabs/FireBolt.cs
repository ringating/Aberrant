using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt : MonoBehaviour
{
    public Transform aimer;
    public GameObject boltPrefab;
    public Enemy myEnemy;

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Fire();
        }
    }*/

    public void Fire()
    {
        GameObject g = Instantiate(boltPrefab, aimer.transform.position, aimer.transform.rotation);

        BoltMovement b = g.GetComponent<BoltMovement>();
        b.myEnemy = myEnemy;

        g.transform.parent = transform.parent; // make it a child of my parent (the current room, probably) so the bolt will be destroyed when the level is destroyed
    }
}
