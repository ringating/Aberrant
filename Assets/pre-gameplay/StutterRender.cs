using UnityEngine;

public class StutterRender : MonoBehaviour
{
    public static StutterRender instance;

    [SerializeField] int renderEvery = 1;
    [SerializeField] int minInterval = 30;
    [SerializeField] int maxInterval = 60;
    [SerializeField] int minStutter = 1;
    [SerializeField] int maxStutter = 3;

    Camera cam;

    int countSinceLastStutter;
    int interval;
    int stutter;

    // other scripts can set this to freeze rendering for a certain number of real frames (useful for creating "freeze-frames" for "Impact")
    public int extraStutter { get; set; }

	private void Awake()
	{
        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of StutterRender already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;
	}

	void Start()
    {
        cam = GetComponent<Camera>();
        cam.enabled = false;

        BeginCycle();
    }

    void Update()
    {
        if (extraStutter <= 0)
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
        else
        {
            extraStutter--;
        }
    }

    void BeginCycle()
    {
        countSinceLastStutter = 0;
        interval = Random.Range(minInterval, maxInterval + 1);
        stutter = Random.Range(minStutter, maxStutter + 1);
    }
}
