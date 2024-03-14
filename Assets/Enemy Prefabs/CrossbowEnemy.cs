using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowEnemy : Enemy
{
    public FireBolt boltShooter;
    public float timeBetweenShots = 3;
    public float fireDelay = 0.3f;
    public AudioClipWithVolume armingSound;
    public AudioClipWithVolume firingSound;
}



[System.Serializable]
public class AudioClipWithVolume 
{
    public AudioClip audioClip;
    public float volume;
}