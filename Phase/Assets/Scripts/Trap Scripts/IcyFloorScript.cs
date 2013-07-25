using UnityEngine;
using System.Collections;

public class IcyFloorScript : MonoBehaviour
{
	public AudioClip slidingSound;
	bool hasCrossed;
	
	// Use this for initialization
	void Start () 
	{
		hasCrossed = false;
	}
	
	void OnTriggerStay (Collider collObjCollider)
	{
		if (collObjCollider.CompareTag ("Player"))
		{
			if (!hasCrossed)
			{	
				hasCrossed = true;
				Debug.Log("Sliding on ice!!");
				AudioSource.PlayClipAtPoint(slidingSound, collObjCollider.transform.position);
			}
		}
	}
}
