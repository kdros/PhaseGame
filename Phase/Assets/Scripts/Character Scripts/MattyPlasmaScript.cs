using UnityEngine;
using System.Collections;

public class MattyPlasmaScript : MatterScript 
{

	public override bool FallingBouldersCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool FlamePillarCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool GrateCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool IceCeilingCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool IcyFloorCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool LavaCollisionResolution()
	{
		// NO EFFECT
		Debug.Log("lava collision resolution called");
		return false;
	}
	
	public override bool PitfallCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool SpikeCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool SwingingMaceCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool WindTunnelCollisionResolution()
	{
		// NO EFFECT
		return false;
	}

}
