using UnityEngine;
using System.Collections;

public class PitfallScript : MonoBehaviour {
	
	public AudioClip fallingSound;
	bool hasCrossed;
	
	// Use this for initialization
	void Start ()
	{
		hasCrossed = false;
	}
	
	// Update is called once per frame
	void Update () {
	//AudioSource.PlayClipAtPoint(fallingSound, gameObject.transform.position);
	}
	
	void OnTriggerEnter (Collider collObjCollider)
	{
		if (collObjCollider.CompareTag ("Player"))
		{
			if (!hasCrossed)
			{	
				hasCrossed = true;
				Debug.Log("Falling into Pitfall!!");
				AudioSource.PlayClipAtPoint(fallingSound, collObjCollider.transform.position);
			}
		}
	}
}
