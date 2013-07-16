using UnityEngine;
using System.Collections;

public class MattyScript : MatterScript {
	
	public MatterScript matter_matty;
	
	void Start ()
	{
		matter_matty = new MatterScript();
	}

	void Update ()
	{
	}
	
	// None of the functions, except for the checkpoint, need to be overriden
	
	// TODO: Figure out what goes here
	//public override bool CheckpointCollisionResolution()
	//{
		//return false;
	//}
	
	public void Die ()
	{
    	Destroy(gameObject);
	}
}
