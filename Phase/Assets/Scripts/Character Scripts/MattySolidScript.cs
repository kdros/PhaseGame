using UnityEngine;
using System.Collections;

public class MattySolidScript : MonoBehaviour {
	
    void Update() {
		
		// Lock any movement in the z direction
        Vector3 mattySolidPos = transform.position;
     	mattySolidPos.z = 0;
     	transform.position = mattySolidPos;
    }
	
	void OnCollisionEnter(Collision collision)
	{
		Collider collider = collision.collider;
		
		if(collider.CompareTag ("IcyFloor"))
		{
			Debug.Log ("Solid is sliding on icy floor");
		}
	}
}
