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
	public float userPanspeed = 3f;
	
	float speed;
	float curTime;
	float waitAfterPanning;
	bool isPanning;
	bool stopPanning;
	Vector3 newPosition;
	Vector3 normalizedError;
	
	Vector3 camDist;
	void Start()
	{
		if (distance == 0.0f)
			distance = 10.0f;
		
		if (panTime == 0f)
			panTime = 3f;
		speed = 0f;
		curTime = 0f;
		waitAfterPanning = 0f;
		isPanning = false;
		newPosition = Vector2.zero;
		normalizedError = Vector3.one;
		
		camDist = new Vector3(0,-height,distance);
	}
	
	
	void LateUpdate () 
	{
		if (!target)
			return;
		
		Vector3 lookAtTarget = target.position;
		
		if (isPanning)
		{	
			// Panning is in progress.
			Vector3 error = newPosition - camDist - transform.position;
			if ((error.sqrMagnitude < 0.01f)||(Vector3.Dot (error, normalizedError) < 0))
			{
				if (!stopPanning)
				{
					// Camera has reached close enough to the target position.
					// Set Camera position to target position.
					transform.position = newPosition - camDist;
					// Stop moving the camera.
					stopPanning = true;
				}
				else
				{
					// Camera has reached target position and must wait waitAfterPanning seconds
					// before setting isPanning to false, indicating that panning is complete.
					curTime += Time.deltaTime;
					if (curTime > waitAfterPanning)
					{
						curTime = 0f;
						stopPanning = false;
						newPosition = Vector3.zero;
						isPanning = false;
					}
				}
			}
				
			else
				// Keep moving the camera to target position.
				transform.position += (normalizedError*Time.deltaTime*speed);
			
			lookAtTarget = transform.position + camDist;
		}
		else
		{
			// Camera is not panning. Respond to user controls to increase/decrease height/distance.
			
			float h = Input.GetAxis ("CameraHeight");
			camDist.y -= (h*Time.deltaTime*userPanspeed);
			if (h < 0)
				camDist.y = Mathf.Min (camDist.y, -height);
			else if (h > 0)
				camDist.y = Mathf.Max (camDist.y, -(height+3f));
			
			float z = Input.GetAxis ("CameraZoom");
			camDist.z += (z*Time.deltaTime*userPanspeed);
			if (z < 0)
				camDist.z = Mathf.Max (camDist.z, distance);
			else if (z > 0)
				camDist.z = Mathf.Min (camDist.z, (distance+10f));
			
			transform.position = target.position - camDist;
		}
		
		transform.LookAt (lookAtTarget);
	}
	
	public void panTo (float x, float y)
	{
		panTo (new Vector3 (x, y, 0), 0f, -1f);
	}
	
	public void panTo (Vector3 newLookAtPosition, float waitTime = 0f, float panningTime = -1f)
	{
		if (panningTime == -1f)
			panningTime = panTime;
		camDist = new Vector3(0,-height,distance);
		
		isPanning = true;	 // Camera is panning.
		stopPanning = false; // Do not stop panning.
		
		newPosition = newLookAtPosition;
		normalizedError = (newPosition - camDist) - transform.position;
		
		// The below hack is to combine two vector length operations into one.
		speed = normalizedError.magnitude;
		normalizedError = normalizedError / speed;
		speed /= panningTime;
		
		// Camera would wait this many seconds after panning before indicating that panning is complete.
		waitAfterPanning = waitTime;
	}
	
	public void PanToAbsolute (Vector3 absoluteCamPos, float waitTime = 0f, float panningTime = -1f)
	{
		camDist = new Vector3(0,-height,distance);
		panTo (absoluteCamPos + camDist, waitTime, panningTime);
	}
	
	public bool isCameraInPosition ()
	{	return !isPanning;	}
	
	public bool IsStopped ()
	{	return stopPanning;	}
	
}
