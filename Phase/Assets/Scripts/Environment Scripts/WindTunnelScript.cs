using UnityEngine;
using System.Collections;

public class WindTunnelScript : MonoBehaviour 
{
	public AudioClip whooshSound;
	bool hasCrossed;
	
	// Use this for initialization
	void Start () {
		hasCrossed = false;
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.transform.Rotate(0.0f, 0.0f, 10.0f);
	}
	
	void OnTriggerStay (Collider collObjCollider)
	{
		if (collObjCollider.CompareTag ("Player"))
		{
			if (!hasCrossed)
			{	
				hasCrossed = true;
				Debug.Log("Hit wind tunnel tracker!");
				AudioSource.PlayClipAtPoint(whooshSound, collObjCollider.transform.position);
			}
		}
	}
}
