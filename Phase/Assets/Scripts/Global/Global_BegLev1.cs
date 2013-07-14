using UnityEngine;
using System.Collections;

public class Global_BegLev1 : MonoBehaviour {

	public float timer;
	public int checkpointHit;
	public Vector3[] checkpointPos;
	public Vector3 originInScreenCoords;
	public GameObject character;
	public bool death;
	
	void Start ()
	{
		timer = 0;
		checkpointHit = 0;
		
		death = false;
		
		// Set checkpoint positions here
		checkpointPos[0] = new Vector3(0.0f, 0.0f, 0.0f);
		checkpointPos[1] = new Vector3(1.0f, 1.0f, 1.0f);
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		//checkpointHit += 1;
		
		if(death == true)
		{
			// Need to restor character to last checkpoint
			Vector3 restorePos = checkpointPos[checkpointHit];
			Instantiate(character, restorePos, Quaternion.identity );
		}
	}
}
