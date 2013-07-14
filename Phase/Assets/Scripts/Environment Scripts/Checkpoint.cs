using UnityEngine;
using System.Collections;
using System.IO;

public class Checkpoint : MonoBehaviour 
{
	bool hasCrossed;
//	uint identifier;
	// Use this for initialization
	void Start () 
	{
		hasCrossed = false;
//		identifier = 0;			// Identifier = 0 means the start of the level.
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (hasCrossed)
		{
			Component checkFlag = gameObject.transform.Find ("CheckFlag");
			// Check if green flag is visible and make it, if it isn't.
			if (!(checkFlag.transform.eulerAngles [2] == -90f))
				makeGreenFlagVisible ();
		}
//		else
	}
	
	void OnCollisionEnter (Collision collisObj)
	{
		Collider collObjCollider = collisObj.collider;
		
		if (collObjCollider.CompareTag ("Player"))
		{
			if (!hasCrossed)
			{	
				hasCrossed = true;
				saveGame ();
			}
		}
	}
	
	void saveGame ()
	{
		if (!Directory.Exists ("Save"))
			Directory.CreateDirectory ("Save");
		StreamWriter sr = new StreamWriter ("Save/currentSave");
		sr.WriteLine ("{0}", gameObject.transform.position [0]);
		sr.WriteLine ("{0}", gameObject.transform.position [1]);
		sr.WriteLine ("{0}", gameObject.transform.position [2]);
		sr.Close ();
	}
	
	void makeGreenFlagVisible ()
	{
		;
	}
}
