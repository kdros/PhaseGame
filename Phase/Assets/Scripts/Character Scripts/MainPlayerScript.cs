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
	public Transform spawnPoint;		// Spawn location
	
	public GameObject phaseEffect; 		// Particle effect for state transition
	public GameObject deathExplosion; 	// Explosion prefab used for death animation
	public GameObject plasmaMatty;
	public GameObject gasMatty;			
	public GameObject liquidMatty;
	public GameObject solidMatty;
	public GameObject defaultMatty;
	
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
	
	/// <summary>
	/// Internal variables
	/// </summary>
	
	protected MattyPlasmaScript m_plasmaMattyScript;	// class associated with Plasma Matty (place holder)
	protected MattySolidScript m_solidMattyScript;		// class associated with Solid Matty
	protected MattyLiquidScript m_liquidMattyScript;	// class associated with Liquid Matty
	protected MattyGasScript m_gasMattyScript;			// class associated with Gas Matty
	protected MattyScript m_defaultMattyScript;			// class associated with default Matty
	
	private int m_currentState;							// keep track of the current state	
	private bool collidedWithGrates;					// set to true if liquid state collided with grates
	enum State {Default, Solid, Liquid, Gas, Plasma};
	
	Color originalAmbientColor;
	CameraFollow camera;
	Vector3 spawnPosition;
	bool playerDead;
	
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
		
		// Temp: Make sure this collider does not collide with each state of matter's colliders
		Physics.IgnoreCollision(collider, m_defaultMatty.collider);
		Physics.IgnoreCollision(collider, m_solidMatty.collider);
		Physics.IgnoreCollision(collider, m_plasmaMatty.collider);
		
		// Temp: Start out with defaultMatty
		//m_defaultMatty.SetActive(false);
		m_solidMatty.SetActive(false);		
		m_liquidMatty.SetActive(false);
		m_gasMatty.SetActive(false);
		m_plasmaMatty.SetActive(false);	
		
		originalAmbientColor = RenderSettings.ambientLight;
		playerDead = false;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow>();
		collidedWithGrates = false;
		spawnPosition = spawnPoint.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!playerDead)
		{
			bool stateChange = false;
			
			if (Input.GetButtonDown ("To Default"))
			{
				if (m_currentState != (int)State.Default)
					stateChange = true;
				
				m_currentState = (int)State.Default;
			}
			else if (Input.GetButtonDown ("To Solid"))
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
		}
		else
		{
			bool cameraInPosition = camera.isCameraInPosition ();
			if (cameraInPosition)
			{
//				if (System.IO.File.Exists ("Save/currentSave"))
//					transform.position = spawnPosition;
//				else
					transform.position = spawnPosition;//spawnPoint.position;
				playerDead = false;
			}		
		}
