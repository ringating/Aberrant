using UnityEngine;
using System.Threading;
using System;
using System.Diagnostics;

// used this as a starting point: https://stackoverflow.com/questions/54148708/programming-an-fps-limiter-for-unity
public class FrameLimiter : MonoBehaviour
{
    private static FrameLimiter instance;
    public FrameLimiter Instance { get { return instance; } }

    public double FPSLimit = 300.0;

    Stopwatch timer;
    long time = 0;

    void Awake()
    {
        if (instance)
        {
            UnityEngine.Debug.LogWarning("older FrameLimiter instance found! oof!");
            Destroy(this);
            return;
        }
        else 
        {
            instance = this;
        }

        timer = new Stopwatch();
        timer.Start();
    }

    void OnDestroy()
    {
        instance = null;
    }

    void Update()
    {
        if (FPSLimit == 0.0) return;

        time += SecondsToStopwatchTicks(1.0 / FPSLimit); // update the time that SpinWait will try to wait until

        if (timer.ElapsedTicks >= time)
        {
            // framerate is naturally lower than the fps limit
            time = timer.ElapsedTicks;
            return;
        }
        else 
        {
            SpinWait.SpinUntil(() => { return timer.ElapsedTicks >= time; });
        }
    }

    static long SecondsToStopwatchTicks(double seconds)
	{
        return Convert.ToInt64(seconds * Stopwatch.Frequency);
	}
}