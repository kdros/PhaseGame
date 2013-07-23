using UnityEngine;
using System.Collections;

public class MattyPlasmaScript : MatterScript 
{
	public float lavaMaxTime = 10f;
	
	ParticleSystem	embers;
	Light glow;
	MeshRenderer meshRenderer;
//	ClothRenderer clothRenderer;
//	CharacterController charControl;
	Color originalColour, origAmbient;
//	Vector3 center, r;
	bool grounded, isOnLava;
	float curTime;
	
	void Start ()
	{
//		clothRenderer = GetComponent<ClothRenderer>();
		meshRenderer = GetComponent<MeshRenderer>();
		embers = GetComponent<ParticleSystem>();
		glow = transform.Find ("Glow").GetComponent<Light>();

		originalColour = glow.color;
		isOnLava = false;
		embers.Stop ();
		curTime = 0f;
	}
	
	void Update ()
	{
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
			meshRenderer.material.SetColor ("_Color", glow.color);
		}
		else
		{
			curTime = 0f;
			if (!glow.color.Equals (originalColour))
				glow.color = originalColour;
			if (!meshRenderer.material.GetColor ("_Color").Equals (glow.color))
				meshRenderer.material.SetColor ("_Color", glow.color);
		}
	}
	
	void Reset ()
	{
		if (embers.isPlaying)
			embers.Stop ();
		glow.color = originalColour;
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
		Reset ();
		return true;
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
			embers.Play ();
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