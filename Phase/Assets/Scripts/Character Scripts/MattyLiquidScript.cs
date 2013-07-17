using UnityEngine;
using System.Collections;

public class MattyLiquidScript : MatterScript 
{
	
	public override bool FallingBouldersCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool FlamePillarCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool GrateCollisionResolution(Collider playerCollider, Collider grateCollider)
	{
		//Need to fall through
		Physics.IgnoreCollision(playerCollider, grateCollider);
		return false;
	}
	
	public override bool IceCeilingCollisionResolution()
	{
		// DEATH
		// TODO: Consider using a freezing death animation
		return true;
	}
	
	public override bool IcyFloorCollisionResolution()
	{
		// DEATH
		// TODO: Consider using a freezing death animation
		return true;
	}
	
	public override bool LavaCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool PitfallCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool SpikeCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool SwingingMaceCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool WindTunnelCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
}
