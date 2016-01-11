using UnityEngine;
using System.Collections;

public class Boomer : MonoBehaviour {

	public AudioClip boomerReadying;
	public float fuseTimer = 2f;

	private DamageDealer damageDealer;
	private DamageTaker damageTaker;
	private AudioSource audioSource;
	private bool isTriggered = false;


	void Awake()
	{
		damageDealer = GetComponent<DamageDealer>();
		damageTaker = GetComponent<DamageTaker>();
		audioSource = GetComponent<AudioSource>();
	}


	void OnEnable()
	{
		damageTaker.OnEnemyDamaged += StartFuse;

		Init();
	}


	void OnDisable()
	{
		damageTaker.OnEnemyDamaged -= StartFuse;

		CancelInvoke();
	}


	void Init()
	{
		isTriggered = false;
	}


	void StartFuse()
	{
		if (!isTriggered)
		{
			isTriggered = true;
			
			Invoke("Explode", fuseTimer);

			if (audioSource != null)
				Invoke("PlayExplodingSound", 0.25f);

			// TODO: Start some coroutine that changes the color or size or whatever.
		}
	}
	void StartFuse(DamageTaker dmgTaker)
	{
		StartFuse();
	}


	void Explode()
	{
		damageDealer.DamageChild();
		// TODO: Write an actual explosion with particles or something.
		this.gameObject.SetActive(false);
	}


	void PlayExplodingSound()
	{
		audioSource.clip = boomerReadying;
		audioSource.Play();
	}
}
