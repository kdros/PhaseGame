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
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if (hasCrossed)
//		{
//			Component checkFlag = gameObject.transform.Find ("CheckFlag");
//			// Check if green flag is visible and make it, if it isn't.
//			if (!(checkFlag.transform.localEulerAngles [2] == -90f) && !hasCrossed)
//			{
////				Vector3 angles = checkFlag.transform.eulerAngles;
////				angles [1] = -90f;
////				checkFlag.transform.eulerAngles = angles;
//				checkFlag.transform.RotateAroundLocal (Vector3.up,-90f);
//			}
//		}
//		else
	}
	
	void OnTriggerEnter (Collider collObjCollider)
	{
		if (collObjCollider.CompareTag ("Player"))
		{
			if (!hasCrossed)
			{	
				hasCrossed = true;
				GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>().SaveSpawnPoint (gameObject.transform.position);
				gameObject.transform.Find ("CheckFlag").transform.RotateAroundLocal (Vector3.up,-90f);
			}
		}
	}
	
//	void saveGame ()
//	{
//		if (!Directory.Exists ("Save"))
//			Directory.CreateDirectory ("Save");
//		StreamWriter sr = new StreamWriter ("Save/currentSave");
//		sr.WriteLine ("{0}", gameObject.transform.position [0]);
//		sr.WriteLine ("{0}", gameObject.transform.position [1]);
//		sr.WriteLine ("{0}", gameObject.transform.position [2]);
//		sr.Close ();
//	}
}
