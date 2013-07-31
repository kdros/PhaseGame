using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Central Player Script.
 * Functionalities:
 * 1. managing the transition between different states of matter. 
 * 2. moving the base character (& the states of matter). That is, deal with locomotion and use the various scripts attached to 
 * each state of matter for collision handling.
 * 
 * <TODO: Determine additional tasks to do in this class>
 * <TODO: All the states of matter prefabs will only be used for collision detection. The locomotion is handled through this class.>
 */

public class MainPlayerScript : MonoBehaviour {
	
	/// <summary>
	/// Public Variables: Need to be set up by using the inspector
	/// </summary>	
	Vector3 spawnPoint;		// Spawn location
	
	public GameObject phaseEffect; 		// Particle effect for state transition
	public GameObject deathExplosion; 	// Explosion prefab used for death animation
	public GameObject plasmaMatty;
	public GameObject gasMatty;			
	public GameObject liquidMatty;
	public GameObject solidMatty;
	public GameObject defaultMatty;
	
	public AudioClip slidingSound;
	
	/// <summary>
	/// Public Variables: Set upon initialization
	/// </summary>

	[System.NonSerialized]
	public GameObject m_gasMatty;
	[System.NonSerialized]
	public GameObject m_liquidMatty;
	[System.NonSerialized]
	public GameObject m_solidMatty;
	[System.NonSerialized]
	public GameObject m_plasmaMatty;
	[System.NonSerialized]
	public GameObject m_defaultMatty;

	public enum State {Default, Solid, Liquid, Gas, Plasma};

	/// <summary>
	/// Internal variables
	/// </summary>
	
	protected MattyPlasmaScript m_plasmaMattyScript;	// class associated with Plasma Matty (place holder)
	protected MattySolidScript m_solidMattyScript;		// class associated with Solid Matty
	protected MattyLiquidScript m_liquidMattyScript;	// class associated with Liquid Matty
	protected MattyGasScript m_gasMattyScript;			// class associated with Gas Matty
	protected MattyScript m_defaultMattyScript;			// class associated with default Matty
	protected PlatformerController m_platCtrlScript;	// class associated with PlatformerController
	
	private int m_currentState;							// keep track of the current state	
	private bool collidedWithGrates;					// set to true if liquid state collided with grates
	private bool[] changeFlags;
	
	CameraFollow m_camera;
	Vector3 spawnPosition;
	private bool playerDead;
	Director dir;
	float origWalkSpeed, origExtraHeight;
	
	// keep track of boulder colliders that are being ignored
	List<Collider> ignoredBoulders;
	
	void Awake ()
	{
		changeFlags = new bool [5];
		for (int i = 0; i < 5; i ++)
			changeFlags [i] = true;

		ignoredBoulders = new List<Collider>();
	}
	
