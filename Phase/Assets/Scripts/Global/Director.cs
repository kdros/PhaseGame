using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour 
{
	public struct DarkCave
	{
		public Vector2 Door1;
		public Vector2 Door2;
	};
	
	public Transform spawnPosition;
	
	Transform sceneCamera;

	Color originalAmbientColor;
	bool darkCave;
	DarkCave curDarkCave;
	float[] darknessTriggerSpots;
	
	bool displayMessage = false;
	float displayTime = 0f;
	string messageToBeDisplayed;
	
	System.Collections.Generic.List<IcicleBase> iciclesList;
	System.Collections.Generic.List<DarkCave> darkCavesList;
	
	// Use this for initialization
	void Start () 
	{
		if (System.IO.File.Exists ("Save/currentSave"))
			System.IO.File.Delete ("Save/currentSave");
		
		displayMessage = false;
		displayTime = 0f;
		messageToBeDisplayed = "";
		
		originalAmbientColor = RenderSettings.ambientLight;
		darknessTriggerSpots = new float [2];
		sceneCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Transform>();
		
		iciclesList = new System.Collections.Generic.List<IcicleBase> ();
		GameObject[] icicleBaseArray = GameObject.FindGameObjectsWithTag ("IceCeiling");
		foreach (GameObject icicle in icicleBaseArray)
			iciclesList.Add (icicle.GetComponent<IcicleBase>());
		
		darkCave = false;
		darkCavesList = new System.Collections.Generic.List<DarkCave>();
		icicleBaseArray = GameObject.FindGameObjectsWithTag ("DarkCave");
		foreach (GameObject darkCaveEnterX in icicleBaseArray)
		{
			DarkCave newCave;
			Transform dcTransform = darkCaveEnterX.GetComponent<Transform>();
			Vector3 dcEnter = dcTransform.Find ("DarkCaveEnter").position;
			Vector3 dcExit = dcTransform.Find ("DarkCaveExit").position;
			
			newCave.Door1 = new Vector2 (dcEnter.x, dcEnter.y);
			newCave.Door2 = new Vector2 (dcExit.x, dcExit.y);
			darkCavesList.Add (newCave);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (darkCave)
		{			
			// Now figure out which door of the dark cave the user is close to
			// and lerp the ambient to dark as he approaches the trigger spot.
			if (sceneCamera.position.x <= darknessTriggerSpots [0])
			{
				if (sceneCamera.position.x <= darknessTriggerSpots [1])
				{
					float t = 0f;
					if (Mathf.Abs (sceneCamera.position.x - curDarkCave.Door1.x) < 
						 Mathf.Abs (sceneCamera.position.x - curDarkCave.Door2.x))
						t = (sceneCamera.position.x - curDarkCave.Door1.x) / 
									(darknessTriggerSpots [0] - curDarkCave.Door1.x);	
						
					else
						t = (sceneCamera.position.x - curDarkCave.Door2.x) / 
									(darknessTriggerSpots [1] - curDarkCave.Door2.x);	
				
					if (t < 0f)
						t = 0f;
					else if (t >= 1f-0.01f)
					 	t = 1f;
				
					RenderSettings.ambientLight = Color.Lerp (originalAmbientColor, Color.black, t);
				}
			}
			else if (sceneCamera.position.x >= darknessTriggerSpots [0])
				if (sceneCamera.position.x >= darknessTriggerSpots [1])
				{
					float t = 0f;
					if (Mathf.Abs (sceneCamera.position.x - curDarkCave.Door1.x) < 
						 Mathf.Abs (sceneCamera.position.x - curDarkCave.Door2.x))
						t = (curDarkCave.Door1.x - sceneCamera.position.x) / 
									(curDarkCave.Door1.x - darknessTriggerSpots [0]);	
						
					else
						t = (curDarkCave.Door2.x - sceneCamera.position.x) / 
									(curDarkCave.Door2.x - darknessTriggerSpots [1]);
					
					if (t < 0f)
						t = 0f;
					else if (t >= 1f-0.001f)
					 	t = 1f;
				
					RenderSettings.ambientLight = Color.Lerp (originalAmbientColor, Color.black, t);
				}	
		}
		
//		otherUpdateStuff ();
	}
		
	void OnGUI ()
	{
		if (displayMessage)
		{
			int absoluteWidth = 300, absoluteHeight = 300;
			int width = 1366, height = 768;
			if (Screen.width < 1366)
				width = Screen.width;
			if (Screen.height < 768)
				height = Screen.height;
			int boxWidth = (int)(width * ((float)absoluteWidth/1366f)), 
				boxHeight = (int)(height * ((float)absoluteHeight/768f));
			int boxStartingX = Screen.width - (int)(absoluteWidth*1.5f), boxStartingY = 10;
			int buttonWidth = (int)(boxWidth * 0.5f), buttonHeight = (int)(boxHeight * 0.1f);
			int buttonStartingX = boxStartingX + (int)(boxWidth*0.25f), 
				buttonStartingY = boxStartingY + boxHeight - (int)(buttonHeight*1.5f);
			
			GUIStyle wordWrapStyle = new GUIStyle ();
			wordWrapStyle.wordWrap = true;
			wordWrapStyle.normal.textColor = Color.white;
			wordWrapStyle.alignment = TextAnchor.UpperCenter;
			
			GUI.Box (new Rect (boxStartingX, boxStartingY, boxWidth, boxHeight), messageToBeDisplayed, wordWrapStyle);
			bool dismiss = GUI.Button (new Rect (buttonStartingX, buttonStartingY, buttonWidth, buttonHeight), "Dismiss");
			
			displayTime += Time.deltaTime;
			if (dismiss || (displayTime > 10.0f))
			{
				displayTime = 0f;
				displayMessage = false;
			}
		}
	}
	
	public void OnEnterDarkCave (Collider collider)
	{
		bool found = false;
		for (int i = 0; i < darkCavesList.Count; i ++)//DarkCave darkCave in )
		{
			Vector2 darkCaveDoor = new Vector2 (collider.transform.position.x, collider.transform.position.y);
			if (darkCaveDoor.Equals (darkCavesList [i].Door1) || darkCaveDoor.Equals (darkCavesList [i].Door2))
			{	
				found = darkCave = true;
				curDarkCave = darkCavesList [i];
				break;
			}	
		}

		if (!found)
			throw new UnityException ("DarkCaveEnter triggered on a DarkCave that doesn't exist!");
		else
		{
			// Find centre of the Cave.
			float darkCaveCentreX = Mathf.Abs(curDarkCave.Door1.x - curDarkCave.Door2.x)/2f;
			if (curDarkCave.Door1.x > curDarkCave.Door2.x)
				darkCaveCentreX += curDarkCave.Door2.x;
			else
				darkCaveCentreX += curDarkCave.Door1.x;
			
			// Find spots at which the ambient should completely be black.
			// That is, when player reaches any of these points from outside the cave,
			// the ambient should have completely transitioned to darkness.
			// If instead, the player reaches these points from inside the cave, 
			// the ambient slowly starts transitioning to original colour as he makes
			// his way out of the cave.
			darknessTriggerSpots [0] = Mathf.Abs(darkCaveCentreX - curDarkCave.Door1.x)/4f;
			darknessTriggerSpots [1] = Mathf.Abs(darkCaveCentreX - curDarkCave.Door2.x)/4f;
			if (curDarkCave.Door1.x > curDarkCave.Door2.x)
			{
				darknessTriggerSpots [1] += curDarkCave.Door2.x;
				darknessTriggerSpots [0] = curDarkCave.Door1.x - darknessTriggerSpots [0];
			}
			else
			{
				darknessTriggerSpots [0] += curDarkCave.Door1.x;
				darknessTriggerSpots [1] = curDarkCave.Door2.x - darknessTriggerSpots [1];
			}
		}
	}
	
	public void OnDarkCaveExit (Collider collider)
	{
		if (!darkCave)
			OnEnterDarkCave (collider);
		else
		{
			darkCave = false;
			RenderSettings.ambientLight = originalAmbientColor;
			curDarkCave.Door1 = Vector2.zero;
			curDarkCave.Door2 = Vector2.zero;

		}
	}
	
	public void ResetIcicles ()
	{
		foreach (IcicleBase icicle in iciclesList)
			icicle.Reset ();
	}
	
	public Vector3 GetSpawnPoint ()
	{
		switch (Application.platform)
		{
		// You mean one cannot fall through a case label?
		// NOT COOL, C#. :/
		case RuntimePlatform.OSXEditor: goto case RuntimePlatform.WindowsPlayer;
		case RuntimePlatform.OSXDashboardPlayer: goto case RuntimePlatform.WindowsPlayer;
		case RuntimePlatform.OSXPlayer: goto case RuntimePlatform.WindowsPlayer;
		case RuntimePlatform.WindowsEditor: goto case RuntimePlatform.WindowsPlayer;
		case RuntimePlatform.WindowsPlayer:
			if (System.IO.File.Exists ("Save/currentSave"))
			{
				Vector3 newPos = new Vector3 ();
				System.IO.StreamReader sr = new System.IO.StreamReader ("Save/currentSave");
				for (int i = 0; i < 3; i ++)
					newPos [i] = float.Parse (sr.ReadLine ());
				sr.Close ();
				spawnPosition.position = newPos;
			}
			break;
		}
//		Vector3 thisIsStupid = new Vector3 ();
//		thisIsStupid = spawnPosition.position;
		return spawnPosition.position;//thisIsStupid;
	}
	
	public void SaveSpawnPoint (Vector3 spawnPt)
	{
		switch (Application.platform)
		{ 
		case RuntimePlatform.OSXEditor: goto case RuntimePlatform.WindowsPlayer;
		case RuntimePlatform.OSXDashboardPlayer: goto case RuntimePlatform.WindowsPlayer;
		case RuntimePlatform.OSXPlayer: goto case RuntimePlatform.WindowsPlayer;
		case RuntimePlatform.WindowsEditor: goto case RuntimePlatform.WindowsPlayer;
		case RuntimePlatform.WindowsPlayer:
			if (!System.IO.Directory.Exists ("Save"))
				System.IO.Directory.CreateDirectory ("Save");
			System.IO.StreamWriter sr = new System.IO.StreamWriter ("Save/currentSave");
			sr.WriteLine ("{0}", spawnPt [0]);
			sr.WriteLine ("{0}", spawnPt [1]);
			sr.WriteLine ("{0}", spawnPt [2]);
			sr.Close ();
			break;
		}
		spawnPosition.position = spawnPt;
	}
	
	public void ShowTriggerText (string colliderName)
	{
//		string triggerName = collider.name;
		int positionOfDelimiter = colliderName.IndexOf ('_');
		if (positionOfDelimiter == -1)
		{
			Debug.Log ("Please name your trigger properly!");
			return;
		}
		string levelID = colliderName.Remove (positionOfDelimiter);

		// Web Player compatibility to be figured out later.
		System.IO.StreamReader sr = new System.IO.StreamReader ("Assets/Text/TextInfo.txt");
		string contents = sr.ReadToEnd ();
		sr.Close ();
		int position = contents.IndexOf (colliderName);
		if (position == -1)
		{
			Debug.Log ("Specified trigger wasn't found in TextInfo.");
			return;
		}
		position += colliderName.Length + 1;
		int finishingPos = contents.IndexOf (levelID, position);
		
		string rawMessage = "Placeholder";
		if (finishingPos != -1)
			rawMessage = contents.Substring (position, finishingPos-position);
		else
			// This indicates that the current trigger is the last in the file.
			rawMessage = contents.Substring (position);
		messageToBeDisplayed = rawMessage.Trim ();
		displayMessage = true;
		displayTime = 0f;
	}
}
