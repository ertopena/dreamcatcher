using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {

	public delegate void DamageEvent(DamageDealer dealer, int damageDealt);
	public static event DamageEvent OnDamageToChildAttempted;


	public int standardDamage = 40;


	public void DamageChild()
	{
		OnDamageToChildAttempted(this, EffectiveDamage());
	}


	int EffectiveDamage()
	{
		// TODO: Expand to add periods of reduced damage or invincibility.
		return standardDamage;
	}
}
