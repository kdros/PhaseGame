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
	
	public override bool FallingBouldersCollisionResolution()
	{
		// No Effect
		return false;
	}
	
	public override bool FlamePillarCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool GrateCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool IceCeilingCollisionResolution()
	{
		// DEATH
		// TODO: Condensation
		
		Debug.Log("Gas-IcyFloor: Need to do condenstation effect");
		return false;
	}
	
	public override bool IcyFloorCollisionResolution()
	{
		// NO EFFECT
		// TODO: Condensation
		
		Debug.Log("Gas-IcyFloor: Need to do condenstation effect");
		return false;
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
		// DEATH
		return true;
	}
	
	public override bool SwingingMaceCollisionResolution()
	{
		// No EFFECT
		return false;
	}
	
	public override bool WindTunnelCollisionResolution()
	{
		// NO EFFECT
		// TODO: Gas will need to float in the direction
		// of the wind.
		return false;
	}
}