// TODO: Resolve conflict.
//		if (stateChange)
//			enableState (m_currentState);
//		
//		setStatePosition (m_currentState);	
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
		}
		else if (m_currentState == (int)State.Solid)
		{
			m_defaultMatty.SetActive(false);
			m_solidMatty.SetActive(true);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(false);
			Physics.IgnoreCollision(collider, m_solidMatty.collider);
		}
		else if (m_currentState == (int)State.Liquid)
		{
			m_defaultMatty.SetActive(false);
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(true);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(false);	
		}
		else if (m_currentState == (int)State.Gas)
		{
			m_defaultMatty.SetActive(false);
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(true);
			m_plasmaMatty.SetActive(false);	
		}
		else if (m_currentState == (int)State.Plasma)
		{
			m_defaultMatty.SetActive(false);
			m_solidMatty.SetActive(false);		
			m_liquidMatty.SetActive(false);
			m_gasMatty.SetActive(false);
			m_plasmaMatty.SetActive(true);
			//Physics.IgnoreCollision(collider, m_plasmaMatty.collider);
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
			Debug.Log("Player on platform");
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
			RenderSettings.ambientLight = Color.black;
		}
		else if (collider.CompareTag ("DarkCaveExit"))
		{
			Debug.Log("Player is exiting dark cave trigger");
			if (!RenderSettings.ambientLight.Equals (originalAmbientColor))
				RenderSettings.ambientLight = originalAmbientColor;
		}
		else if (collider.CompareTag ("Lava"))
		{
			Debug.Log("Player hit lava");
			if(stateScript.LavaCollisionResolution())
				Die();
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
				gameObject.SendMessage("SpeedUp", 10.0f);
			else if(m_currentState == (int)State.Gas)
				m_gasMattyScript.Condenstation();
			else if(stateScript.IcyFloorCollisionResolution())
				Die();
		}
		else if (collider.CompareTag("Checkpoint"))
		{
			Debug.Log("Player reached checkpoint!!!!!!");
			
//			GameObject obj = GameObject.Find("GlobalObject_BegLev1");
//			Global_BegLev1 g = obj.GetComponent<Global_BegLev1>();
//			// Check to see if collider 
//			if (collider.gameObject.name == "Checkpoint_01")
//			{
//				if (g.checkpoint_01_hit == false)
//				{
//					g.checkpoint_01_hit = true;
//					g.hitCheckpoints = g.hitCheckpoints + 1;
//				}
//			}
//			if (collider.gameObject.name == "Checkpoint_02")
//			{
//				if (g.checkpoint_02_hit == false)
//				{
//					g.checkpoint_02_hit = true;
//					g.hitCheckpoints = g.hitCheckpoints + 1;
//				}
//			}
//			if (collider.gameObject.name == "Checkpoint_03")
//			{
//				if (g.checkpoint_03_hit == false)
//				{
//					g.checkpoint_03_hit = true;
//					g.hitCheckpoints = g.hitCheckpoints + 1;
//				}
//			}
		}
			
	}
	
	void OnTriggerExit (Collider collider)
	{
		if (collider.CompareTag ("Lava") && m_currentState == (int)State.Plasma)
		{
			m_plasmaMattyScript.NotOnLava ();
		}
		
		if (collider.CompareTag ("IcyFloor") && m_currentState == (int)State.Gas)
		{
			Debug.Log("destroy rain");
			m_gasMattyScript.StopCondensation ();
		}
		
	}
	
	void Die() 
	{
		GameObject explosion = Instantiate(deathExplosion, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy (explosion, 2);
		
		playerDead = true;
		if (System.IO.File.Exists ("Save/currentSave"))
		{
			System.IO.StreamReader sr = new System.IO.StreamReader ("Save/currentSave");
			for (int i = 0; i < 3; i ++)
				spawnPosition [i] = float.Parse (sr.ReadLine ());
			sr.Close ();
		}
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow>().panTo 
			(new Vector2 (spawnPosition.x, spawnPosition.y));
		// Temp: Just put player back at the spawn point
//		if (cameraInPosition)
//		{
//			transform.position = spawnPoint.position;
////			CameraInPosition = false;
//		}
		// TODO: Respawn
//		GameObject obj = GameObject.Find("GlobalObject_BegLev1");
//		Global_BegLev1 g = obj.GetComponent<Global_BegLev1>();
//		
//		Vector3 restorePos = new Vector3(0.0f, 0.0f, 0.0f);
//		if (g.hitCheckpoints == 0)
//			restorePos = g.checkpoint_01.transform.position;
//		if (g.hitCheckpoints == 1)
//			restorePos = g.checkpoint_01.transform.position;
//		else if (g.hitCheckpoints == 2)
//			restorePos = g.checkpoint_02.transform.position;
//		else if(g.hitCheckpoints == 3)
//			restorePos = g.checkpoint_03.transform.position;
//		transform.position = restorePos;
		
		// Temp: Just put player back at the spawn point
		//transform.position = spawnPoint.position;
		
		// Temporarily disabled to make testing easier
		//Destroy (m_defaultMatty);
		//Destroy (m_solidMatty);
		//Destroy (m_liquidMatty);
		//Destroy (m_gasMatty);
		//Destroy (m_plasmaMatty);
		//Destroy (gameObject);
		
	}
	
//	public void CameraInPosition ()
//	{
//		cameraInPosition = true;
//	}
}
