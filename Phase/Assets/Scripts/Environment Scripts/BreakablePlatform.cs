using UnityEngine;
using System.Collections;

public class BreakablePlatform : MonoBehaviour 
{
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
		if (collider.CompareTag ("FallingBoulders"))
			rigidbody.isKinematic = false;
	}
}
