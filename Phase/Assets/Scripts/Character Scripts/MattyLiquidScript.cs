using UnityEngine;
using System.Collections;

public class MattyLiquidScript : MatterScript 
{
	
	public virtual bool FallingBouldersCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public virtual bool FlamePillarCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public virtual bool GrateCollisionResolution()
	{
		// NO EFFECT
		// TODO: Need to fall through
		return false;
	}
	
	public virtual bool IceCeilingCollisionResolution()
	{
		// DEATH
		// TODO: Consider using a freezing death animation
		return true;
	}
	
	public virtual bool IcyFloorCollisionResolution()
	{
		// DEATH
		// TODO: Consider using a freezing death animation
		return true;
	}
	
	public virtual bool LavaCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public virtual bool PitfallCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public virtual bool SpikeCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public virtual bool SwingingMaceCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public virtual bool WindTunnelCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
}
