using UnityEngine;
using System.Collections;

public class MattySolidScript : MatterScript 
{
	//public MatterScript matter_solid;
	public float facingRight_yDegrees;
	public float facingLeft_yDegrees;
	
	void Start ()
	{
		//matter_matty = new MatterScript();
		
		facingRight_yDegrees = gameObject.transform.eulerAngles.y + 180;
		facingLeft_yDegrees = gameObject.transform.eulerAngles.y;
		
		//gameObject.transform.position = new Vector3(gameObject.transform.position.x,
										//gameObject.transform.position.y - 100.0f,
										//gameObject.transform.position.z);

		transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,
												facingRight_yDegrees,
												gameObject.transform.eulerAngles.z);
		
		// Set Idle and Walk to loop
		gameObject.animation["Walk"].wrapMode = WrapMode.Loop;
		gameObject.animation["Idle"].wrapMode = WrapMode.Loop;
	   
		// Set Jump to only play once
		gameObject.animation["Jump"].wrapMode = WrapMode.Once;
		
		gameObject.animation.Play("Idle", PlayMode.StopAll);
		
	}
	
	void OnGUI ()
	{
		if(Input.GetKey("left"))
		{
			transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,
												facingLeft_yDegrees,
												gameObject.transform.eulerAngles.z);
			gameObject.animation.Play("Walk", PlayMode.StopAll);
		}
		else if(Input.GetKey("right"))
		{
			transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,
												facingRight_yDegrees,
												gameObject.transform.eulerAngles.z);
			gameObject.animation.Play("Walk", PlayMode.StopAll);
		}
		else if(Input.GetKey("space"))
			gameObject.animation.Play("Jump", PlayMode.StopAll);
		else
			gameObject.animation.Play("Idle", PlayMode.StopAll);
	}
	
	// Functions that do not need to be overriden:
	// FallingBoulders, FlamePillar, Grate, Lava, Pitfall
	// Spike, SwingingMace, WindTunnel
	
	public override bool CheckpointCollisionResolution()
	{
		return false;
	}
	
	public override bool IcyFloorCollisionResolution()
	{
		// No effect
		return false;
	}
	
	public override bool LavaCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public void Die ()
	{
    	Destroy(gameObject);
	}
}