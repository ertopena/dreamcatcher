using UnityEngine;
using System.Collections;

public class EnemyMotion : MonoBehaviour {


	public GameController gameController;
	public DamageDealer damageDealer;
	public Transform tetherTransform;
	public Transform spriteTransform;

	public float startingDistance = 6f;
	public float enemySpeed = 1f;
	public float rotationSpeed = 0;
	public float spriteRadius = 0.5f;


	private bool isMoving = true;		// TODO: Make this dependent on DamageTaker.


	void Awake()
	{
		if (damageDealer == null)
			damageDealer = GetComponent<DamageDealer>();
	}


	void OnEnable()
	{
		Init();
	}


	void Init()
	{
		// Set distance of the sprite to the tether (which must be at the origin).
		spriteTransform.position = StartingPosition();

		// Rotate the tether randomly so that each enemy approaches from a new angle.
		tetherTransform.Rotate(RandomRotation());
	}


	void FixedUpdate()
	{
		if (isMoving)
		{
			ApplyRotation();
			GetCloserToChild();

			if (IsTouchingChild())
			{
				damageDealer.DamageChild();
				// end motion.
				// death animation.
			}
		}
	}


	void ApplyRotation()
	{
		tetherTransform.Rotate(new Vector3(0, 0, rotationSpeed * Time.fixedDeltaTime));
	}


	void GetCloserToChild()
	{
		Vector3 currentPosition = spriteTransform.position;
		float distanceTraveled = enemySpeed * Time.fixedDeltaTime;

		spriteTransform.position = new Vector3(0, currentPosition.y - distanceTraveled, 0);
	}


	bool IsTouchingChild()
	{
		if (gameController == null)
			gameController = FindObjectOfType<GameController>();

		if (gameController != null)
			return spriteTransform.position.y - spriteRadius < gameController.childRadius;

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
