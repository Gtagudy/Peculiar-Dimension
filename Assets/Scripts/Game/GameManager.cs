using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int netCoins = 0;
    public int greedMark = 50;

    [SerializeField] GameObject winnerWinner; 
    [SerializeField] GameObject ripBozo;



	private void Update()
	{
		if(netCoins >= greedMark)
        {
            WinGame();
        }
	}
	private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	private void Start()
	{
        winnerWinner.SetActive(false);
        ripBozo.SetActive(false);
	}
	public void AddCoins(int Coin)
    {
        netCoins += Coin;
		CanWeLeave();
    }

    private void CanWeLeave()
    {
        if (netCoins >= 50)
        {
			WinGame();
        }
	}

	private void WinGame()
	{
        winnerWinner?.SetActive(true);
        Time.timeScale = 0;
	}
	public void PlayerLoses()
	{
		ripBozo.SetActive(true);
		Time.timeScale = 0; 
	}

	public void RestartGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
