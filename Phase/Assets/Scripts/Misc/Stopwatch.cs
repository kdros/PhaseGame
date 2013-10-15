using UnityEngine;
using System;
using System.Collections;

// Stopwatch class that defines the behavior of the stopwatch prefab.
// Mainly used to time a player's completion time
public class Stopwatch : MonoBehaviour 
{
	public Font timerFont;
	
	private GUIStyle timerGUIStyle;
	private bool isActive;
	private float timeInSec;
	private TimeSpan ts;
	
	// Initialization
	void Awake ()
	{
		isActive = false;
		timeInSec = 0.0f;
		
		timerGUIStyle = new GUIStyle();
		timerGUIStyle.wordWrap = true;
		timerGUIStyle.font = timerFont;
		timerGUIStyle.fontSize = 25;
		timerGUIStyle.normal.textColor = Color.white;
		timerGUIStyle.alignment = TextAnchor.MiddleCenter;
	}
	
	// Display time information
	void OnGUI()
	{
		if (isActive)
			GUI.Box (new Rect ((float)Screen.width / 2 - 125.0f, (float)Screen.height / 15, 250.0f, 30.0f), getFormattedTimeString (), timerGUIStyle);
	}
	
	// Accumulate time
	void FixedUpdate () 
	{
		if (isActive)
		{
			timeInSec += Time.deltaTime;
		}
	}
	
	// Stopwatch controls
	public void pauseStopwatch()
	{
		isActive = false;
	}
	
	public void resumeStopwatch()
	{
		isActive = true;
	}
	
	public void startStopwatch()
	{
		restartStopwatch();
	}
	
	public void restartStopwatch()
	{
		timeInSec = 0.0f;
		isActive = true;	
	}
	
	// returns the formatted time in HH:MM:SS
	public string getFormattedTimeString()
	{
		string[] result = new string[4];
		ts = TimeSpan.FromSeconds(timeInSec);
		result[0] = ts.Hours.ToString().PadLeft(2,'0');
		result[1] = ts.Minutes.ToString().PadLeft(2,'0');
		result[2] = ts.Seconds.ToString().PadLeft(2,'0');
		result[3] = ts.Milliseconds.ToString().PadLeft(3,'0');	
		string timeString = result[0] + ":" + result[1] + ":" + result[2];
		return timeString;
	}
	
	// return accumulated time in seconds.
	public double getTime()
	{
		return timeInSec;
	}
	
	// return resulting time in seconds.
	public double getTotalSeconds()
	{
		return ts.TotalSeconds;
	}
}
