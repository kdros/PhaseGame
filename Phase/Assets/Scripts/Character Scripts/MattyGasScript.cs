using UnityEngine;
using System.Collections;

public class MattyGasScript : MatterScript 
{
	
	//public GameObject waterDroplets; // used for condenstation effects
	
	// Will instantiate waterDroplets.
	// Called by IceCeilingCollisionResolution()
	//void Condenstation()
	//{
	//	
	//}
	
	public virtual bool FallingBouldersCollisionResolution()
	{
		// No Effect
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
		return false;
	}
	
	public virtual bool IceCeilingCollisionResolution()
	{
		// DEATH
		// TODO: Condensation
		
		Debug.Log("Gas-IcyFloor: Need to do condenstation effect");
		return false;
	}
	
	public virtual bool IcyFloorCollisionResolution()
	{
		// NO EFFECT
		// TODO: Condensation
		
		Debug.Log("Gas-IcyFloor: Need to do condenstation effect");
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
		// No EFFECT
		return false;
	}
	
	public virtual bool WindTunnelCollisionResolution()
	{
		// NO EFFECT
		// TODO: Gas will need to float in the direction
		// of the wind.
		return false;
	}
}
