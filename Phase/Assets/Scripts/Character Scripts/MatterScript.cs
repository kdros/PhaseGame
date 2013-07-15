using UnityEngine;
using System.Collections;

public class MatterScript : MonoBehaviour {
	
	public virtual bool CheckpointCollisionResolution()
	{
		return false;
	}
	
	public virtual bool FallingBouldersCollisionResolution()
	{
		return false;
	}
	
	public virtual bool FlamePillarCollisionResolution()
	{
		return false;
	}
	
	public virtual bool GrateCollisionResolution()
	{
		return false;
	}
	
	public virtual bool IceCeilingCollisionResolution()
	{
		return false;
	}
	
	public virtual bool IcyFloorCollisionResolution()
	{
		return false;
	}
	
	public virtual bool LavaCollisionResolution()
	{
		return false;
	}
	
	public virtual bool PitfallCollisionResolution()
	{
		return true;
	}
	
	public virtual bool SpikeCollisionResolution()
	{
		return false;
	}
	
	public virtual bool SwingingMaceCollisionResolution()
	{
		return false;
	}
	
	public virtual bool WindTunnelCollisionResolution()
	{
		return false;
	}
}
