using UnityEngine;
using System.Collections;

// Boulder class to be attached to the boulder prefab. Boulder is responsible for doing
// collision against the death plane.
public class Boulder : MonoBehaviour 
{	
	// Collision detection and resolution.
	void OnCollisionEnter (Collision collision)
	{
		Collider collider = collision.collider;
		if (collider.CompareTag ("DeathPlane"))
			Destroy (gameObject);
	}
}
