using UnityEngine;
using System.Collections;

public class PlayerTouch : MonoBehaviour {

	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
			CheckForHitAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		else
		{
			foreach (Touch t in Input.touches)
			{
				if (t.phase == TouchPhase.Began)
					CheckForHitAt(Camera.main.ScreenToWorldPoint(t.position));
			}
		}
	}


	void CheckForHitAt(Vector3 position)
	{
		RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

		if (hit.collider != null)
		{
			DamageTaker enemyHealth = hit.collider.GetComponentInParent<DamageTaker>();
			if (enemyHealth != null)
				enemyHealth.TakeDamage();
		}
	}
}
