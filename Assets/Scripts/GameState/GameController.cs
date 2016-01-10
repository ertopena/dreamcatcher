using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public delegate void GameStateEvent();
	public event GameStateEvent OnWaveStart;
	public event GameStateEvent OnSpawningCompleted;
	public event GameStateEvent OnWaveCleared;
	public event GameStateEvent OnPlayerDeath;


	public GameState State { get; private set; }

	public HeartRateTracker heartRateTracker;
	public float childRadius = 0.5f;


	void Awake()
	{
		State = GameState.PrePlay;
	}


	void OnEnable()
	{
		heartRateTracker.OnLethalHeartRate += HandlePlayerDeath;
		EnemyCounter.OnEnemyCountUpdate += HandleEnemyCountChange;
	}


	void OnDisable()
	{
		heartRateTracker.OnLethalHeartRate -= HandlePlayerDeath;
		EnemyCounter.OnEnemyCountUpdate -= HandleEnemyCountChange;
	}


	void StartWave()
	{
		if (OnWaveStart != null)
			OnWaveStart();
	}


	void HandleSpawningCompleted()
	{
		// TODO
	}


	void HandleEnemyCountChange(int newCount)
	{
		if (newCount == 0)
			HandleWaveCleared();
	}


	void HandleWaveCleared()
	{

	}


	void HandlePlayerDeath(HeartRateTracker hrt, int finalHeartRate)
	{
		// TODO;
	}
}
