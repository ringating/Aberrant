using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraNumberText : MonoBehaviour
{
	public static CameraNumberText instance;

	public string text { get { return tmp.text; } set { tmp.text = value; } }

	TextMeshProUGUI tmp;

	private void Awake()
	{
		// singleton stuff
		if (instance)
		{
			Debug.LogError("An instance of CameraNumberText already exists! Destroying this instance!");
			Destroy(this);
			return;
		}
		instance = this;

		// set values
		tmp = GetComponent<TextMeshProUGUI>();
	}

	public void SetNumber(int number)
	{
		text = "CAM " + TimeWriter.TwoDigits(number);
	}

	public void SetUnknown() 
	{
		text = "CAM ??";
	}
}

