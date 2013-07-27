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
	public float tipDisplayTime = 0f;
	
	Transform sceneCamera;
	LevelDirector ld;
	
	Color originalAmbientColor;
	bool darkCave;
	DarkCave curDarkCave;
	float[] darknessTriggerSpots;
	
	bool displayMessage = false;
	float displayTime = 0f;
	string triggerMessages, messageToBeDisplayed;
	
	System.Collections.Generic.List<IcicleBase> iciclesList;
	System.Collections.Generic.List<DarkCave> darkCavesList;
	
	// For displaying message boxes:
	int absoluteWidth, absoluteHeight, width, height;
	int boxWidth, boxHeight, boxStartingX, boxStartingY;
	int buttonWidth, buttonHeight, buttonStartingX, buttonStartingY;
		
	// Get the current scene
	public string currentLevel;
	public int currentLevelNum;
	public float moveTimer;
	
	// Use this for initialization
	void Start () 
	{
		if (System.IO.File.Exists ("Save/currentSave"))
			System.IO.File.Delete ("Save/currentSave");
		
		displayMessage = false;
		displayTime = 0f;
		messageToBeDisplayed = "";
		if (tipDisplayTime == 0f)
			tipDisplayTime = 10f;
		
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
		
		LoadTriggerMessagesFromFile ();
		GUIDimensionSetup ();
		SetCurrentLevel();
		
		try
		{
			ld = GameObject.FindGameObjectWithTag ("LevelDirector").GetComponent<LevelDirector>();
		}
		catch (System.Exception e)
		{
			Debug.LogWarning ("A Level Director was not found for this level!");
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
		
		if (displayMessage)
		{
			displayTime += Time.deltaTime;
			if (displayTime > tipDisplayTime)
			{
				displayTime = 0f;
				displayMessage = false;
			}
		}

	}
		
	void OnGUI ()
	{
		if (displayMessage)
		{
			GUIStyle wordWrapStyle = new GUIStyle ();
			wordWrapStyle.wordWrap = true;
			wordWrapStyle.normal.textColor = Color.white;
			wordWrapStyle.alignment = TextAnchor.UpperCenter;
			
			GUI.Box (new Rect (boxStartingX, boxStartingY, boxWidth, boxHeight), messageToBeDisplayed, wordWrapStyle);
			bool dismiss = GUI.Button (new Rect (buttonStartingX, buttonStartingY, buttonWidth, buttonHeight), "Dismiss");
			
			if (dismiss)
			{
				displayTime = 0f;
				displayMessage = false;
			}
		}
	}
	
	void GUIDimensionSetup ()
	{
		// Box dimensions are 300x300 for a 1366x768 screen.
		absoluteWidth = 300; absoluteHeight = 300;
		width = 1366; height = 768;
		
		// Scale box in accordance with resolution. Do not scale if resolution higher than 1366x768. 
		if (Screen.width < 1366)
			width = Screen.width;
		if (Screen.height < 768)
			height = Screen.height;
		
		// Scaling logic.
		boxWidth = (int)(width * ((float)absoluteWidth/1366f)); 
		boxHeight = (int)(height * ((float)absoluteHeight/768f));
		
		// Position the box at upper right corner, padded to the left by
		// 1.5 times the width of the box at the current resolution.
		boxStartingX = Screen.width - (int)(absoluteWidth*1.5f); 
		boxStartingY = 10;
		
		// Button's width specified as half the width of the box. Height is 10% of box's height.
		buttonWidth = (int)(boxWidth * 0.5f);
		buttonHeight = (int)(boxHeight * 0.1f);
		
		// Centre the button in the box.
		buttonStartingX = boxStartingX + (int)(boxWidth*0.25f); 
		buttonStartingY = boxStartingY + boxHeight - (int)(buttonHeight*1.5f);
	}
	
	void LoadTriggerMessagesFromFile ()
	{
		System.IO.StreamReader sr = new System.IO.StreamReader ("Assets/Text/TextInfo.txt");
		triggerMessages = sr.ReadToEnd ();
		sr.Close ();
	}
	
	public void OnEnterDarkCave (Collider collider)
	{
		bool found = false;
		for (int i = 0; i < darkCavesList.Count; i ++)
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
		return spawnPosition.position;
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
		int positionOfDelimiter = colliderName.IndexOf ('_');
		if (positionOfDelimiter == -1)
		{
			Debug.Log ("Please name your trigger properly!");
			return;
		}
		string levelID = colliderName.Remove (positionOfDelimiter);

		// Web Player compatibility to be figured out later.
		int position = triggerMessages.IndexOf (colliderName);
		if (position == -1)
		{
			Debug.Log ("Specified trigger wasn't found in TextInfo.");
			return;
		}
		position += colliderName.Length + 1;
		int finishingPos = triggerMessages.IndexOf (levelID, position);
		
		string rawMessage = "Placeholder";
		if (finishingPos != -1)
			rawMessage = triggerMessages.Substring (position, finishingPos-position);
		else
		{	// This indicates that the current trigger is the last with the specified levelID.
			
			// Find index of last char in string.
			finishingPos = triggerMessages.IndexOf (System.Environment.NewLine, position);
			if (finishingPos == 0) // If current position in string = last char 
			{	
				position += System.Environment.NewLine.Length; // Move position to the next line.
				finishingPos = triggerMessages.IndexOf (System.Environment.NewLine, position); 
			}
			else if (finishingPos == -1) // If current trigger is the very last in the file.
				finishingPos = triggerMessages.Length;
			// Read the whole line.
			rawMessage = triggerMessages.Substring (position, finishingPos-position);
		}
		
		DisplayMessage (rawMessage.Trim ()); 
	}
	
	public void DisplayMessage (string message_in)
	{
		messageToBeDisplayed = message_in;
		displayMessage = true;
		displayTime = 0f;
	}
	
	// Handles event triggers when player collides with them
	public bool EventTrigger (string eventName)
	{
		if (ld != null)
			return ld.OnEventTrigger (eventName);
		return false;
	}
	
	// Handles event triggers when other objects collide with them
	public bool EventTrigger (string eventName, string colliderName)
	{
		if (ld != null)
			return ld.OnEventTrigger (eventName, colliderName);
		return false;
	}
	
	public void SetCurrentLevel()
	{
		currentLevelNum = 0;
		
		currentLevel = Application.loadedLevelName;
		
		// Level scenes start at "2", because GameIntro is 0 and GameMenu is 1
		if (currentLevel == "BeginnerLevel1Scene")
			currentLevelNum = 2;
		if (currentLevel == "BeginnerLevel2")
			currentLevelNum = 3;
		if (currentLevel == "BeginnerLevel3")
			currentLevelNum = 4;
		if (currentLevel == "BeginnerLevel4")
			currentLevelNum = 5;
		if (currentLevel == "IntermediateLevel1")
			currentLevelNum = 6;
		if (currentLevel == "IntermediateLevel2")
			currentLevelNum = 7;
		if (currentLevel == "AdvancedLevel1")
			currentLevelNum = 8;
		if (currentLevel == "Winner")
			currentLevelNum = 9;
	}
	
	public void MoveToNextLevel()
	{
		if (currentLevelNum == 9)
			Application.LoadLevel(1); // Load GameMenu scene
		else
			Application.LoadLevel((currentLevelNum + 1)); // Load the next level	
	}
}
