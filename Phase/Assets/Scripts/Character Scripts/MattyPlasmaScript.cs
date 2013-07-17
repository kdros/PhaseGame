using UnityEngine;
using System.Collections;

public class MattyPlasmaScript : MatterScript 
{
	public float lavaMaxTime = 10f;
	
	ParticleSystem	embers;
	Light glow;
	ClothRenderer clothRenderer;
//	CharacterController charControl;
	Color originalColour, origAmbient;
//	Vector3 center, r;
	bool grounded, isOnLava;
	float curTime;
	
	void Start ()
	{
		clothRenderer = GetComponent<ClothRenderer>();
		embers = GetComponent<ParticleSystem>();
		glow = transform.Find ("Glow").GetComponent<Light>();

		originalColour = glow.color;
		isOnLava = false;
		embers.Stop ();
		curTime = 0f;
	}
	
	void Update ()
	{
		Color lightColour = glow.color;
		if (curTime > 0f)
		{
			if (isOnLava)
			{
				if (curTime < lavaMaxTime)
					curTime += Time.deltaTime;
				else
					curTime = lavaMaxTime;
			}
			else
				curTime -= Time.deltaTime;
			
			glow.color = Color.Lerp (originalColour, Color.red, curTime/lavaMaxTime);
			clothRenderer.material.SetColor ("_Color", glow.color);//.Lerp (originalMat, LavaMat, curTime/lavaMaxTime);
		}
		else
		{
			curTime = 0f;
			if (!glow.color.Equals (originalColour))
				glow.color = originalColour;
			if (!clothRenderer.material.GetColor ("_Color").Equals (glow.color))
				clothRenderer.material.SetColor ("_Color", glow.color);
		}
	}
	
	public void NotOnLava ()
	{
			Debug.Log ("Exiting Lava!");
			embers.loop = false;
			isOnLava = false;
	}

	public override bool FallingBouldersCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool IceCeilingCollisionResolution()
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
		// DEATH
		return true;
	}
	
	public override bool LavaCollisionResolution()
	{
		// NO EFFECT
		Debug.Log("lava collision resolution called");
		embers.loop = true;
		embers.Play ();
		curTime += Time.deltaTime;
		isOnLava = true;
		return false;
	}
	
	public override bool FlamePillarCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool GrateCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool IcyFloorCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
	
	public override bool PitfallCollisionResolution()
	{
		// DEATH
		return true;
	}
	
	public override bool WindTunnelCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
}