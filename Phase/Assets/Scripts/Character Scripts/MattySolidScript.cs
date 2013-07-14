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
	
	void OnCollisionEnter(Collision collision)
	{
		Collider collider = collision.collider;
		
		if(collider.CompareTag ("Pitfall"))
		{
			
			//Kill
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		
		if (hit.collider.gameObject.tag == "Pitfall")
		{
			Debug.Log ("Crashed Into Pitfall");
			AudioSource.PlayClipAtPoint(deathFallingIntoPit, gameObject.transform.position);
			Die();
		}
		if (hit.collider.gameObject.tag == "SwingingMace")
		{
			Debug.Log ("Hit by swinging mace.");
			Die();
		}
        	
    }
	
	public void Die ()
	{
    	Destroy(gameObject);
	}
}
