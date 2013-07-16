using UnityEngine;
using System.Collections;

public class MattySolidScript : MatterScript 
{
	//public MatterScript matter_solid;

	
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
