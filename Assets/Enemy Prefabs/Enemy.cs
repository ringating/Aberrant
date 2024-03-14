using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 8;
    public int damage = 2;
    public float dangerBudgetCost = 1;
    public int bounty = 1;

    public AudioClipWithVolume soundWhenHit;
    public AudioClipWithVolume soundWhenDie;

    public int hp { get; private set; }

    public delegate void DeathAction();
    public event DeathAction OnDeath;

    public delegate void HitAction();
    public event HitAction OnHit;

	private void Awake()
	{
        hp = maxHP;
	}

	public void HitAndShakeScreenAndFreeze(int damage)
    {
        hp -= damage;

        print($"my hp is now {hp}");

        OnHit?.Invoke();
        Impacter.instance.RandomImpact(0.1f, false);
        bool dead = CheckIfDead();

        if (dead)
        {
            Audio2DSingleton.instance.audioSource.PlayOneShot(soundWhenDie.audioClip, soundWhenDie.volume);
        }
        else 
        {
            Audio2DSingleton.instance.audioSource.PlayOneShot(soundWhenHit.audioClip, soundWhenHit.volume);
        }
    }

    bool CheckIfDead()
    {
        if (hp <= 0)
        {
            OnDeath?.Invoke();
            MoneyManager.instance.AddMoney(bounty);
            Destroy(gameObject); // this is fine for now (and means i dont have to worry about this executing several times, probably)
            return true;
        }

        return false;
    }
}
