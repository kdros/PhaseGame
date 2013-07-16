using UnityEngine;
using System.Collections;

public class IcicleParticle : MonoBehaviour 
{
	float curLife;
	// Use this for initialization
	void Start () 
	{
		curLife = 1.0f;
	}
	
	void Melt ()
	{
		if (curLife > 0.1f)
		{
			curLife -= 0.1f;
//			float originalY = transform.position.y;
			transform.localScale *= curLife;
//				transform.position = new Vector3 (transform.position.x, originalY, transform.position.z);
		}
		else
		{
			if (transform.parent != null)
				transform.parent = null;
			Destroy (gameObject);
		}
	}
	
	void OnCollisionEnter (Collision collObj) 
	{
		Collider collObjCollider = collObj.collider;
		if (collObjCollider.CompareTag ("Ground") || collObjCollider.CompareTag ("Platform"))
			rigidbody.useGravity = false;
//		Debug.Log (collObjCollider.gameObject.name);
//		Debug.Log (collObjCollider.gameObject.tag);
	}
	
	void OnTriggerEnter (Collider collObjCollider)
	{
		if (collObjCollider.CompareTag ("Lava"))
			Melt ();
	}
	
	void OnTriggerStay (Collider collObjCollider)
	{
		if (collObjCollider.CompareTag ("Lava"))
			Melt ();
	}
}
