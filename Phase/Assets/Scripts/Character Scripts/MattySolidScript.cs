using UnityEngine;
using System.Collections;

public class MattySolidScript : MonoBehaviour {
	
	public AudioClip deathFallingIntoPit;
	
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
		//WindTunnel Collision has no effect on solid state
        	
    }
		
	public void Die ()
	{
    	Destroy(gameObject);
	}
}
