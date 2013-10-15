using UnityEngine;
using System.Collections;

// MaceScript is responsible for defining the behavior of the mace prefab.
public class MaceScript : MonoBehaviour {
	
	// Time to stay idle before the mace starts swinging
	public float timer = 0;
	
	// Speed of the swing
	public float swingSpeed = 1.0f;
	
	// Max angle the mace will reach before switcing directions
	public float maxAngle = 45f;
	
	private float curTimer;
	
	// Use this for initialization
	void Start () {
		
		if (swingSpeed == 0f)
			swingSpeed = 1f;
		if (maxAngle == 0f)
			maxAngle = 45f;
		curTimer = 0f;
	}
	
	void FixedUpdate() 
	{
		// Swinging maces will be moving passively; Player has no control
		curTimer += Time.deltaTime;
		
		if(curTimer >= timer)
		{
			if ((gameObject.transform.localEulerAngles.x > maxAngle) && (gameObject.transform.localEulerAngles.x < (360f-maxAngle)))
				swingSpeed = swingSpeed * -1.0f;
			gameObject.transform.Rotate(swingSpeed, 0.0f, 0.0f);
		}
	}
}
