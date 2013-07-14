using UnityEngine;
using System.Collections;

public class Global_BegLev1 : MonoBehaviour {
	
	public int numCheckpoints;
	public int checkpointHit;
	public Vector3[] checkpointPos;
	public GameObject mattySolid;
	public bool mattySolidDeath;
	
	void Start ()
	{
		// Set number of checkpoints that are in the scene
		numCheckpoints = 1;
		
		// Since there is a checkpoint at the beginning of each level, set to 1
		checkpointHit = 1;
		
		// Set checkpoint positions here
		checkpointPos = new Vector3[1]; 
		checkpointPos[0] = new Vector3(-8.523637f, 2.04423f, 3.322249f);
		
		mattySolidDeath = false;
	}
	
	
	void Update ()
	{	
		//checkpointHit += 1;
		
		if(mattySolidDeath == true)
		{
			Debug.Log ("Matty Solid Character was destroyed! Restoring...");
			// Need to restor character to last checkpoint
			Vector3 restorePos = checkpointPos[0];
			Instantiate(mattySolid, restorePos, Quaternion.identity );
			
			// Reset boolean variable
			mattySolidDeath = false;
		}
	}
}
