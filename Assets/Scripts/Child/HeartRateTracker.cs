using UnityEngine;
using System.Collections;

public class HeartRateTracker : MonoBehaviour {

	public delegate void HeartRateEvent(HeartRateTracker tracker, int heartRate);
	public event HeartRateEvent OnHeartRateUpdated;
	public event HeartRateEvent OnLethalHeartRate;


	public int HeartRate { get; private set; }

	public GameController gameController;
	public int startingHeartRate = 60;
	public int lethalHeartRate = 200;
	public float rateOfRecovery = 2.0f;


	void OnEnable()
	{
		gameController.OnGameStart += Init;		// TODO: Probably change this to OnGameStart
		DamageDealer.OnDamageToChildAttempted += RaiseHeartRate;
	}


	void OnDisable()
	{
		gameController.OnGameStart -= Init;
		DamageDealer.OnDamageToChildAttempted -= RaiseHeartRate;
	}


	void Init()
	{
		SetHeartRate(startingHeartRate);
		RecoverDamage();
	}


	void SetHeartRate(int newHeartRate)
	{
		HeartRate = newHeartRate;

		if (OnHeartRateUpdated != null)
			OnHeartRateUpdated(this, HeartRate);

		if (HeartRate >= lethalHeartRate)
			Die();
	}


	void RaiseHeartRate(int damage)
	{
		SetHeartRate(HeartRate + damage);
	}
	void RaiseHeartRate(DamageDealer dealer, int damage)
	{
		RaiseHeartRate(damage);
	}


	void Die()
	{
		CancelInvoke();
		if (OnLethalHeartRate != null)
			OnLethalHeartRate(this, HeartRate);
	}


	void RecoverDamage()
	{
		if (HeartRate > startingHeartRate)
			RaiseHeartRate(-1);

		Invoke("RecoverDamage", rateOfRecovery);
	}
}
