using UnityEngine;
using System.Collections;

public class MattySolidScript : MonoBehaviour {
	
	public AudioClip deathFallingIntoPit;
	public float speed;
	public Vector3 direction;
	//CharacterController controller;
	
	void Start()
	{
		//controller = GetComponent<CharacterController>();
		direction.x = -10;
		speed = 100.0f;
	}
	
    void Update() {
		
		// Lock any movement in the z direction
        Vector3 mattySolidPos = transform.position;
     	mattySolidPos.z = 0;
     	transform.position = mattySolidPos;
    }
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		
		if (hit.collider.gameObject.tag == "Pitfall")
		{
			Debug.Log ("Crashed Into Pitfall - DEATH");
			AudioSource.PlayClipAtPoint(deathFallingIntoPit, gameObject.transform.position);
			Die();
		}
		if (hit.collider.gameObject.tag == "SwingingMace")
		{
			Debug.Log ("Hit by swinging mace. - DEATH");
			Die();
		}
		if (hit.collider.gameObject.tag == "Spike")
		{
			Debug.Log ("Landed on spike. - DEATH");
			Die();
		}
		if (hit.collider.gameObject.tag == "Grate")
		{
			Debug.Log ("On Grate. - NO EFFECT");
		}
		if (hit.collider.gameObject.tag == "IcyFloor")
		{
			Debug.Log ("Sliding on Ice. - Increased Velocity");
		}
		if (hit.collider.gameObject.tag == "WindTunnel")
		{
			Debug.Log ("Hit Wind Tunnel - No Effect");
		}
		if (hit.collider.gameObject.tag == "FlamePillar")
		{
			Debug.Log ("Hit Flame Pillar - DEATH");
			Die();
		}
		if (hit.collider.gameObject.tag == "Lava")
		{
			Debug.Log ("Hit Lava - DEATH");
			Die();
		}
        	
    }
		
	public void Die ()
	{
    	Destroy(gameObject);
	}
}
