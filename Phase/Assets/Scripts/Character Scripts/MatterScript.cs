using UnityEngine;
using System.Collections;

public class MatterScript : MonoBehaviour {
	
	public virtual void CheckpointCollisionResolution()
	{
		
	}
	
	public virtual void FallingBouldersCollisionResolution()
	{
		Die();
	}
	
	public virtual void FlamePillarCollisionResolution()
	{
		Die();
	}
	
	public virtual void GrateCollisionResolution()
	{
		
	}
	
	public virtual void IceCeilingCollisionResolution()
	{
		Die();
	}
	
	public virtual void IcyFloorCollisionResolution()
	{

	}
	
	public virtual void LavaCollisionResolution()
	{
		Die();
	}
	
	public virtual void PitfallCollisionResolution()
	{
		Die();
	}
	
	public virtual void SpikeCollisionResolution()
	{
		Die();
	}
	
	public virtual void SwingingMaceCollisionResolution()
	{
		Die();
	}
	
	public virtual void WindTunnelCollisionResolution()
	{
		
	}
	
	public void Die ()
	{		
    	Destroy(gameObject);
	}
}
