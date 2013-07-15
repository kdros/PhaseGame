using UnityEngine;
using System.Collections;

public class MattySolidScript : MatterScript {
	
	public MatterScript matter_solid;
	
	void Start()
	{
		matter_solid = new MatterScript();
	}
	
    void Update()
	{
	
    }
	
	// Functions that do not need to be overriden:
	// FallingBoulders, FlamePillar, Grate, Lava, Pitfall
	// Spike, SwingingMace, WindTunnel
	
	public override bool CheckpointCollisionResolution()
	{
		return false;
	}

	public override bool IceCeilingCollisionResolution()
	{
		// Shatters ice ceiling
		return false;
	}
	
	public override bool IcyFloorCollisionResolution()
	{
		// Slides on ice floor
		BroadcastMessage("SpeedUp", 18.0f);
		return false;
	}
	
	public void Die ()
	{
		//GameObject obj = GameObject.Find("GlobalObject_BegLev1");
		//Global_BegLev1 g = obj.GetComponent<Global_BegLev1>();
		//g.mattySolidDeath = true;
		
		
    	Destroy(gameObject);
	}
}
