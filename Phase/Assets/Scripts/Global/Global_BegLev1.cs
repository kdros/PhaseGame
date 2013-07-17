using UnityEngine;
using System.Collections;

public class Global_BegLev1 : MonoBehaviour {
	
	public int numCheckpoints;
	public int hitCheckpoints;
	public GameObject checkpoint_01;
	public GameObject checkpoint_02;
	public GameObject checkpoint_03;
	public bool checkpoint_01_hit;
	public bool checkpoint_02_hit;
	public bool checkpoint_03_hit;
	
	void Start ()
	{
		// Set number of checkpoints that are in the scene
		numCheckpoints = 3;
		
		// Since there is a checkpoint at the beginning of each level, set to 1
		hitCheckpoints = 0;
		
		checkpoint_01_hit = false;
		checkpoint_02_hit = false;
		checkpoint_03_hit = false;
	}
	
	
	void Update ()
	{	
		print (hitCheckpoints);
	}
}
