using UnityEngine;

public class Impacter : MonoBehaviour
{
    [SerializeField] int hitStopFrames;
    [Header("animation stuff")]
    [SerializeField] Animation mainCamAnimation;
    [SerializeField] string impactRightClipName;
    [SerializeField] string impactLeftClipName;
    [SerializeField] Animation screenShakeAnim;

    [Header("light physics stuff")]
    [SerializeField] Rigidbody lightRB;
    [SerializeField] float lightImpulse;

    public static Impacter instance;

    public enum ImpactType 
    {
        right,
        left
    }

    public void Impact(ImpactType impactType, float lightForceScalar = 1f, bool mainCamImpact = true) 
    {
        StutterRender.instance.extraStutter = hitStopFrames;
        //FreezeFrames.instance.Freeze(hitStopFrames);

        ScreenShakeOnly();

        if (mainCamImpact && mainCamAnimation.isPlaying) mainCamAnimation.Stop();

        switch (impactType) 
        {
            case ImpactType.left:
                if (mainCamImpact) mainCamAnimation.Play(impactLeftClipName);
                lightRB.AddForce(Vector3.right * lightImpulse * lightForceScalar, ForceMode.Impulse);
                break;

            case ImpactType.right:
                if (mainCamImpact) mainCamAnimation.Play(impactRightClipName);
                lightRB.AddForce(Vector3.left * lightImpulse * lightForceScalar, ForceMode.Impulse);
                break;
        }
    }

    public void RandomImpact(float lightForceScalar = 1f, bool mainCamImpact = true)
    {
        Impact(Random.value < 0.5f ? ImpactType.left : ImpactType.right, lightForceScalar, mainCamImpact);
    }

    private void ScreenShakeOnly()
    {
        if (screenShakeAnim.isPlaying) screenShakeAnim.Stop();
        screenShakeAnim.Play();
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
