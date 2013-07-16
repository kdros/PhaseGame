using UnityEngine;
using System.Collections;

public class IcicleParticle : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	void OnCollisionEnter (Collision collObj) 
	{
		Collider collObjCollider = collObj.collider;
		if (collObjCollider.CompareTag ("Ground") || collObjCollider.CompareTag ("Platform"))
			rigidbody.useGravity = false;
		
//		Debug.Log (collObjCollider.gameObject.name);
//		Debug.Log (collObjCollider.gameObject.tag);
	}
}
