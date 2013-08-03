using UnityEngine;
using System;
using System.Collections;


// Base class for all the text related scripts to extend
// This will take care of changing the color of the text to the appropriate color according to mouse interaction
// classes are responsible for implementing the respondToClick function.
public class TextScript : MonoBehaviour 
{
	public string textName;
	public int columnNumber; // number from 1 to 3
	
	[System.NonSerialized]
	public AudioClip hoverSound;
	[System.NonSerialized]
	public AudioClip clickSound;
	
	// Used by GameMenu Script
	private float travelDistance; // distance traveled in the +x or -x direction in order for the text to be shown or hidden
	private float moveSpeed;      // speed at which the text will travel with
	private float currentDistance;
		
	void Start ()
	{
		currentDistance = 0.0f;
		if (textName == null)
		{
			Debug.LogWarning("Text Script should have a name associated with it");
			textName = "noName";
		}
		
		if (columnNumber > 3)
			columnNumber = 3;
		else if (columnNumber <= 0)
			columnNumber = 1;
	}
	
	
	void OnMouseEnter()
	{
		renderer.material.color = Color.red;
		AudioSource.PlayClipAtPoint(hoverSound, transform.position);
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;	
	}
			
	void OnMouseDown ()
	{
		AudioSource.PlayClipAtPoint(clickSound, transform.position);
		respondToClick ();
	}
	
	public virtual void respondToClick()
	{
		// no op
	}
	
	// translate in "direction" direction by moveSpeed for up to travelDistance
	public bool translateText (Vector3 direction)
	{
		Vector3 deltaPos = direction * moveSpeed * Time.deltaTime;
		currentDistance += deltaPos.magnitude;
		
		transform.position = transform.position + deltaPos;
		
		if (currentDistance >= travelDistance)
		{
			// get rid of overshooting
			float offset = currentDistance - travelDistance;
			float newX = (float)Math.Round (Math.Abs (transform.position.x) - offset, 3);
			Vector3 newP = new Vector3(newX * Math.Sign(transform.position.x), transform.position.y, transform.position.z);
			transform.position = newP;			
			currentDistance = 0.0f;
			return true;
		}
		else 
		{
			return false;	
		}
	}
		
	public void setDistance(float dist)
	{
		travelDistance = dist;
	}
	
	public void setSpeed(float speed)
	{
		moveSpeed = speed;
	}
	
}
