using UnityEngine;

public class EnableAndDisableAnotherGameObject : MonoBehaviour
{
    public GameObject toEnableAndDisable;
	public bool enable = true;
	public bool disable = true;

	private void OnEnable()
	{
		if (enable)
		{
			toEnableAndDisable.SetActive(true);
		}
	}

	private void OnDisable()
	{
		if (disable)
		{
			toEnableAndDisable.SetActive(false);
		}
	}
}
