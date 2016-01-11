using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class DamageTaker : MonoBehaviour {

	public delegate void AnimationDelegate();
	public AnimationDelegate AnimateDamage;
	public AnimationDelegate AnimateDeath;

	public delegate void EnemyDamageEvent(DamageTaker damageTaker);
	public event EnemyDamageEvent OnEnemyDamaged;
	public event EnemyDamageEvent OnEnemyDeath;


	public int Health { get; private set; }
	public int baseHealth = 5;
	public AudioClip enemyHurt;
    public AudioClip enemyDeath;

	private EnemyMotion enemyMotion;
	private SpriteRenderer enemySprite;
	private Transform enemyTransform;
	
    private AudioSource enemyAudio;


	void Awake()
	{
		enemyMotion = GetComponent<EnemyMotion>();
		enemySprite = enemyMotion.GetComponent<SpriteRenderer>();
		enemyTransform = enemyMotion.spriteTransform;
        enemyAudio = GetComponent<AudioSource>();
	}


	void OnEnable()
	{
		Init();
	}


	void Init()
	{
		Health = baseHealth;
		GetComponentInChildren<Collider2D>().enabled = true;

		if (enemyAudio != null)
			enemyAudio.clip = enemyHurt;
	}


	public void TakeDamage()
	{
		Health--;
		
		if (Health <= 0)
			Die();
		else
		{
			if (AnimateDamage != null)
				AnimateDamage();

            if (enemyAudio != null)
                enemyAudio.Play();

			if (OnEnemyDamaged != null)
				OnEnemyDamaged(this);
		}
	}


	void Die()
	{
		if (AnimateDeath != null)
			AnimateDeath();
		
		GetComponentInChildren<Collider2D>().enabled = false;

        if (enemyAudio != null)
        {
            enemyAudio.clip = enemyDeath;
            enemyAudio.Play();
        }

		if (OnEnemyDeath != null)
			OnEnemyDeath(this);
	}
}
