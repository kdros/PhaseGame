using UnityEngine;
using System;
using System.Collections;

// Used to time a player's completion time
public class Stopwatch : MonoBehaviour 
{
	public Font timerFont;
	
	private GUIStyle timerGUIStyle;
	private bool active;
	private float timeInSec;
	private TimeSpan ts;
	
	void Start ()
	{
		active = true;
		timeInSec = 0.0f;
		
		timerGUIStyle = new GUIStyle();
		timerGUIStyle.wordWrap = true;
		timerGUIStyle.font = timerFont;
		timerGUIStyle.fontSize = 25;
		timerGUIStyle.normal.textColor = Color.white;
		timerGUIStyle.alignment = TextAnchor.MiddleCenter;
	}
	
	void OnGUI()
	{
		GUI.Box (new Rect ((float)Screen.width / 2 - 125.0f, (float)Screen.height / 15, 250.0f, 30.0f), getFormattedTimeString (), timerGUIStyle);
	}
	
	void FixedUpdate () 
	{
		getTime ();
		if (active)
		{
			timeInSec += Time.deltaTime;
		}
	}
	
	public void pauseStopwatch()
	{
		active = false;
	}
	
	public void resumeStopwatch()
	{
		active = true;
	}
	
	public void startStopwatch()
	{
		restartStopwatch();
	}
	
	public void restartStopwatch()
	{
		timeInSec = 0.0f;
		active = true;	
	}
	
	public string getFormattedTimeString()
	{
		string[] result = new string[4];
		ts = TimeSpan.FromSeconds(timeInSec);
		result[0] = ts.Hours.ToString().PadLeft(2,'0');
		result[1] = ts.Minutes.ToString().PadLeft(2,'0');
		result[2] = ts.Seconds.ToString().PadLeft(2,'0');
		result[3] = ts.Milliseconds.ToString().PadLeft(3,'0');	
		
		//string timeString = result[0] + ":"+ result[1] + ":" + result[2] + "." + result[3];
		string timeString = result[0] + ":" + result[1] + ":" + result[2];
		return timeString;
	}
	
	public string getTime()
	{
//		Debug.Log("Time: "+TimeSpan.FromSeconds(timeInSec).ToString());
		string timeString = TimeSpan.FromSeconds(timeInSec).ToString();
		return timeString.Substring(0,timeString.Length - 4);
	}
}
