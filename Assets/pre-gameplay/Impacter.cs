using UnityEngine;

public class Impacter : MonoBehaviour
{
    [SerializeField] int hitStopFrames;
    [Header("animation stuff")]
    [SerializeField] Animation mainCamAnimation;
    [SerializeField] string impactRightClipName;
    [SerializeField] string impactLeftClipName;

    [Header("light physics stuff")]
    [SerializeField] Rigidbody lightRB;
    [SerializeField] float lightImpulse;

    public static Impacter instance;

    public enum ImpactType 
    {
        right,
        left
    }

    public void Impact(ImpactType impactType) 
    {
        StutterRender.instance.extraStutter = hitStopFrames;
        //FreezeFrames.instance.Freeze(hitStopFrames);
        
        if (mainCamAnimation.isPlaying) 
        {
            mainCamAnimation.Stop();
        }

        switch (impactType) 
        {
            case ImpactType.left:
                mainCamAnimation.Play(impactLeftClipName);
                lightRB.AddForce(Vector3.right * lightImpulse, ForceMode.Impulse);
                break;

            case ImpactType.right:
                mainCamAnimation.Play(impactRightClipName);
                lightRB.AddForce(Vector3.left * lightImpulse, ForceMode.Impulse);
                break;
        }
    }

    public void RandomImpact()
    {
        Impact(Random.value < 0.5f ? ImpactType.left : ImpactType.right);
    }

	private void Awake()
	{
        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of Impacter already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;
    }
}
