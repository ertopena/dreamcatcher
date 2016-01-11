using UnityEngine;
using System.Collections;

public class EnemyAnimator : MonoBehaviour {

	public delegate void EnemyAnimationEvent();
	public event EnemyAnimationEvent OnDamageAnimationEnd;


	public float shakeRadius = 0.05f;

	private DamageTaker damageTaker;
	private Vector3 positionWhenDamaged;
	private EnemyMotion enemyMotion;
	private SpriteRenderer enemySprite;
	private Transform enemyTransform;


	void Awake()
	{
		damageTaker = GetComponent<DamageTaker>();
		enemyMotion = GetComponent<EnemyMotion>();
		enemySprite = enemyMotion.GetComponent<SpriteRenderer>();
		enemyTransform = enemyMotion.spriteTransform;
	}


	void OnEnable()
	{
		damageTaker.AnimateDamage = DoHitAnimation;
		damageTaker.AnimateDeath = DoDeathAnimation;


		positionWhenDamaged = Vector2.zero;
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
			OnDamageAnimationEnd();
	}


	void DoDeathAnimation()
	{
		StopAllCoroutines();
		StartCoroutine(CoDoDeathAnimation());
	}


	IEnumerator CoDoDeathAnimation()
	{
		// TODO: Write actual death animations.
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


		this.gameObject.SetActive(false);
	}


	void ShakeEnemy()
	{
		float randomAngle = Random.Range(0, 2 * Mathf.PI);
		Vector3 shakeDisplacement = new Vector3(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle), 0);
		shakeDisplacement *= shakeRadius;

		enemyTransform.localPosition = positionWhenDamaged - shakeDisplacement;
	}
}
