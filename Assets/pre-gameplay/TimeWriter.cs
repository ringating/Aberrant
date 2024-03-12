using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeWriter : MonoBehaviour
{
    public float time;

	[SerializeField] TextMeshProUGUI timeText;

	private void Update()
	{
		int seconds = Mathf.FloorToInt(time % 60);
		int minutes = Mathf.FloorToInt((time / 60) % 60);
		int hours = Mathf.FloorToInt(time / 3600);

		timeText.text = $"{TwoDigits(hours)}:{TwoDigits(minutes)}:{TwoDigits(seconds)}";
	}

	public static string TwoDigits(int number)
	{
		if (number < 10)
		{
			return "0" + number.ToString();
		}

		return number.ToString();
	}
}
