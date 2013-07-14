using UnityEngine;
using System.Collections;

public class Global_BegLev1 : MonoBehaviour {

	public float timer;
	public int numCheckpoints;
	public Vector3[] checkpointPos;
	public Vector3 originInScreenCoords;
	
	void Start ()
	{
		timer = 0;
		numCheckpoints = 2;
		
		// Set checkpoint positions here
		checkpointPos[0] = new Vector3(0.0f, 0.0f, 0.0f);
		checkpointPos[1] = new Vector3(1.0f, 1.0f, 1.0f);
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;

	}

}
