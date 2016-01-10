using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PoolBuilder))]
public class EnemyManager : MonoBehaviour {

	public delegate void EnemyManagerEvent();
	public event EnemyManagerEvent OnAllWaveEnemiesSpawned;

	public GameController gameController;
	public GameObject[] enemyPrefabs;

	private List<GameObject[]> prefabPools;


	void OnEnable()
	{
		gameController.OnWaveStart += SpawnEnemies;
	}


	void Start()
	{
		SeedEnemies();
	}


	void SeedEnemies()
	{
		prefabPools = GetComponent<PoolBuilder>().BuildPools(enemyPrefabs);
	}


	void SpawnEnemies()
	{
		// TODO
	}
}
