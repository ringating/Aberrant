using System.Collections.Generic;
using UnityEngine;

public class RewindBuffer : MonoBehaviour
{
    public static RewindBuffer instance;

    [SerializeField] Camera mainCam;
    [SerializeField] RenderTexture toDupe;
    public int frameCount = 120;
    [SerializeField] int captureEvery = 1;

    int framesSinceLastCapture;

    [HideInInspector] public List<RenderTexture> replayBuffer;

    public bool recording = true;

	private void Awake()
	{
        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of RewindBuffer already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;
    }

	void Start()
    {
        framesSinceLastCapture = 0;

        replayBuffer = new List<RenderTexture>();
    }

    void Update()
    {
        if (recording) 
        {
            if (framesSinceLastCapture >= captureEvery)
            {
                framesSinceLastCapture = 0;
                AddOrOverwrite();
            }

            framesSinceLastCapture++;
        }
    }

    void AddOrOverwrite()
    {
        RenderTexture toRenderTo;

        if (replayBuffer.Count < frameCount)
        {
            // add a new element
            toRenderTo = Instantiate(toDupe);
            replayBuffer.Add(toRenderTo);
        }
        else
        {
            // remove the oldest element from the list and re-add it as the newest element of the list
            RenderTexture oldest = replayBuffer[0];
            replayBuffer.RemoveAt(0);
            replayBuffer.Add(oldest);
            toRenderTo = oldest;
        }

        RenderToTexture(toRenderTo);
    }

    void RenderToTexture(RenderTexture rt)
    {
        mainCam.targetTexture = rt;
        mainCam.Render();
        mainCam.targetTexture = null;
    }
}
