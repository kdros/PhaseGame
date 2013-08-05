using UnityEngine;
using System.Collections;

public class BreakablePlatform : MonoBehaviour 
{
	public string[] platformBreakers;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnCollisionEnter (Collision collision)
	{
		Collider collider = collision.collider;
		if (platformBreakers != null)
			foreach (string breakerTag in platformBreakers)
				if (collider.CompareTag (breakerTag))
				{
					rigidbody.isKinematic = false;
					break;
				}	
	}
}
