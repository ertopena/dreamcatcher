using UnityEngine;
using System.Collections;

public class UprightOrienter : MonoBehaviour {

	void FixedUpdate()
	{
		// Set the rotation of this object to be upright.
		this.transform.rotation = Quaternion.identity;
	}
}
