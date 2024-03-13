using UnityEngine;

[RequireComponent(typeof(Animation))]
public class LoopAnimation : MonoBehaviour
{
    Animation anim;

    public bool looping = true;
    public bool restartOnEnable = true;

    void Awake()
    {
        anim = GetComponent<Animation>();
    }

	private void OnEnable()
	{
        if (restartOnEnable)
        {
            anim.Stop();
            anim.Play();
        }
	}

	private void OnDisable()
	{
        if (restartOnEnable)
        {
            anim.Stop();
        }
    }

	void Update()
    {
        if (looping && !anim.isPlaying)
        {
            anim.Play();
        }
    }
}
