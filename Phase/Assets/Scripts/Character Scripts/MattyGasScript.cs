using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MattyGasScript : MatterScript 
{	
	public GameObject rain; // used for condenstation effects
	private GameObject rainParticles;
	private Vector3 windTunnelAccel;
	private bool rainActive;
	
	
	void Start()
	{
		rainActive = false;
		windTunnelAccel = new Vector3(0,500,0);
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
	
	public override bool WindTunnelCollisionResolution(PlatformerController pc)
	{
		// NO EFFECT
		pc.externalAcc = windTunnelAccel;
		return false;
	}
}
