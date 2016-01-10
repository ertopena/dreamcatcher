using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class DamageTaker : MonoBehaviour {

	public delegate void EnemyDamageEvent(DamageTaker damageTaker);
	public event EnemyDamageEvent OnEnemyDamaged;
	public event EnemyDamageEvent OnDamageAnimationEnd;
	public event EnemyDamageEvent OnEnemyDeath;


	public int Health { get; private set; }
	public int baseHealth = 5;
	public float shakeRadius = 0.05f;

	private EnemyMotion enemyMotion;
	private SpriteRenderer enemySprite;
	private Transform enemyTransform;
	private Vector3 positionWhenDamaged;


	void Awake()
	{
		enemyMotion = GetComponent<EnemyMotion>();
		enemySprite = enemyMotion.GetComponent<SpriteRenderer>();
		enemyTransform = enemyMotion.spriteTransform;
	}


	void OnEnable()
	{
		Health = baseHealth;
		positionWhenDamaged = Vector2.zero;
	}

	public void TakeDamage()
	{
		Health--;
		
		if (Health <= 0)
			Die();
		else
		{
			DoHitAnimation();

			if (OnEnemyDamaged != null)
				OnEnemyDamaged(this);
		}
	}


	void DoHitAnimation()
	{
		StopAllCoroutines();
		StartCoroutine(CoDoHitAnimation());
	}


	IEnumerator CoDoHitAnimation()
	{
		yield return new WaitForFixedUpdate();


		if (positionWhenDamaged == Vector3.zero)
			positionWhenDamaged = enemyTransform.localPosition;


		for (int i = 0; i < 20; i++)
		{
			ShakeEnemy();
			yield return new WaitForFixedUpdate();
		}


		enemyTransform.localPosition = positionWhenDamaged;
		positionWhenDamaged = Vector3.zero;


		if (OnDamageAnimationEnd != null)
			OnDamageAnimationEnd(this);
	}


	void DoDeathAnimation()
	{
		StopAllCoroutines();
		StartCoroutine(CoDoDeathAnimation());
	}


	IEnumerator CoDoDeathAnimation()
	{
		yield return new WaitForFixedUpdate();


		if (positionWhenDamaged == Vector3.zero)
			positionWhenDamaged = enemyTransform.localPosition;


		for (int i = 0; i < 20; i++)
		{
			ShakeEnemy();
			yield return new WaitForFixedUpdate();
		}


		enemyTransform.localPosition = positionWhenDamaged;
		positionWhenDamaged = Vector3.zero;
	}


	void ShakeEnemy()
	{
		float randomAngle = Random.Range(0, 2 * Mathf.PI);
		Vector3 shakeDisplacement = new Vector3(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle), 0);
		shakeDisplacement *= shakeRadius;

		enemyTransform.localPosition = positionWhenDamaged - shakeDisplacement;
	}


	void Die()
	{
		// TODO;
		DoDeathAnimation();
		
		GetComponentInChildren<Collider2D>().enabled = false;

		if (OnEnemyDeath != null)
			OnEnemyDeath(this);
	}
}
