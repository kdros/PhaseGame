using UnityEngine;
using System.Collections;

public class Global_BegLev1 : MonoBehaviour {
	
	public int numCheckpoints;
	public int hitCheckpoints;
	public GameObject checkpoint_01;
	public GameObject checkpoint_02;
	public GameObject checkpoint_03;
	
	void Start ()
	{
		// Set number of checkpoints that are in the scene
		numCheckpoints = 3;
		
		// Since there is a checkpoint at the beginning of each level, set to 1
		hitCheckpoints = 0;
	}
	
	
	void Update ()
	{	
		print (hitCheckpoints);
	}
}
