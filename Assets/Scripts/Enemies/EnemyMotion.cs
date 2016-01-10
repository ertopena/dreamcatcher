using UnityEngine;
using System.Collections;

public class EnemyMotion : MonoBehaviour {


	public GameController gameController;
	public Transform tetherTransform;
	public Transform spriteTransform;

	public bool IsMoving { get; private set; }
	public float startingDistance = 6f;
	public float enemySpeed = 1f;
	public float rotationSpeed = 0;
	public float spriteRadius = 0.5f;

	private DamageDealer damageDealer;
	private DamageTaker damageTaker;


	void Awake()
	{
		damageDealer = GetComponent<DamageDealer>();
		damageTaker = GetComponent<DamageTaker>();
	}


	void OnEnable()
	{
		damageTaker.OnEnemyDamaged += SuspendMotion;
		damageTaker.OnDamageAnimationEnd += ResumeMotion;
		damageTaker.OnEnemyDeath += SuspendMotion;

		Init();
	}


	void OnDisable()
	{
		damageTaker.OnEnemyDamaged -= SuspendMotion;
		damageTaker.OnDamageAnimationEnd -= ResumeMotion;
		damageTaker.OnEnemyDeath -= SuspendMotion;
	}


	void Init()
	{
		// If the GameController is not set, ask a parent.
		if (gameController == null)
			gameController = GetComponentInParent<EnemyManager>().gameController;

		// Set distance of the sprite to the tether (which must be at the origin).
		spriteTransform.localPosition = StartingPosition();

		// Rotate the tether randomly so that each enemy approaches from a new angle.
		tetherTransform.Rotate(RandomRotation());

		// Start motion.
		IsMoving = true;
	}


	void FixedUpdate()
	{
		if (IsMoving)
		{
			ApplyRotation();
			GetCloserToChild();

			if (IsTouchingChild())
			{
				damageDealer.DamageChild();
				SuspendMotion();
			}
		}
	}


	void SuspendMotion()
	{
		IsMoving = false;
	}
	void SuspendMotion(DamageTaker dmgTaker)
	{
		SuspendMotion();
	}


	void ResumeMotion(DamageTaker dmgTaker)
	{
		IsMoving = true;
	}


	void ApplyRotation()
	{
		tetherTransform.Rotate(new Vector3(0, 0, rotationSpeed * Time.fixedDeltaTime));
	}


	void GetCloserToChild()
	{
		Vector3 currentPosition = spriteTransform.localPosition;
		float distanceTraveled = enemySpeed * Time.fixedDeltaTime;

		spriteTransform.localPosition = new Vector3(0, currentPosition.y - distanceTraveled, 0);
	}


	bool IsTouchingChild()
	{
		if (gameController == null)
			gameController = FindObjectOfType<GameController>();

		if (gameController != null)
			return spriteTransform.localPosition.y - spriteRadius < gameController.childRadius;

		Debug.Log("Enemy failed to find the GameController. Check that everything's hooked up right.");
		return false;
	}


	Vector3 StartingPosition()
	{
		return new Vector3(0, startingDistance, 0);
	}


	Vector3 RandomRotation()
	{
		return new Vector3(0, 0, Random.Range(0f, 360f));
    }
}
