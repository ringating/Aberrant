using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public static HPManager instance;

    public int maxHP = 8;
    public int hp { get; set; }
    public TextMeshProUGUI hpBar;

    public delegate void DeathAction();
    public static event DeathAction OnDeath;

    public delegate void HitAction();
    public static event HitAction OnHit;

    private void Awake()
    {
        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of HPManager already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;

        // set initial values
        hp = maxHP;
        alreadyDied = false;
        UpdateHPBar();
    }

    public void HitAndScreenShake(int damage)
    {
        hp -= damage;
        Impacter.instance.RandomImpact(0.2f, false);
        UpdateHPBar();
        OnHit?.Invoke();
        CheckIfDead();
    }

    bool alreadyDied;
    void CheckIfDead()
    {
        if (hp <= 0 && !alreadyDied)
        {
            OnDeath?.Invoke();
            alreadyDied = true;
        }
    }

    void UpdateHPBar()
    {
        string hpString = "";

        for (int i = 0; i < hp; ++i) 
        {
            hpString += "/";
        }

        hpBar.text = hpString;
    }
}
