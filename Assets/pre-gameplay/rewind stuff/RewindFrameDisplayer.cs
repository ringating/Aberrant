using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RewindFrameDisplayer : MonoBehaviour
{
	[Header("animate-able properties")]
    public bool displaying;
    [Range(0f, 1f)] public float rewindProgress;

	[Header("other")]
    public PostProcessVolume volume;
	public MeshRenderer rewindQuadMR;

	Grain grain;

	private void Start()
	{
		volume.profile.TryGetSettings(out grain);
		rewindQuadMR.material = Instantiate(rewindQuadMR.material); // so we don't overwrite properties of the actual asset file
	}

	private void Update()
	{
		if (displaying)
		{
			RewindBuffer.instance.recording = false;

			grain.active = true;
			rewindQuadMR.enabled = true;

			rewindQuadMR.material.mainTexture = GetRenderTextureForCurrentRewindProgress();
		}
		else 
		{
			RewindBuffer.instance.recording = true;

			grain.active = false;
			rewindQuadMR.enabled = false;

			rewindProgress = 0;
		}
	}

	RenderTexture GetRenderTextureForCurrentRewindProgress()
	{
		int frameIndex = Mathf.RoundToInt((1f - rewindProgress) * (RewindBuffer.instance.replayBuffer.Count - 1));
		return RewindBuffer.instance.replayBuffer[frameIndex];
	}

	public void ClearRewindBuffer()
	{
		RewindBuffer.instance.replayBuffer.Clear();
	}
}
