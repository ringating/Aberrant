using UnityEngine;

public class StutterRender : MonoBehaviour
{
    [SerializeField] int renderEvery = 1;
    [SerializeField] int minInterval = 30;
    [SerializeField] int maxInterval = 60;
    [SerializeField] int minStutter = 1;
    [SerializeField] int maxStutter = 3;

    Camera cam;

    int countSinceLastStutter;
    int interval;
    int stutter;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.enabled = false;

        BeginCycle();
    }

    void Update()
    {
        if (countSinceLastStutter > interval + stutter)
        {
            BeginCycle();
        }

        if (countSinceLastStutter <= interval)
        {
            if (countSinceLastStutter % renderEvery == 0)
            {
                cam.Render();
            }
        }
        else // stuttering
        {
            // uh
        }

        countSinceLastStutter++;
    }

    void BeginCycle()
    {
        countSinceLastStutter = 0;
        interval = Random.Range(minInterval, maxInterval + 1);
        stutter = Random.Range(minStutter, maxStutter + 1);
    }
}
