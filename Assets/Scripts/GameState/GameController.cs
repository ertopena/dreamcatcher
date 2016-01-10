using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public delegate void GameStateEvent();
	public event GameStateEvent OnLevelStart;
	public event GameStateEvent OnLevelComplete;
	public event GameStateEvent OnPlayerDeath;


	public HeartRateTracker heartRateTracker;


	public float childRadius = 0.5f;


	void OnEnable()
	{
		heartRateTracker.OnLethalHeartRate += HandlePlayerDeath;
	}


	void OnDisable()
	{
		heartRateTracker.OnLethalHeartRate -= HandlePlayerDeath;
	}


	void StartLevel()
	{
		if (OnLevelStart != null)
			OnLevelStart();
	}


	void HandlePlayerDeath(HeartRateTracker hrt, int finalHeartRate)
	{
		// TODO;
	}
}
