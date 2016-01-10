using UnityEngine;
using System.Collections;

public class EnemyCounter : MonoBehaviour {

	public delegate void EnemyCountEvent(int newCount);
	public static event EnemyCountEvent OnEnemyCountUpdate;

	public static int Count { get; private set; }


	void OnEnable()
	{
		UpdateCount(1);
	}


	void OnDisable()
	{
		UpdateCount(-1);
	}


	void UpdateCount(int change)
	{
		if (Count + change >= 0)
		{
			Count += change;

			if (OnEnemyCountUpdate != null)
				OnEnemyCountUpdate(Count);
		}
	}
}
