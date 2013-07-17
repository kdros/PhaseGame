using UnityEngine;
using System.Collections;

/*
 * Base class for each state of matter. Use for collision resolution.
 * Each function will return true if the collision causes death, otherwise return false. 
 */
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
	
	// The colliders will be used by MattyLiquidScript to make liquid fall through.
	// The collision check is re-enabled in MainPlayerScript
	public virtual bool GrateCollisionResolution(Collider playerCollider, Collider grateCollider)
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
