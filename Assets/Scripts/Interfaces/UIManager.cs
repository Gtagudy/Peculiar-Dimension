using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] public TextMeshProUGUI coinCountText;
	[SerializeField] public TextMeshProUGUI depositRequirementText;
	[SerializeField] public TextMeshProUGUI depositedCoinsText;
	[SerializeField] public TextMeshProUGUI enemiesLeftText;

	[SerializeField] private PlayerScript player;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private Reinforcements reinforcements;


	private void Start()
	{
		//player = GetComponent<PlayerScript>();
		//gameManager = GetComponent<GameManager>();
		//reinforcements = GetComponent<Reinforcements>();

		UpdateUI();
	}

	private void Update()
	{
		UpdateUI();
	}

	public void UpdateUI()
	{
		coinCountText.text = "Coins: " + player.coins.ToString() + " Out of " + player.pocketSize.ToString();
		depositRequirementText.text = "Deposit Goal: " + gameManager.greedMark.ToString();
		depositedCoinsText.text = "Deposited Coins: " + gameManager.netCoins.ToString();
		enemiesLeftText.text = "Enemies Left: " + reinforcements.enemyLeft.ToString();
	}
}
