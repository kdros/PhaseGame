using UnityEngine;
using System.Collections;

public class MatterScript : MonoBehaviour {
	
	public virtual bool CheckpointCollisionResolution()
	{
		return false;
	}
	
	public virtual bool FallingBouldersCollisionResolution()
	{
		return true;
	}
	
	public virtual bool FlamePillarCollisionResolution()
	{
		return true;
	}
	
	public virtual bool GrateCollisionResolution()
	{
		return false;
	}
	
	public virtual bool IceCeilingCollisionResolution()
	{
		return true;
	}
	
	public virtual bool IcyFloorCollisionResolution()
	{
		return false;
	}
	
	public virtual bool LavaCollisionResolution()
	{
		return true;
	}
	
	public virtual bool PitfallCollisionResolution()
	{
		return true;
	}
	
	public virtual bool SpikeCollisionResolution()
	{
		return true;
	}
	
	public virtual bool SwingingMaceCollisionResolution()
	{
		return true;
	}
	
	public virtual bool WindTunnelCollisionResolution()
	{
		return false;
	}
}
