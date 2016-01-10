using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {

	public delegate void DamageEvent(DamageDealer dealer, int damageDealt);
	public static event DamageEvent OnDamageDealt;


	public int standardDamage = 40;
}
