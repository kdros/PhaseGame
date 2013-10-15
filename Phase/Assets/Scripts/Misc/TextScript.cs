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
	public bool isTransitionText;
	
	[System.NonSerialized]
	public AudioClip hoverSound;
	[System.NonSerialized]
	public AudioClip clickSound;
	[System.NonSerialized]
	public Vector3 originalPosition;
	
	private float travelDistance; // distance traveled in the +x or -x direction in order for the text to be shown or hidden
	private float moveSpeed;      // speed at which the text will travel with
	private bool isSelected;	  // used for speedRun and normal Texts (make sure the text stays green)
	private float currentDistance;
	
	// Initialization
	void Awake ()
	{
		originalPosition = transform.position;
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
		
		isSelected = false;
	}
	
	// Change the color of the text to red when the user mouse over the text
	void OnMouseEnter()
	{
		renderer.material.color = Color.red;
		AudioSource.PlayClipAtPoint(hoverSound, Camera.main.transform.position);
	}
	
	// When the mouse is exiting the text, change the color of the text back to white if the 
	// text is not selected. Otherwise, leave the color as green.
	void OnMouseExit()
	{
		if (!isSelected)
			renderer.material.color = Color.white;	
		else 
			renderer.material.color = Color.green;
	}
	
	// When the user clicks on the text, the text should respond somehow.		
	void OnMouseDown ()
	{
		AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
		respondToClick ();
	}
	
	// This function will need to be overridden by the derived classes to provide specific
	// response to click
	public virtual void respondToClick()
	{
		// no op
	}
	
	// translate in "direction" direction by moveSpeed for up to travelDistance
	public bool translateText (Vector3 direction)
	{
		Vector3 deltaPos = direction * moveSpeed * Time.deltaTime;
		currentDistance += deltaPos.magnitude;
			
		if (currentDistance >= travelDistance)
		{
			transform.position = originalPosition + direction * travelDistance;
			originalPosition = transform.position;
			currentDistance = 0.0f;
			return true;
		}
		else
		{
			transform.position = transform.position + deltaPos;
			return false;	
		}
	}
	
	// Setters	
	public void setOriginalPosition(Vector3 originalPos)
	{
		originalPosition = originalPos;
	}
		
	public void setDistance(float dist)
	{
		travelDistance = dist;
	}
	
	public void setSpeed(float speed)
	{
		moveSpeed = speed;
	}
	
	// Text selection response
	public void selectText ()
	{
		isSelected = true;
		renderer.material.color = Color.green;	
	}
	
	public void unselectText()
	{
		isSelected = false;
		renderer.material.color = Color.white;
	}
}
