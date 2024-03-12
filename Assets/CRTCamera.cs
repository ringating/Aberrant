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

		myCount = ++count;
	}
}
