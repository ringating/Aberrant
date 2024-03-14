using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
	public static MoneyManager instance;

	public TextMeshProUGUI moneyText;

	public int money { get; set; }

	private void Awake()
	{
		// singleton stuff
		if (instance)
		{
			Debug.LogError("An instance of MoneyManager already exists! Destroying this instance!");
			Destroy(this);
			return;
		}
		instance = this;
	}

	public void AddMoney(int money)
	{
		this.money += money;

		moneyText.text = this.money.ToString();
	}
}
