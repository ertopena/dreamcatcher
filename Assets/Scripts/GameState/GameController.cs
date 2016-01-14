using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public delegate void GameStateEvent();
	public event GameStateEvent OnGameStart;
	public event GameStateEvent OnTransitionStart;
	public event GameStateEvent OnSpawningStart;
	public event GameStateEvent OnSpawningCompleted;
	public event GameStateEvent OnWaveCleared;
	public event GameStateEvent OnPlayerDeath;


	public int CurrentWave { get; private set; }
	public bool IsSpawningCompleted { get; private set; }
	public HeartRateTracker heartRateTracker;
	public EnemyManager enemyManager;
	public float childRadius = 0.5f;
	public float transitionDuration = 2f;


	void OnEnable()
	{
		heartRateTracker.OnLethalHeartRate += HandlePlayerDeath;
		enemyManager.OnAllWaveEnemiesSpawned += HandleSpawningCompleted;
		EnemyCounter.OnEnemyCountUpdate += HandleEnemyCountChange;
	}


	void OnDisable()
	{
		heartRateTracker.OnLethalHeartRate -= HandlePlayerDeath;
		enemyManager.OnAllWaveEnemiesSpawned -= HandleSpawningCompleted;
		EnemyCounter.OnEnemyCountUpdate -= HandleEnemyCountChange;
	}


	void Start()
	{
		StartGame();
	}


	public void StartGame()
	{
		CurrentWave = 0;

		if (OnGameStart != null)
			OnGameStart();

		StartWave();
	}


	public void StartWave()
	{
		CurrentWave++;
		IsSpawningCompleted = false;

		if(this.gameObject.activeInHierarchy)
			StartCoroutine(CoTransitionToWave());
	}


	IEnumerator CoTransitionToWave()
	{
		if (OnTransitionStart != null)
			OnTransitionStart();


		// TODO: Flesh out with whatever happens during transition.
		yield return new WaitForSeconds(transitionDuration);


		if (OnSpawningStart != null)
			OnSpawningStart();
	}


	void HandleSpawningCompleted()
	{
		IsSpawningCompleted = true;
	}


	void HandleEnemyCountChange(int newCount)
	{
		if (IsSpawningCompleted && newCount == 0)
			HandleWaveCleared();
	}


	void HandleWaveCleared()
	{
		StartWave();
	}


	void HandlePlayerDeath(HeartRateTracker hrt, int finalHeartRate)
	{
		// TODO: HOOK UP PLAYER HEALTH
	}
}
