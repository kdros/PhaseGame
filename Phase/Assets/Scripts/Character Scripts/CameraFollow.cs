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
		
	void Start()
	{
		if (distance == 0.0f)
			distance = 10.0f;
	}
	
	
	void LateUpdate () 
	{
		if (!target)
			return;
		
		Vector3 camDist = new Vector3(0,-height,distance);
		transform.position = target.position - camDist;
		transform.LookAt (target);
	}
}
