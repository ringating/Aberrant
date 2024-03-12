using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class RandomTextureSwitcher : MonoBehaviour
{
	public float textureDuration = 0.125f;
	public Texture2D[] textures;

	MeshRenderer mr;
	float timer;
	int prevIndex;

	private void Start()
	{
		mr = GetComponent<MeshRenderer>();
		mr.material = Instantiate(mr.material);

		// set first texture and prevIndex
		prevIndex = Random.Range(0, textures.Length);
		mr.material.mainTexture = textures[prevIndex];
	}

	private void Update()
	{
		if (timer > textureDuration)
		{
			if (textures.Length < 2)
			{
				Debug.LogError("RandomTextureSwitcher needs at least 2 textures!");
				return;
			}

			mr.material.mainTexture = GetNextTexture();
			timer = 0;
		}

		timer += Time.deltaTime;
	}

	public Texture2D GetNextTexture()
	{
		int index = Random.Range(0, textures.Length - 1); // one less than usual, since im excluding the previous index

		if (index >= prevIndex)
		{
			index++;
		}

		prevIndex = index;

		return textures[index];
	}
}
