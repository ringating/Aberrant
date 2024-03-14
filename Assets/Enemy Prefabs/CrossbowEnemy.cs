using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowEnemy : Enemy
{
    public FireBolt boltShooter;
    public float minTimeBetweenShots = 2;
    public float maxTimeBetweenShots = 5;
    public float fireDelay = 0.3f;
    public AudioClipWithVolume armingSound;
    public AudioClipWithVolume firingSound;
    public GameObject green;
    public GameObject red;

    float shotTimer;
    float wait;

    bool armed;

	private void Start()
	{
        shotTimer = 0;
        armed = false;
	}

    int armCount;
    int shotCount;

	private void Update()
    {
        if (Vector3.Distance(Player.instance.transform.position, transform.position) < 30)
        {
            shotTimer += Time.deltaTime;
        }

        if (shotTimer > wait + fireDelay)
        {
            // shoot
            //print($"armed {++shotCount}");
            boltShooter.Fire();
            Audio2DSingleton.instance.audioSource.PlayOneShot(firingSound.audioClip, firingSound.volume);
            PrepForNextShot();
            armed = false;
        }
        else if (shotTimer > wait)
        {
            // armed

            if (!armed)
            {
                //print($"armed {++armCount}");
                armed = true;
                Arm();
            }
        }
        else
        {
            // waiting

        }
    }

    void PrepForNextShot()
    {
        green.SetActive(true);
        red.SetActive(false);

        shotTimer = 0;
        wait = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Arm(bool playSound = true) 
    {
        if (playSound) Audio2DSingleton.instance.audioSource.PlayOneShot(armingSound.audioClip, armingSound.volume);
        green.SetActive(false);
        red.SetActive(true);
    }
}



[System.Serializable]
public class AudioClipWithVolume 
{
    public AudioClip audioClip;
    public float volume;
}