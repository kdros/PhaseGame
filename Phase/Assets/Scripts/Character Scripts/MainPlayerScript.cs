using UnityEngine;
using System.Collections;

/*
 * Central Player Script.
 * Responsible for managing the transition between different states of matter
 * 
 * <TODO: Determine additional tasks to do in this class>
 * <TODO: Should this be attached to an empty game object that has a rigid body component?
 *        That is, all the states of matter prefabs will only be used for collision detection. The locomotion is handled through this class.>
 */

public class MainPlayerScript : MonoBehaviour {
	
	// Explosion prefab used for death animation
	public GameObject deathExplosion;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Die() {
		
	}
}
