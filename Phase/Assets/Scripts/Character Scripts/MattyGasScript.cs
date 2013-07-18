using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MattyGasScript : MatterScript 
{	
	public GameObject rain; // used for condenstation effects
	private GameObject rainParticles;
	private bool rainActive;
	
	void Start()
	{
		rainActive = false;	
	}
	
	
	public void Condenstation()
	{
		if (!rainActive)
		{
			rainActive = true;
			rainParticles = Instantiate (rain, transform.position, Quaternion.identity) as GameObject;
		}
		
	}
	
	public void StopCondensation()
	{
		if (rainActive)
		{
			Destroy (rainParticles);
			rainActive = false;
		}
	}
	
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
	
	public override bool IceCeilingCollisionResolution()
	{
		// DEATH
		// TODO: Condensation
		
		Debug.Log("Gas-IcyFloor: Need to do condenstation effect");
		Condenstation ();
		return false;
	}
	
	public override bool IcyFloorCollisionResolution()
	{
		// NO EFFECT
		// TODO: Condensation
		
		Debug.Log("Gas-IcyFloor: Need to do condenstation effect");
		Condenstation ();
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
