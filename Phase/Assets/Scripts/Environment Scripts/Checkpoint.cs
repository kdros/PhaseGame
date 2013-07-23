using UnityEngine;
using System.Collections;
using System.IO;

public class Checkpoint : MonoBehaviour 
{
	public AudioClip hitCheckpoint;

	bool hasCrossed;
	
	void Start () 
	{
		hasCrossed = false;
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
				AudioSource.PlayClipAtPoint(hitCheckpoint, collObjCollider.transform.position);
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