	// Use this for initialization
	void Start () 
	{
		m_currentState = (int)State.Default;
				
		// instantiate each state of matter
		m_defaultMatty = Instantiate (defaultMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
		m_solidMatty = Instantiate (solidMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
		m_liquidMatty = Instantiate (liquidMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
		m_gasMatty = Instantiate (gasMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
		m_plasmaMatty = Instantiate (plasmaMatty, gameObject.transform.position, Quaternion.identity) as GameObject;		
		
		// find the class associated with each state of matter
		m_defaultMattyScript = m_defaultMatty.GetComponent<MattyScript>();
		m_solidMattyScript = m_solidMatty.GetComponent<MattySolidScript>();
		m_liquidMattyScript = m_liquidMatty.GetComponent<MattyLiquidScript>();
		m_gasMattyScript = m_gasMatty.GetComponent<MattyGasScript>();
		m_plasmaMattyScript = m_plasmaMatty.GetComponent<MattyPlasmaScript>();
		m_platCtrlScript = gameObject.GetComponent<PlatformerController>();
		
		// Temp: Make sure this collider does not collide with each state of matter's colliders
		Physics.IgnoreCollision(collider, m_defaultMatty.collider);
		Physics.IgnoreCollision(collider, m_solidMatty.collider);
		Physics.IgnoreCollision(collider, m_plasmaMatty.collider);
				
		playerDead = false;
		m_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow>();
		dir = GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>();

		collidedWithGrates = false;
		
		origWalkSpeed = m_platCtrlScript.movement.walkSpeed;
		origExtraHeight = m_platCtrlScript.jump.extraHeight;
		spawnPoint = new Vector3 ();
		spawnPoint = dir.GetSpawnPoint ();
		m_platCtrlScript.SetSpawnPoint (spawnPoint, true);
		
		enableState ((int)State.Default);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!playerDead)
		{
			bool stateChange = false;
			
			if (Input.GetButtonDown ("To Default"))
			{
				if ((m_currentState != (int)State.Default) && changeFlags [0])
					stateChange = true;
				
				m_currentState = (int)State.Default;
			}
			else if ((Input.GetButtonDown ("To Solid")) && changeFlags [1])
			{
				if (m_currentState != (int)State.Solid)
					stateChange = true;
				
				m_currentState = (int)State.Solid;
			}
			else if ((Input.GetButtonDown ("To Liquid")) && changeFlags [2])
			{
				if (m_currentState != (int)State.Liquid)
					stateChange = true;
				
				m_currentState = (int)State.Liquid;
			}
			else if ((Input.GetButtonDown ("To Gas")) && changeFlags [3])
			{
				if (m_currentState != (int)State.Gas)
					stateChange = true;
				
				m_currentState = (int)State.Gas;
			}
			else if ((Input.GetButtonDown ("To Plasma")) && changeFlags [4])
			{
				if (m_currentState != (int)State.Plasma)
					stateChange = true;
				
				m_currentState = (int)State.Plasma;
			}
	
			if (stateChange)
				enableState (m_currentState);
			
			setStatePosition (m_currentState);
			
			if (collidedWithGrates && m_currentState != (int)State.Liquid)
			{
				Debug.Log ("Collision with grates restored");
				GameObject[] grateObjects;
				grateObjects = GameObject.FindGameObjectsWithTag("Grate");
				
				foreach (GameObject grateObject in grateObjects)
				{
					Physics.IgnoreCollision(gameObject.collider, grateObject.collider, false);
				}
			
				collidedWithGrates = false;	
			}
			
			if (ignoredBoulders.Count != 0 && (m_currentState != (int)State.Gas && m_currentState != (int)State.Liquid))
			{
				for (int i = ignoredBoulders.Count - 1 ; i >= 0 ; --i)
				{
					Physics.IgnoreCollision (gameObject.collider, ignoredBoulders[i], false);
					ignoredBoulders.RemoveAt(i);
				}
			}
		}
		else
		{
			bool cameraInPosition = m_camera.isCameraInPosition ();
			if (cameraInPosition)
			{
				playerDead = false;
				
				m_platCtrlScript.SetSpawnPoint (spawnPoint);
//				transform.position = spawnPoint;
				m_platCtrlScript.canControl = true;
				
				enableState (m_currentState);
			}		
		}
	}
	
	
	// given the current state of matter s, set the position
	private void setStatePosition(int s)
	{
		if (m_currentState == (int)State.Default)
			m_defaultMatty.transform.position = new Vector3(transform.position.x,(transform.position.y-0.6f),transform.position.z);	
		else if (m_currentState == (int)State.Solid)		
			m_solidMatty.transform.position = new Vector3(transform.position.x,(transform.position.y-0.6f),transform.position.z);
		else if (m_currentState == (int)State.Liquid)
			m_liquidMatty.transform.position = transform.position;
		else if (m_currentState == (int)State.Gas)
			m_gasMatty.transform.position = transform.position;
		else if (m_currentState == (int)State.Plasma)
			m_plasmaMatty.transform.position = transform.position;
		else
		{
			// BLAHHHH!	SHOULD NEVER REACH THIS CASE!
		}
	}
	
	// given the current state of matter s, set the appropriate game object to active
	// TODO: See if we can find an improvement to this as SetActive is a little slow.
	private void enableState(int s)
	{
		if (m_currentState == (int)State.Default)
		{
			m_defaultMatty.SetActive(true);
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(false);
			Physics.IgnoreCollision(collider, m_defaultMatty.collider);
			
			// Character control parameters
			// TODO: Fine tune the following:
			// In default state, Matty will walk slower compared to solid state
			m_platCtrlScript.canControl = true;
			// Rohith's Note: The below line tripped me up for almost a day.
			// Probably should have notified of such a change via email.
			// I've changed it again so that the Inspector-set values are used
			// instead of some hard-coded value.
			m_platCtrlScript.movement.walkSpeed = origWalkSpeed/2f;
			m_platCtrlScript.jump.enabled = true;
			m_platCtrlScript.jump.extraHeight = origExtraHeight / 4f;
			m_platCtrlScript.movement.gravity = 60;
		}
		else if (m_currentState == (int)State.Solid)
		{
			m_defaultMatty.SetActive(false);
			m_solidMatty.SetActive(true);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(false);
			Physics.IgnoreCollision(collider, m_solidMatty.collider);
			
			// Character control parameters
			// TODO: Fine tune the following:
			// In solid state, Matty will walk faster compared to default state
			m_platCtrlScript.canControl = true;
			// Rohith's Note: Changed below code so that the Inspector-set 
			// values are used instead of some hard-coded value.
			m_platCtrlScript.movement.walkSpeed = origWalkSpeed*1.5f;
			m_platCtrlScript.jump.enabled = true;
			m_platCtrlScript.jump.extraHeight = origExtraHeight;
			m_platCtrlScript.movement.gravity = 60;
		}
		else if (m_currentState == (int)State.Liquid)
		{
			m_defaultMatty.SetActive(false);
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(true);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(false);
			
			// Character control parameters
			// TODO: Fine tune the following:
			// In liquid state, Matty will not be able to jump
			m_platCtrlScript.canControl = true;
			// Rohith's Note: Changed below code so that the Inspector-set 
			// values are used instead of some hard-coded value.
			m_platCtrlScript.movement.walkSpeed = origWalkSpeed;
			m_platCtrlScript.jump.enabled = false;
			m_platCtrlScript.movement.gravity = 60;
		}
		else if (m_currentState == (int)State.Gas)
		{
			m_defaultMatty.SetActive(false);
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(true);
			m_plasmaMatty.SetActive(false);
			
			// Character control parameters
			// TODO: Fine tune the following:
			// In gas state, Matty will walk slower compared to solid state
			m_platCtrlScript.canControl = true;
			// Rohith's Note: Changed below code so that the Inspector-set 
			// values are used instead of some hard-coded value.
			m_platCtrlScript.movement.walkSpeed = origWalkSpeed/5f;
			m_platCtrlScript.jump.enabled = false;
			m_platCtrlScript.movement.gravity = 10;
		}
		else if (m_currentState == (int)State.Plasma)
		{
			m_defaultMatty.SetActive(false);
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(true);
			
			// Character control parameters
			// TODO: Fine tune the following:
			// In plasma state, Matty will walk a bit faster than in solid state, but jump a little lower
			m_platCtrlScript.canControl = true;
			// Rohith's Note: Changed below code so that the Inspector-set 
			// values are used instead of some hard-coded value.
			m_platCtrlScript.movement.walkSpeed = origWalkSpeed*1.6f;
			m_platCtrlScript.jump.enabled = true;
			m_platCtrlScript.jump.extraHeight = origExtraHeight*0.927f;
			m_platCtrlScript.movement.gravity = 60;
		}
		else
		{
			// BLAHHHH!	SHOULD NEVER REACH THIS CASE!
		}	
	}
	
	// Will handle common collisions such as falling down a platform.
	// Note that OnControllerColliderHit must be used instead of OnCollisionEnter because the MainPlayerScript is not attached to a game object
	// with a rigidbody component
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		
		Collider collider = hit.collider;
		//Debug.Log("called OnControllerColliderHit");
		
		// Assuming solid state as default for now (to initialize variable)
		MatterScript stateScript = m_defaultMattyScript;
		
		if(m_currentState == (int)State.Default)
			stateScript = m_defaultMattyScript;
		else if(m_currentState == (int)State.Solid)
			stateScript = m_solidMattyScript;
		else if(m_currentState == (int)State.Liquid)
			stateScript = m_liquidMattyScript;
		else if(m_currentState == (int)State.Gas)
			stateScript = m_gasMattyScript;
		else if(m_currentState == (int)State.Plasma)
			stateScript = m_plasmaMattyScript;
		
		if (collider.CompareTag("Platform"))
		{
			// do nothing
			//Debug.Log("Player on platform");
		}
		else if (collider.CompareTag("DeathPlane"))
		{
			Debug.Log("Fell off");
			Die ();	
		}
		
		// testing
		
		else if (collider.CompareTag("FallingBoulders"))
		{
			Debug.Log("Player got hit by falling boulders");
			
			if(stateScript.FallingBouldersCollisionResolution())
				Die();
			else
			{
				// If current state is plasma and it's hot, destroy boulder.
				if (m_currentState == (int)State.Plasma)
				{
					GameObject explosion = Instantiate(deathExplosion, collider.transform.position, Quaternion.identity) as GameObject;
					Destroy (explosion, 2);
					Destroy (collider.gameObject);
				}
				else if ((m_currentState == (int)State.Gas) || (m_currentState == (int)State.Liquid))
				{
					Physics.IgnoreCollision(gameObject.collider,collider);
					ignoredBoulders.Add (collider);
				}
				
			}
		}
		else if (collider.CompareTag("FlamePillar"))
		{
			Debug.Log("Player got hit by flame pillar");
			if(stateScript.FlamePillarCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("Grate"))
		{
			Debug.Log("Player reached grate");
			
			if(m_currentState == (int)State.Liquid)
				collidedWithGrates = true;
			
			if(stateScript.GrateCollisionResolution(gameObject.collider, collider))
				Die();
		}
		else if (collider.CompareTag("IceCeiling"))
		{
			Debug.Log("Player got hit by ice ceiling");
			if(stateScript.IceCeilingCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("Lava"))
		{
			Debug.Log("Player hit lava");
			if(stateScript.LavaCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("Spike"))
		{
			Debug.Log("Player got hit spike");
			if(stateScript.SpikeCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("SwingingMace"))
		{
			Debug.Log("Player got hit by mace");
			if(stateScript.SwingingMaceCollisionResolution())
				Die();
		}
		else if (collider.CompareTag ("Icicle"))
		{
			Debug.Log("Player hit icicle");
			if (m_currentState == (int)State.Gas) 	// If current state is gas, change icicle collider to trigger
				collider.isTrigger = true;			// we'll set it back to normal once it exits the collider.
			// The above was done to correctly handle condensation when gas hits icicle (it didn't stop before).
			if(stateScript.IceCeilingCollisionResolution())
				Die();
		}
	}
	
	// Handle collision with game objects that are marked as triggers. Thees game objects will allow our player to pass through during collision.
	void OnTriggerEnter (Collider collider)
	{
		MatterScript stateScript = m_defaultMattyScript;
		
		if(m_currentState == (int)State.Default)
			stateScript = m_defaultMattyScript;
		else if(m_currentState == (int)State.Solid)
			stateScript = m_solidMattyScript;
		else if(m_currentState == (int)State.Liquid)
			stateScript = m_liquidMattyScript;
		else if(m_currentState == (int)State.Gas)
			stateScript = m_gasMattyScript;
		else if(m_currentState == (int)State.Plasma)
			stateScript = m_plasmaMattyScript;
		
		if (collider.CompareTag ("DarkCaveEnter"))
		{
			Debug.Log("Player is entering dark cave trigger");
			dir.OnEnterDarkCave (collider);
//			RenderSettings.ambientLight = Color.black;
		}
		else if (collider.CompareTag ("DarkCaveExit"))
		{
			Debug.Log("Player is exiting dark cave trigger");
			dir.OnDarkCaveExit (collider);
//			if (!RenderSettings.ambientLight.Equals (originalAmbientColor))
//				RenderSettings.ambientLight = originalAmbientColor;
		}
		else if (collider.CompareTag ("Lava"))
		{
			Debug.Log("Player hit lava");
			if(stateScript.LavaCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("FlamePillar"))
		{
			Debug.Log("Player got hit by flame pillar");
			if(stateScript.FlamePillarCollisionResolution())
				Die();
		}
		else if (collider.CompareTag ("IcyFloor"))
		{
			Debug.Log ("In IcyFloor");
		}
		else if (collider.CompareTag ("TriggerText"))
			dir.ShowTriggerText (collider.name);
		
		else if (collider.CompareTag ("TriggerEvent"))
		{
			if (dir.EventTrigger (collider.name))
				collider.gameObject.SetActive (false);
		}
	}
	
	void OnTriggerStay (Collider collider)
	{
		MatterScript stateScript = m_defaultMattyScript;
		
		if(m_currentState == (int)State.Default)
			stateScript = m_defaultMattyScript;
		else if(m_currentState == (int)State.Solid)
			stateScript = m_solidMattyScript;
		else if(m_currentState == (int)State.Liquid)
			stateScript = m_liquidMattyScript;
		else if(m_currentState == (int)State.Gas)
			stateScript = m_gasMattyScript;
		else if(m_currentState == (int)State.Plasma)
			stateScript = m_plasmaMattyScript;
		

		if (collider.CompareTag("IcyFloor"))
		{
			Debug.Log("Player hit icy floor");
			if(m_currentState == (int)State.Solid)
			{
				AudioSource.PlayClipAtPoint(slidingSound, gameObject.transform.position);
				m_platCtrlScript.SpeedUp(10.0f);
			}
			else if(m_currentState == (int)State.Gas)
				m_gasMattyScript.Condenstation();
			else if(stateScript.IcyFloorCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("Checkpoint"))
		{
			Debug.Log("Player reached checkpoint!");
//			reachedCheckPoint = true;
		}
		else if (collider.CompareTag("WindTunnel"))
		{
			Debug.Log("Player hit wind tunnel");
			if(stateScript.WindTunnelCollisionResolution(m_platCtrlScript))
				Die();
		}
		else if (collider.CompareTag ("Lava"))
		{
			Debug.Log("Player hit lava");
			if(stateScript.LavaCollisionResolution())
				Die();
		}
	}
	
	void OnTriggerExit (Collider collider)
	{
		MatterScript stateScript = m_defaultMattyScript;
		
		if(m_currentState == (int)State.Default)
			stateScript = m_defaultMattyScript;
		else if(m_currentState == (int)State.Solid)
			stateScript = m_solidMattyScript;
		else if(m_currentState == (int)State.Liquid)
			stateScript = m_liquidMattyScript;
		else if(m_currentState == (int)State.Gas)
			stateScript = m_gasMattyScript;
		else if(m_currentState == (int)State.Plasma)
			stateScript = m_plasmaMattyScript;
		
		if (collider.CompareTag ("Lava") && m_currentState == (int)State.Plasma)
		{
			m_plasmaMattyScript.NotOnLava ();
		}
		else if (collider.CompareTag("Pitfall"))
		{
			Debug.Log("Player fell into pitfall");
			Die (false);
//			// Want to kill player but don't want there to be an explosion, so re-use some Die() code
//			playerDead = true;
//
//			spawnPoint = dir.GetSpawnPoint ();
//			m_platCtrlScript.SetSpawnPoint (spawnPoint);
//		
//			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow>().panTo 
//			(new Vector2 (spawnPoint.x, spawnPoint.y));
//		
//			// Reset speed
//			m_platCtrlScript.ResetCharSpeed();
		}
		else if (collider.CompareTag ("IcyFloor"))
		{
			Debug.Log ("Out of IcyFloor");
		}
		else if (collider.CompareTag ("WindTunnel"))
		{
			stateScript.WindTunnelExit(m_platCtrlScript);
		}
			
		if (m_currentState == (int)State.Gas)
		{
			if (collider.CompareTag ("IcyFloor"))
			{
				Debug.Log("Gas-Matty exits IcyFloor, destroy rain");
				m_gasMattyScript.StopCondensation ();
			}
//			else if (collider.CompareTag("WindTunnel"))
//			{
//				Debug.Log("Gas-Matty exits wind tunnel");
//				m_gasMattyScript.WindTunnelExit (m_platCtrlScript);
//			}
			else if (collider.CompareTag("Icicle"))
			{
				Debug.Log("Gas-Matty exits icicle, stopping rain");
				m_gasMattyScript.StopCondensation ();
				collider.isTrigger = false; // Set collider back to normal.
			}
		}
	}
	
	void Die(bool explode = true) 
	{
		if (explode)
		{
			Vector3 explosionPos = transform.position;
			GameObject explosion = Instantiate(deathExplosion, explosionPos, Quaternion.identity) as GameObject;
			Destroy (explosion, 2);
		}
		
		playerDead = true;

		m_defaultMatty.SetActive(false);
		m_solidMatty.SetActive(false);		
		m_liquidMatty.SetActive(false);
		m_gasMatty.SetActive(false);
		m_plasmaMatty.SetActive(false);
//		if (System.IO.File.Exists ("Save/currentSave") && reachedCheckPoint)
//		{
//			System.IO.StreamReader sr = new System.IO.StreamReader ("Save/currentSave");
//			for (int i = 0; i < 3; i ++)
//				spawnPosition [i] = float.Parse (sr.ReadLine ());
//			sr.Close ();
//		}
//		else
//		{
//			// Temp: used for web browser version as the above case would not work. TODO: Instead of using StreamReader/StreamWriter, use PlayerPrefs
//			transform.position = spawnPoint.transform.position;
//		}
		spawnPoint = dir.GetSpawnPoint ();
		transform.position = spawnPoint;
		m_platCtrlScript.canControl = false;
		
//		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow>().panTo 
			
		m_camera.panTo (new Vector2 (spawnPoint.x, spawnPoint.y));
		
		// Reset speed
		m_platCtrlScript.ResetCharSpeed();

		dir.ResetIcicles ();
	}
	
	public void CanChange (State state, bool ableToChange)
	{
		int i = 0;
		switch (state)
		{
		case State.Solid:
			i = 1;
			break;
		case State.Liquid:
			i = 2;
			break;
		case State.Gas:	
			i = 3;
			break;
		case State.Plasma:	
			i = 4;
			break;
		}
		
		changeFlags [i] = ableToChange;
	}
	
	public bool IsStateEnabled (int stateIndex)
	{	// 0: default, 1: Solid, 2: Liquid, 3: Gas, 4: Plasma.
		return changeFlags [stateIndex];
	}
	
	public int CurrentState ()
	{
		return m_currentState;
	}
}
