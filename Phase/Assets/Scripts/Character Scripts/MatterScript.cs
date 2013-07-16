using UnityEngine;
using System.Collections;

public class MatterScript : MonoBehaviour 
{
	public virtual bool CheckpointCollisionResolution()
	{
		return false;
	}
	
	public virtual bool FallingBouldersCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public virtual bool FlamePillarCollisionResolution()
	{
		// DEATH
		return true;
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
		return true;
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
