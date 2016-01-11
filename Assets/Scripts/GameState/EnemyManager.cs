using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PoolBuilder))]
public class EnemyManager : MonoBehaviour {

	public delegate void EnemyManagerEvent();
	public event EnemyManagerEvent OnAllWaveEnemiesSpawned;

	public GameController gameController;
	public GameObject[] enemyPrefabs;
	public float baseWaveDuration = 20f;
	public float baseSpawnInterval = 2f;
	public float WaveDuration { get { return baseWaveDuration; } }
	public float SpawnInterval
	{
		get { return baseSpawnInterval * Mathf.Pow(Mathf.Sqrt(0.5f), gameController.CurrentWave); } 
	}

	private PoolBuilder poolBuilder;
	private List<GameObject[]> enemyPools;
	private float waveStartTime;


	void Awake()
	{
		poolBuilder = GetComponent<PoolBuilder>();
	}


	void OnEnable()
	{
		gameController.OnSpawningStart += SpawnEnemies;
	}


	void OnDisable()
	{
		gameController.OnSpawningStart -= SpawnEnemies;
	}


	void Start()
	{
		SeedEnemies();
	}


	void SeedEnemies()
	{
		enemyPools = poolBuilder.BuildPools(enemyPrefabs);
	}


	void SpawnEnemies()
	{
		StartCoroutine(CoSpawnEnemies());
	}


	IEnumerator CoSpawnEnemies()
	{
		waveStartTime = Time.fixedTime;


		while (Time.fixedTime - waveStartTime <= WaveDuration)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(SpawnInterval);
		}


		if (OnAllWaveEnemiesSpawned != null)
			OnAllWaveEnemiesSpawned();
	}


	void SpawnEnemy()
	{
		GameObject enemy = poolBuilder.GetNextInstanceFromPool(RandomEnemyIndex());
		enemy.SetActive(true);
	}


	void DespawnEnemies()
	{
		foreach(GameObject[] pool in enemyPools)
		{
			foreach(GameObject enemy in pool)
			{
				enemy.SetActive(false);
			}
		}
	}


	int RandomEnemyIndex()
	{
		int index = 0;
		float randomNumber = Random.Range(0, 100f);

		// REFACTOR: Find a more elegant way to deal with different probabilities for enemies.
		if (randomNumber > 95)
			index = 2;
		else if (randomNumber > 75)
			index = 1;

		return index;
	}
}
