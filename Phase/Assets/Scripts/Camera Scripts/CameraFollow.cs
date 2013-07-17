using UnityEngine;
using System.Collections;
/*
 * Use this script to make the camera follow Matty.
 * Assume that the camera will always be looking down the positive z-axis
 * 
 * M: TODO - Make it so that when the player jumps, the camera will not translate in the y direction with the player.
 */


public class CameraFollow : MonoBehaviour {
	public Transform target;	// the target that we want to follow
	public float distance; 		// the distance in the x-y plane to the target
	public float height;		// the height we want the camera to be above the target
	public float panTime;
	
	float speed;
	bool isPanning;
	Vector2 newPosition;
	Vector3 normalizedError;
	void Start()
	{
		if (distance == 0.0f)
			distance = 10.0f;
		
		if (panTime == 0f)
			panTime = 3f;
		speed = 0f;
		isPanning = false;
		newPosition = Vector2.zero;
		normalizedError = Vector3.one;
	}
	
	
	void LateUpdate () 
	{
		if (!target)
			return;
		
		Vector3 camDist = new Vector3(0,-height,distance);
		Vector3 lookAtTarget = target.position;
//		transform.position = target.position - camDist;
		
//		Vector3 camDist = new Vector3(0,-height,distance);
//		newPosition = newLookAtPosition - camDist;
		
		if (isPanning)
		{	
			Vector3 error = new Vector3 (newPosition.x, newPosition.y, 0) - camDist - transform.position;
			if (error.sqrMagnitude < 0.25f)
			{
				transform.position = new Vector3 (newPosition.x, newPosition.y, 0) - camDist;
				newPosition = Vector2.zero;
				isPanning = false;
			}
			else
				transform.position += (normalizedError*Time.deltaTime*speed);
			
			lookAtTarget = transform.position + camDist;
//			Debug.Log ("Panning. Current Pos: " + transform.position.ToString());
		}
		else
			transform.position = target.position - camDist;

//		transform.LookAt (target);
		transform.LookAt (lookAtTarget);
	}
	
	public void panTo (Vector2 newLookAtPosition)
	{
		Vector3 camDist = new Vector3(0,-height,distance);
		
		isPanning = true;
		newPosition = newLookAtPosition;
		normalizedError = (new Vector3 (newPosition.x, newPosition.y, 0) - camDist) - transform.position;
		
		// The below hack is to combine two vector length operations into one.
		speed = normalizedError.magnitude;
		normalizedError = normalizedError / speed;
		speed /= panTime;
//		Vector3 camDist = new Vector3(0,-height,distance);
//		newPosition = newLookAtPosition - camDist;
//		
//		Vector3 error = newPosition - transform.position;
//		while (error.sqrMagnitude >= 1.0f)
//		{			
////			transform.LookAt (target);
//			
//			error = newPosition - transform.position;
//		}
//		
//		transform.position = newPosition;
//		transform.LookAt (target);
	}
	
	public bool isCameraInPosition ()
	{	return isPanning;	}
}
