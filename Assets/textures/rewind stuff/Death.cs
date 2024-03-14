using UnityEngine;

public class Death : MonoBehaviour
{
    public static Death instance;

    [SerializeField] Animation anim;
    [SerializeField] AudioSource audioSource2D;
    [SerializeField] AudioClip deathSound;

	private void Awake()
	{
        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of Death already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;
    }

    public static void Die()
    {
        instance.audioSource2D.PlayOneShot(instance.deathSound);

        instance.anim.Stop();
        instance.anim.Play();
    }

	private void Update()
	{
        // test key
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            Die();
        }
	}
}
