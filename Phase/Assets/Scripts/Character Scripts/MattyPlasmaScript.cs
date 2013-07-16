using UnityEngine;
using System.Collections;

public class MattyPlasmaScript : MatterScript 
{

	public virtual bool FallingBouldersCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public virtual bool FlamePillarCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public virtual bool GrateCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public virtual bool IceCeilingCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public virtual bool IcyFloorCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public virtual bool LavaCollisionResolution()
	{
		// DEATH
		return false;
	}
	
	public virtual bool PitfallCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public virtual bool SpikeCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public virtual bool SwingingMaceCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public virtual bool WindTunnelCollisionResolution()
	{
		// NO EFFECT
		return false;
	}

}
