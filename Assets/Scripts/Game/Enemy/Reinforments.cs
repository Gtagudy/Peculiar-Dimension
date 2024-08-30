using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinforcements : MonoBehaviour
{
	[SerializeField] public GameObject enemyType;
	[SerializeField] public int enemyCount = 5;
	[SerializeField] public float spawnDelay = 2f;

	[SerializeField] public int enemyLeft = 0;
	[SerializeField] private bool currentReinforcement = false;

	[SerializeField] float spawnRadius = 20f; 
	private void Start()
	{
		StartWave();
	}
	public void StartWave()
	{
		currentReinforcement = true;
		enemyLeft = enemyCount;
		for (int i = 0; i < enemyCount; i++)
		{
			SpawnEnemy();
		}
	}
	public void SpawnEnemy()
	{
		Instantiate(enemyType, GetRandomSpawnPosition(), Quaternion.identity);
	}
	public void EnemyDefeated()
	{
		enemyLeft--;
		if (enemyLeft <= 0)
		{
			StartWave();
		}
	}
	private Vector3 GetRandomSpawnPosition()
	{
		Vector3 randomPosition = new Vector3(
			Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius));
		return randomPosition;
	}
}
