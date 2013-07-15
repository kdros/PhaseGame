using UnityEngine;
using System.Collections;

public class MattySolidScript : MatterScript {
	
	public AudioClip deathFallingIntoPit;
	public float speed;
	public Vector3 direction;
	
	void Start()
	{
		direction.x = -10;
		speed = 100.0f;
	}
	
    void Update() {
		
		// Lock any movement in the z direction
        Vector3 mattySolidPos = transform.position;
     	mattySolidPos.z = 0;
     	transform.position = mattySolidPos;
    }
		
	public void Die ()
	{
		GameObject obj = GameObject.Find("GlobalObject_BegLev1");
		Global_BegLev1 g = obj.GetComponent<Global_BegLev1>();
		g.mattySolidDeath = true;
		
		
    	Destroy(gameObject);
	}
}
