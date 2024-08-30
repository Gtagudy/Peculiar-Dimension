using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public int coinValue = 1;
	[SerializeField] PlayerScript player;

	private void OnTriggerEnter(Collider other)
	{
		player = other.GetComponent<PlayerScript>();

		if (other.CompareTag("Player"))
		{
				player.CoinUp(coinValue);
				Destroy(gameObject);
		}
	}
}
