using UnityEngine;

public class CRTCamera : MonoBehaviour
{
    public Camera myCamera { get; private set; }

	public int myCount;

	private static int count;

	private void Awake()
	{
		myCamera = GetComponent<Camera>();
		myCamera.enabled = false;

		// i don't want to have to remember to set this manually every time i make a new room prefab, so im just doing it here
		myCamera.nearClipPlane = 0.09f;

		myCount = ++count;
	}
}
