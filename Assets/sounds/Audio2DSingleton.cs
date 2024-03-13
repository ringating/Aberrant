using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio2DSingleton : MonoBehaviour
{
    public static Audio2DSingleton instance;

    [HideInInspector] public AudioSource audioSource;

	private void Awake()
	{
        // get component
        audioSource = GetComponent<AudioSource>();

        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of Audio2DSingleton already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;
    }
}
