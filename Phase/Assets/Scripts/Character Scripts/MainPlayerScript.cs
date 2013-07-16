using UnityEngine;
using System.Collections;

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

	public GameObject phaseEffect; 		// Particle effect for state transition
	public GameObject deathExplosion; 	// Explosion prefab used for death animation
	public GameObject plasmaMatty;
	public GameObject gasMatty;			
	public GameObject liquidMatty;
	public GameObject solidMatty;
	//public GameObject defaultMatty;
	
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
	
	/// <summary>
	/// Internal variables
	/// </summary>

	protected MattySolidScript m_solidMattyScript;		// class associated with Solid Matty
	protected MattyLiquidScript m_liquidMattyScript;	// class associated with Liquid Matty
	protected MattyGasScript m_gasMattyScript;			// class associated with Gas Matty
	protected MattyPlasmaScript m_plasmaMattyScript;	// class associated with Plasma Matty (place holder)
	
	private int m_currentState;							// keep track of the current state
	
	enum State {Default, Solid, Liquid, Gas, Plasma};
	
	// Use this for initialization
	void Start () 
	{
		m_currentState = (int)State.Solid;
		
		// instantiate each state of matter
		m_solidMatty = Instantiate (solidMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
		m_liquidMatty = Instantiate (liquidMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
		m_gasMatty = Instantiate (gasMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
		m_plasmaMatty = Instantiate (plasmaMatty, gameObject.transform.position, Quaternion.identity) as GameObject;		
		
		// find the class associated with each state of matter
		m_solidMattyScript = m_solidMatty.GetComponent<MattySolidScript>();
		
		// Temp: Make sure this collider does not collide with each state of matter's colliders
		Physics.IgnoreCollision(collider, m_solidMatty.collider);
		Physics.IgnoreCollision(collider, m_plasmaMatty.collider);
		
		// Temp: Start out with soildMatty
		//m_solidMatty.SetActive(false);		
		m_liquidMatty.SetActive(false);
		m_gasMatty.SetActive(false);
		m_plasmaMatty.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool stateChange = false;
		
		if (Input.GetButtonDown ("To Solid"))
		{
			if (m_currentState != (int)State.Solid)
				stateChange = true;
			
			m_currentState = (int)State.Solid;
		}
		else if(Input.GetButtonDown ("To Liquid"))
		{
			if (m_currentState != (int)State.Liquid)
				stateChange = true;
			
			m_currentState = (int)State.Liquid;
		}
		else if(Input.GetButtonDown ("To Gas"))
		{
			if (m_currentState != (int)State.Gas)
				stateChange = true;
			
			m_currentState = (int)State.Gas;
		}
		else if(Input.GetButtonDown ("To Plasma"))
		{
			if (m_currentState != (int)State.Plasma)
				stateChange = true;
			
			m_currentState = (int)State.Plasma;
		}

		if (stateChange)
			enableState (m_currentState);
		
		setStatePosition (m_currentState);
	}
	
	
	// given the current state of matter s, set the position
	private void setStatePosition(int s)
	{
		if (m_currentState == (int)State.Default)
		{
			// TODO: Fill this in	
		}
		else if (m_currentState == (int)State.Solid)		
			m_solidMatty.transform.position = transform.position;
		else if (m_currentState == (int)State.Liquid)
			m_liquidMatty.transform.position = transform.position;
		else if (m_currentState == (int)State.Gas)
			m_gasMatty.transform.position = transform.position;
		else if (m_currentState == (int)State.Plasma)
			m_plasmaMatty.rigidbody.MovePosition(transform.position);
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
			// TODO: Fill this in	
		}
		else if (m_currentState == (int)State.Solid)
		{
			m_solidMatty.SetActive(true);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(false);
			Physics.IgnoreCollision(collider, m_solidMatty.collider);
		}
		else if (m_currentState == (int)State.Liquid)
		{
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(true);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(false);	
		}
		else if (m_currentState == (int)State.Gas)
		{
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(true);
			m_plasmaMatty.SetActive(false);	
		}
		else if (m_currentState == (int)State.Plasma)
		{
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(true);
			Physics.IgnoreCollision(collider, m_plasmaMatty.collider);
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
		Debug.Log("called OnControllerColliderHit");
		
		// Assuming solid state as default for now (to initialize variable)
		MatterScript stateScript = m_solidMattyScript;

		if(m_currentState == (int)State.Solid)
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
			Debug.Log("Player on platform");
		}
		else if (collider.CompareTag("DeathPlane"))
		{
			Debug.Log("Fell off");
			Die ();	
		}
		
		// testing
		else if (collider.CompareTag("Checkpoint"))
		{
			Debug.Log("Player reached checkpoint");
			//TODO - Not sure what should be returned since no death
		}
		else if (collider.CompareTag("FallingBoulders"))
		{
			Debug.Log("Player got hit by falling boulders");
			if(stateScript.FallingBouldersCollisionResolution())
				Die();
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
			if(stateScript.GrateCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("IceCeiling"))
		{
			Debug.Log("Player got hit by ice ceiling");
			if(stateScript.IceCeilingCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("IcyFloor"))
		{
			Debug.Log("Player hiit icy floor");
			if(stateScript.IcyFloorCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("Lava"))
		{
			Debug.Log("Player hit lava");
			if(stateScript.LavaCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("Pitfall"))
		{
			Debug.Log("Player fell into pitfall");
			if(stateScript.PitfallCollisionResolution())
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
		else if (collider.CompareTag("WindTunnel"))
		{
			Debug.Log("Player hit wind tunnel");
			if(stateScript.WindTunnelCollisionResolution())
				Die();
		}
		else if (collider.CompareTag ("Icicle"))
		{
			Debug.Log("Player hit icicle");
			if(stateScript.IceCeilingCollisionResolution())
				Die();
		}
	}
	
	void Die() 
	{
		GameObject explosion = Instantiate(deathExplosion, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy (explosion, 2);
		
		Destroy (m_solidMatty);
		Destroy (m_liquidMatty);
		Destroy (m_gasMatty);
		Destroy (m_plasmaMatty);
		Destroy (gameObject);
		
		// TODO: Respawn
	}
}
