using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CoinRegister : MonoBehaviour
{
	private bool isPlayerReady = false;

	public bool IsPlayerDepositing()
	{
		return isPlayerReady;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerReady = true;
		Debug.Log("Here I ammmmmm!");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerReady = false;
		Debug.Log("Aiight Ima head out");
		}
	}
	private void Update()
	{
		if (isPlayerReady && Input.GetKeyDown(KeyCode.E)) 
		{
			PlayerScript player = FindFirstObjectByType<PlayerScript>();
			if (player != null)
			{
				player.DepositCoins(this);
			}
		}
	}
}