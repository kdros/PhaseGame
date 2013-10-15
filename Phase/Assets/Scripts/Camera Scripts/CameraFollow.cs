using UnityEngine;
using System.Collections;
/*
 * Use this script to make the camera follow Matty.
 * Assume that the camera will always be looking down the positive z-axis
 * 
 * Written by Rohith Chandran, Mikey Chen and Kristen Drosinos.
 */


public class CameraFollow : MonoBehaviour {
	public Transform target;	// the target that we want to follow
	public float distance; 		// the distance in the x-y plane to the target
	public float height;		// the height we want the camera to be above the target
	public float panTime;		// Camera would take these many seconds to pan to the target position.
	public float userPanspeed = 3f; // Used to control the speed of zoom/height changes by the user.
	
	public bool oldStyle = true;	// Use this to switch between Old-Style ("Sonic") camera and the 
									// New-Style camera where the camera does not translate with the player when he jumps.
	float speed;				
	float curTime;				// Counter that keeps track of time elapsed since some event.
	float waitAfterPanning;		// Used to specify a wait time - Camera would wait these many seconds after panning to one target before panning to the next.
	bool isPanning;				// Used to keep track if camera is panning or not.
	bool stopPanning;			// Used to keep track if the camera has stopped after panning.
	bool isRepositioning = false;	// Used to keep track if the camera is repositioning itself in new-style camera panning.
	Vector3 newPosition;		// The new position the camera should pan to.
	Vector3 normalizedError;	// The normalized difference vector between current position and target position.
	
	Vector3 camDist;			// The camera's position is offset by this vector from the lookAtTarget's position.
								// camDist is calculated as (0, -height, distance).
	Vector3 lookAtTarget;		// Position of the target object the camera should look at after panning.
	
	MainPlayerScript player;

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
		lookAtTarget = target.position;
		transform.position = target.position - camDist;
		
		oldStyle = true;
		
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<MainPlayerScript>();
	}
	
	
	void LateUpdate () 
	{
		if (!target)
			return;
				
		if (isPanning)
		{	
			// Panning is in progress.
			Vector3 error = newPosition - camDist - transform.position;
			if ((error.sqrMagnitude < Mathf.Pow (Time.deltaTime*speed, 2f))||(Vector3.Dot (error, normalizedError) < 0))
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
			
			Vector3 wouldBePosition = target.position - camDist;
			
			// New style camera positioning.
			// Camera will follow player in the +y-direction only if he goes outside a fixed area on the screen. 
			if (!oldStyle)
			{
				float yDiff = (wouldBePosition.y - transform.position.y);
				if ((yDiff > 0f) && (yDiff < 4.5f))
				{
					if (!player.IsGrounded ())
						wouldBePosition.y = transform.position.y;
				}			
				rePosition (wouldBePosition, 0.25f);
			}
			// Old-style positioning.
			// Camera will follow the player always, so that he's at the centre of the screen.
			else
				transform.position = wouldBePosition;

			lookAtTarget = transform.position + camDist;
		}
		
		transform.LookAt (lookAtTarget);
	}
	
	// This function "repositions" the camera in the new-style camera panning.
	void rePosition (Vector3 newCamPosition, float panningTime)
	{
		newPosition = newCamPosition;

		// The below hack is to combine two vector length operations into one.
		normalizedError = (newPosition) - transform.position;
		speed = normalizedError.magnitude;
		if (speed > 0)
			normalizedError = normalizedError / speed;
		speed /= panningTime;
		
		transform.position += (normalizedError*Time.deltaTime*speed);
		
		// Camera will always follow the player in the x-direction.
		Vector3 resetX = transform.position;
		resetX.x = newCamPosition.x;
		transform.position = resetX;
	}
	
	// Pans to (x, y, 0)
	public void panTo (float x, float y)
	{
		panTo (new Vector3 (x, y, 0), 0f, -1f);
	}
	
	// Given a target position (newLookAtPosition), this function translates the camera to that position, 
	// offset by the camDist vector. A waiting time (waitTime) and speed of panning (panningTime) can be 
	// optionally specified.
	public void panTo (Vector3 newLookAtPosition, float waitTime = 0f, float panningTime = -1f)
	{
		if (panningTime == -1f)
			panningTime = panTime;
		
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
	
	// Pans the camera to a specified absolute position (absoluteCamPos). Camera's new position 
	// will NOT be offset by camDist, if this function is used. 
	public void PanToAbsolute (Vector3 absoluteCamPos, float waitTime = 0f, float panningTime = -1f)
	{
		panTo (absoluteCamPos + camDist, waitTime, panningTime);
	}
	
	public bool isCameraInPosition ()
	{	return !isPanning;	}
	
	public bool IsStopped ()
	{	return stopPanning;	}
	
}
