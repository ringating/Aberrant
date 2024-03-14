using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDied : MonoBehaviour
{
    public static YouDied instance;

    public GameObject[] problemScripts;

    public CanvasGroup cg;

    float timer;

    bool dead { get; set; }

	private void OnEnable()
	{
        HPManager.OnDeath += PlayDeathScreenAndFreezeGame;
	}

	private void OnDisable()
	{
        HPManager.OnDeath -= PlayDeathScreenAndFreezeGame;
    }

	private void Awake()
    {
        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of YouDied already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;

        // other
        timer = 0;
    }

    void Update()
    {
        if (dead) 
        {
            timer += Time.deltaTime;

            cg.alpha = timer;

            if (timer >= 4)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void PlayDeathScreenAndFreezeGame() // not accurate anymore but w/e
    {
        dead = true;
    }
}
