using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour 
{
	// Use this for initialization
	void OnCollisionEnter (Collision collision)
	{
		Collider collider = collision.collider;
		if ((!collider.CompareTag ("Player")) && (!collider.tag.Contains ("Matty")))
			Destroy (collider.gameObject);	
	}
}
