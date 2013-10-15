using UnityEngine;
using System.Collections;

// PitfallScript is responsible for the behavior of the pitfall prefab
public class PitfallScript : MonoBehaviour 
{
	// audio to play when the player falls through the pitfall
	public AudioClip fallingSound;
	bool hasCrossed;
	
	// Use this for initialization
	void Start ()
	{
		hasCrossed = false;
	}
		
	// Collision detection and resolution.
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
