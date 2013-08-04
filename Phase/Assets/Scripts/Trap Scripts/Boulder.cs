using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour 
{	
	void OnCollisionEnter (Collision collision)
	{
		Collider collider = collision.collider;
		if (collider.CompareTag ("DeathPlane"))
			Destroy (gameObject);
	}
}
