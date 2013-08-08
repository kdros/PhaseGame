using UnityEngine;
using System.Collections;

public class MattyPlasmaScript : MatterScript 
{
	public float lavaMaxTime = 7f;
	public float lavaHeatDecay = 1.0f;
	public float lavaHeatThreshold = 0.5f;
//	public float lavaMaxTime = 20f;
	
	ParticleSystem	embers;
	Light glow;
	MeshRenderer meshRenderer;
//	ClothRenderer clothRenderer;
//	CharacterController charControl;
	Color originalColour, origAmbient;
//	Vector3 center, r;
	bool grounded, isOnLava;
	float curTime, lavaMaxTimeBy2;
	
	void Awake ()
	{
//		clothRenderer = GetComponent<ClothRenderer>();
		meshRenderer = GetComponent<MeshRenderer>();
		embers = GetComponent<ParticleSystem>();
		glow = transform.Find ("Glow").GetComponent<Light>();

		originalColour = glow.color;
		isOnLava = false;
		embers.Stop ();
		curTime = 0f;
		lavaMaxTimeBy2 = lavaMaxTime/2f;
	}
	
	void Update ()
	{
		if (curTime > 0f)
		{
			if (isOnLava)
			{
				if (curTime < lavaMaxTime)
				{	
					curTime += Time.deltaTime;
					if (Mathf.Abs (curTime - lavaMaxTimeBy2) < 0.1f)
						embers.Play ();
				}
				else
				{
					curTime = lavaMaxTime;
					if (!embers.isPlaying)
						embers.Play ();
				}
			}
			else
			{
				curTime -= (lavaHeatDecay*Time.deltaTime);
//				if (Mathf.Abs (curTime - lavaMaxTimeBy2) < 0.1f)
//					embers.loop = false;
			}
			
			glow.color = Color.Lerp (originalColour, Color.red, curTime/lavaMaxTime);
			meshRenderer.material.SetColor ("_Color", glow.color);
		}
		else if (curTime <= 0f)
		{
			curTime = 0f;
			if (!glow.color.Equals (originalColour))
				glow.color = originalColour;
			if (!meshRenderer.material.GetColor ("_Color").Equals (glow.color))
				meshRenderer.material.SetColor ("_Color", glow.color);
		}
	}
	
	public void Reset ()
	{
		if (embers.isPlaying)
			embers.Stop ();
		embers.Clear ();
		glow.color = originalColour;
		curTime = 0f;
	}
	
	public void NotOnLava ()
	{
			Debug.Log ("Exiting Lava!");
			embers.loop = false;
			isOnLava = false;
	}
	
	public override bool FallingBouldersCollisionResolution()
	{
		if ((curTime/lavaMaxTime) < lavaHeatThreshold)
//		if (curTime<=0)
		{
		// DEATH if plasma is not hot
			Reset ();
			return true;
		}
		// Otherwise, destroy boulder.
		return false;
	}
	
	public override bool IceCeilingCollisionResolution()
	{
		// DEATH
		Reset ();
		return true;
	}
	
	public override bool SpikeCollisionResolution()
	{
		// DEATH
		Reset ();
		return true;
	}
	
	public override bool SwingingMaceCollisionResolution()
	{
		// DEATH
		Reset ();
		return true;
	}
	
	public override bool PitfallCollisionResolution()
	{
		// DEATH
		Reset ();
		return true;
	}
	
	public override bool LavaCollisionResolution()
	{
		// NO EFFECT
		Debug.Log("lava collision resolution called");
		if (!isOnLava)
		{
			embers.loop = true;
			curTime += Time.deltaTime;
			isOnLava = true;
		}
		return false;
	}
	
	public override bool FlamePillarCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
		
	public override bool IcyFloorCollisionResolution()
	{
		// NO EFFECT
		return false;
	}
}