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
	public int lastLevel = Constants.lastPlayableSceneIndex;
	
	public Font messageFont;
	public Font menuFont;
	
	public Texture2D defaultIcon;
	public Texture2D solidIcon;
	public Texture2D liquidIcon;
	public Texture2D gasIcon;
	public Texture2D plasmaIcon;
	public Texture2D pausedTexture;
	public Texture2D pauseBkgdTexture;
	
	// Character profile textures
	public Texture2D blackBackground;
	public Texture2D mattyDescription;
	public Texture2D liquidDescription;
	public Texture2D solidDescription;
	public Texture2D gasDescription;
	public Texture2D plasmaDescription;
	
	public float maximumSpeedUp = 150f;
	
	Transform sceneCamera;
	CameraFollow camFollow;
	LevelDirector ld;
	
	Color originalAmbientColor;
	bool darkCave;
	DarkCave curDarkCave;
	float[] darknessTriggerSpots;
	
	Light[] lights;
	float[] origLightIntensities;
	Material origSkybox;
	
	bool displayMessage = false;
	bool displayPauseMenu = false;
	bool mattyProfile = false;
	bool liquidProfile = false;
	bool solidProfile = false;
	bool gasProfile = false;
	bool plasmaProfile = false;
	bool panTriggerActive = false;
	float displayTime = 0f;
	float one = 1f;
	string triggerMessages, messageToBeDisplayed, messageToPlayer = "";
	
	System.Collections.Generic.List<IcicleBase> iciclesList;
	System.Collections.Generic.List<DarkCave> darkCavesList;
	
	// For displaying message boxes:
	int absoluteWidth, absoluteHeight, width, height, screenWidthBy2, screenHeightBy2;
	int boxWidth, boxHeight, boxStartingX, boxStartingY;
	int buttonWidth, buttonHeight, buttonStartingX, buttonStartingY;
	
	GUIStyle stateInfoBox;
	int[] reorderedStates = new int [5] {2, 1, 3, 4, 0};	// To match display to keyboard order.
	MainPlayerScript player;
	
	PanTrigger.PanDestination[] dest;
	Transform currentSubsectCamPos; // Used to keep track of where the subcamera should be positioned.
	Camera mainCam;
	Camera subsectionCam;
	bool startSubsectCam; // set to true once the player has reached the first pan trigger
	GameObject subcam;
	
	int currentIndex = 0;
	
	GameObject levelStopwatch;
	Stopwatch stopwatch;
	bool isSpeedRun;
		
	// Get the current scene
//	public string currentLevel;
//	public int currentLevelNum;
//	public float moveTimer;
	
	// Use this for initialization
	void Start () 
	{
		cacheCurrentLevelIndex();
		
		if (System.IO.File.Exists ("Save/currentSave"))
			System.IO.File.Delete ("Save/currentSave");
		
		displayMessage = false;
		displayPauseMenu = false;
		bool mattyProfile = false;
		bool liquidProfile = false;
		bool solidProfile = false;
		bool gasProfile = false;
		bool plasmaProfile = false;
		panTriggerActive = false;
		displayTime = 0f;
		one = 1f;
		messageToBeDisplayed = "";
		if (tipDisplayTime == 0f)
			tipDisplayTime = 5f;
		
		originalAmbientColor = RenderSettings.ambientLight;
		darknessTriggerSpots = new float [2];
		sceneCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Transform>();
		camFollow = sceneCamera.gameObject.GetComponent<CameraFollow>();
		
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
		
		// Creates a backup of the light intensities of all the lights in the scene.
		// This is needed since we need to lerp the light intensities to zero when player
		// enters dark caves.
		lights = GameObject.FindSceneObjectsOfType (typeof(Light)) as Light[];
		origLightIntensities = new float [lights.Length];
		for (int i = 0; i < lights.Length; i ++)
			origLightIntensities [i] = lights [i].intensity;
		
		// Creates a backup of the skybox assigned to the scene.
		// This is needed since we set the skybox to null as the player enters dark caves.
		origSkybox = RenderSettings.skybox;
		
		// Needed for dark caves. If background colour is not set to black, dark caves
		// won't be "dark" anymore.
		sceneCamera.gameObject.GetComponent<Camera>().backgroundColor = Color.black;
		
		// Define the GUIStyle to be used in the State Info Box at the bottom of the screen.
		stateInfoBox = new GUIStyle ();
		stateInfoBox.name = "StateInfoBox";
		stateInfoBox.alignment = TextAnchor.LowerRight;
		stateInfoBox.font = menuFont;
		stateInfoBox.normal.textColor = Color.white;
		
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<MainPlayerScript>();
		
		try
		{
			ld = GameObject.FindGameObjectWithTag ("LevelDirector").GetComponent<LevelDirector>();
		}
		catch (System.Exception e)
		{
			Debug.LogWarning ("A Level Director was not found for this level!");
		}
		
		try
		{
			levelStopwatch = GameObject.FindGameObjectWithTag("Stopwatch");
			stopwatch = levelStopwatch.GetComponent<Stopwatch>();
		
			if (PlayerPrefs.GetString(Constants.GameModeKey).Equals(Constants.GameModeSpeedRun))
			{
				stopwatch.startStopwatch();
				isSpeedRun = true;
			}
		}
		catch (System.NullReferenceException e)
		{
			Debug.LogWarning ("Stopwatch was not found for this level!");	
		}
		catch (System.Exception e)
		{
			Debug.LogError (e.Message);
		}
		
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		
		try
		{
			subcam = GameObject.FindGameObjectWithTag("SubsectionCamera");
			subsectionCam = subcam.GetComponent<Camera>();
			//subsectionCamPos = subcam.transform;
			
			subsectionCam.enabled = false;
			mainCam.enabled = true;
		}
		catch (System.Exception e)
		{
			Debug.LogWarning("No subsection camera was found");	
		}
		
		startSubsectCam = false;
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
					for (int i = 0; i < lights.Length; i ++)
						lights [i].intensity = Mathf.Lerp (origLightIntensities [i], 0, t);
				}
			}
			else if (sceneCamera.position.x >= darknessTriggerSpots [0])
			{	
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
					for (int i = 0; i < lights.Length; i ++)
						lights [i].intensity = Mathf.Lerp (origLightIntensities [i], 0, t);
				}
			}
			
			if (RenderSettings.ambientLight == originalAmbientColor)
				RenderSettings.skybox = origSkybox;
			else
				RenderSettings.skybox = null;
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
		
		if (Input.GetButtonDown ("LoadPauseMenu"))
			PauseGame ();
		
		if (panTriggerActive && !isSpeedRun)
		{
			if (player.IsControllable ())
				player.SetPlayerControl (false);
			
			if (currentIndex < dest.Length)
			{
				// Cycle through all the positions.
				if (camFollow.isCameraInPosition ())
				{
					// If camera has completed panning to (and waiting at) the previous position,
					// pan to the next (current) position.
					if (dest [currentIndex].panTime == 0f)
						dest [currentIndex].panTime = -1f;
					camFollow.PanToAbsolute (dest [currentIndex].position.position, dest [currentIndex].waitTime, 
											 dest [currentIndex].panTime);
					messageToPlayer = dest [currentIndex].message;
					currentIndex ++;
				}
			}
			else if (currentIndex == dest.Length)
			{
				// Camera has panned or is panning to the last position. 
				if (camFollow.isCameraInPosition ())
				{
					// Camera has completed panning to (and waiting at) the last position. 
					// So, pan back to where the player is.
					camFollow.panTo (player.transform.position.x, player.transform.position.y);
					currentIndex ++;
				}
			}
			else
			{
				// Camera has panned to all positions and is returning to focu on player. 
				if (camFollow.isCameraInPosition ())
				{
					// Camera has panned back to where the player is.
					panTriggerActive = false;
					currentIndex = 0;
					
					if (!player.IsControllable ())
						player.SetPlayerControl (true);
				}
			}
			
			if (camFollow.IsStopped ())
			{	
				// If camera is waiting at the current position, display message, if any.
				if (messageToPlayer != "")
				{
					DisplayMessage (messageToPlayer);
					messageToPlayer = "";
				}
			}
		}
		
		// when player presses "e", main camera will be disabled and subsectionCamera will be enabled.
		// The subsectionCamera should be set up for each pan trigger and it is meant to let the player view the
		// area covered by the pan trigger again (but this time, the player will be able to view everything at once through
		// a static view).
		if (Input.GetButtonDown("toSubsectionCam") && startSubsectCam)
		{
			
			if (subsectionCam != null && currentSubsectCamPos != null)
			{
				mainCam.enabled = !mainCam.enabled;	
				subsectionCam.enabled = !subsectionCam.enabled;
				
				if (subsectionCam.enabled == true)
					player.SetPlayerControl (false);
				else
					player.SetPlayerControl (true);
			}
		}
	}
		
	void OnGUI ()
	{
		GUIStyle newStyle = new GUIStyle ();		
		newStyle = GUI.skin.button;
		newStyle.font = menuFont;
		newStyle.alignment = TextAnchor.MiddleCenter;
		GUI.skin.button = newStyle;
		
		if (displayMessage && !isSpeedRun)
		{
			GUIStyle wordWrapStyle = new GUIStyle ();
			wordWrapStyle.wordWrap = true;
			wordWrapStyle.normal.textColor = Color.white;
			wordWrapStyle.alignment = TextAnchor.UpperCenter;
			wordWrapStyle.font = messageFont;
			
			GUILayout.BeginArea (new Rect (boxStartingX, boxStartingY, boxWidth, boxHeight));
			GUILayout.BeginVertical ();
				GUILayout.Box (messageToBeDisplayed, wordWrapStyle);
				GUILayout.BeginHorizontal ();
					// Left pad the Button with an empty box so that it ends up in the centre.
					GUILayout.Box ("", wordWrapStyle, GUILayout.MaxWidth (buttonWidth/2f), 
									GUILayout.MaxHeight (buttonHeight));
					bool dismiss = GUILayout.Button ("Dismiss", GUILayout.MaxWidth (buttonWidth), 
														GUILayout.MaxHeight (buttonHeight));
					// Right pad the Button with an empty box so that it ends up in the centre.
					GUILayout.Box ("", wordWrapStyle, GUILayout.MaxWidth (buttonWidth/2f), 
									GUILayout.MaxHeight (buttonHeight));
				GUILayout.EndHorizontal ();
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
			
			if ((dismiss) || (Input.GetKeyUp (KeyCode.Escape)))
			{
				displayTime = 0f;
				displayMessage = false;
			}
		}
		
		if (displayPauseMenu)
		{
			//stopwatch.pauseStopwatch();
			loadPauseMenu ();
		}
		if (mattyProfile)
		{
			loadMattyProfile ();
		}
		if (liquidProfile)
		{
			loadLiquidProfile ();
		}
		if (solidProfile)
		{
			loadSolidProfile ();
		}
		if (gasProfile)
		{
			loadGasProfile ();
		}
		if (plasmaProfile)
		{
			loadPlasmaProfile ();
		}
		//else
		//	stopwatch.resumeStopwatch();
		
		// Display boxes to show possible transitions.
		for (int i = 0; i < 5; i++)
		{
			GUI.enabled = player.IsStateEnabled (reorderedStates [i]);
			GUIContent content = new GUIContent ();
			switch (reorderedStates [i])
			{
			case 0:
				stateInfoBox.normal.background = defaultIcon;
				content.text = "Z";
				break;
			case 1:
				stateInfoBox.normal.background = solidIcon;
				content.text = "S";
				break;
			case 2:
				stateInfoBox.normal.background = liquidIcon;
				content.text = "A";
				break;
			case 3:
				stateInfoBox.normal.background = gasIcon;
				content.text = "D";
				break;
			case 4:
				stateInfoBox.normal.background = plasmaIcon;
				content.text = "F";
				break;
			}
			
			if (reorderedStates [i] == player.CurrentState ())
				stateInfoBox.normal.textColor = Color.green;
			else if (!GUI.enabled)
				stateInfoBox.normal.textColor = Color.red;
			else
				stateInfoBox.normal.textColor = Color.white;
				
			GUI.Box (new Rect ((float)screenWidthBy2 + ((float)i - 2.5f)*70f, 
								Screen.height - 64f - 5f, 64f, 64f), content, stateInfoBox);
		}
		
		// Set GUI.enabled to true so that subsequent GUI objects aren't rendered as disabled.
		GUI.enabled = true;
	}
	
	void loadPauseMenu()
	{
		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 260, Screen.height / 2 -290, 609, 609), pauseBkgdTexture);

		if(GUI.Button(new Rect(180.0f, 162.0f, 160, 30), "Resume Game"))
		{
			PauseGame ();
		}
		if(GUI.Button(new Rect(180.0f, 227.0f, 160, 30), "Restart Game"))
		{
			PauseGame ();
			Application.LoadLevel(Application.loadedLevel);
		}
		if(GUI.Button(new Rect(180.0f, 292.0f, 160, 30), "Character Profiles"))
		{
			PauseGame ();
			MattyProfile();
		}
		if(GUI.Button(new Rect(180.0f, 357.0f, 160, 30), "Exit to Main Menu"))
		{
			PauseGame ();
			Application.LoadLevel(1);
		}
		
		GUI.EndGroup();	
	}
	
	void loadMattyProfile()
	{

		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), mattyDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Liquid"))
		{
			MattyProfile();
			LiquidProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Solid"))
		{
			MattyProfile();
			SolidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Gas"))
		{
			MattyProfile();
			GasProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Plasma"))
		{
			MattyProfile();
			PlasmaProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			MattyProfile();
			PauseGame();
		}
		
		GUI.EndGroup();	
	}
	
	void loadLiquidProfile()
	{
		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), liquidDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Matty"))
		{
			LiquidProfile();
			MattyProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Solid"))
		{
			LiquidProfile();
			SolidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Gas"))
		{
			LiquidProfile();
			GasProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Plasma"))
		{
			LiquidProfile();
			PlasmaProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			LiquidProfile();
			PauseGame();
		}
		
		GUI.EndGroup();
		
	}
	
	void loadSolidProfile()
	{
		//m_defaultMatty = Instantiate (defaultMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
			//m_liquidMatty = Instantiate (liquidMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
			//m_gasMatty = Instantiate (gasMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
			//m_plasmaMatty = Instantiate (plasmaMatty, gameObject.transform.position, Quaternion.identity) as GameObject;
		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), solidDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Matty"))
		{
			SolidProfile();
			MattyProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Liquid"))
		{
			SolidProfile();
			LiquidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Gas"))
		{
			SolidProfile();
			GasProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Plasma"))
		{
			SolidProfile();
			PlasmaProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			SolidProfile();
			PauseGame();
		}
		
		GUI.EndGroup();
		
	}
	
	void loadGasProfile()
	{
		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), gasDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Matty"))
		{
			GasProfile();
			MattyProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Liquid"))
		{
			GasProfile();
			LiquidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Solid"))
		{
			GasProfile();
			SolidProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Plasma"))
		{
			GasProfile();
			PlasmaProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			GasProfile();
			PauseGame();
		}
		
		GUI.EndGroup();
		
	}
	
	void loadPlasmaProfile()
	{
		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), plasmaDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Matty"))
		{
			PlasmaProfile();
			MattyProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Liquid"))
		{
			PlasmaProfile();
			LiquidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Solid"))
		{
			PlasmaProfile();
			SolidProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Gas"))
		{
			PlasmaProfile();
			GasProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			PlasmaProfile();
			PauseGame();
		}
		
		GUI.EndGroup();
		
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
		boxWidth = (int)((float)(width/1366f) * (float)absoluteWidth); 
		boxHeight = (int)((float)(height/768f) * (float)absoluteHeight);
		
		// Position the box at upper right corner, padded to the left by
		// 1.1 times the width of the box at the current resolution.
		boxStartingX = Screen.width - (int)(absoluteWidth*1.1f); 
		boxStartingY = 10;
		
		// Button's width specified as half the width of the box. Height is 10% of box's height.
		buttonWidth = (int)(boxWidth * 0.5f);
		buttonHeight = (int)(boxHeight * 0.1f);
		
		// Centre the button in the box.
		buttonStartingX = boxStartingX + (int)(boxWidth*0.25f); 
		buttonStartingY = boxStartingY + boxHeight - (int)(buttonHeight*1.5f);
			
		screenWidthBy2 = Screen.width/2;
		screenHeightBy2 = Screen.height/2;
	}
	
	void LoadTriggerMessagesFromFile ()
	{
		System.IO.StreamReader sr = new System.IO.StreamReader (Application.dataPath + "/Text/TextInfo.txt");
		if (sr != null)
			triggerMessages = sr.ReadToEnd ();
		else
			triggerMessages = "Phase (c)2013 MR. K"; 
		sr.Close ();
	}
	
	void PauseGame ()
	{
		displayPauseMenu = !displayPauseMenu;
		one *= -1f;
		Time.timeScale += one;
		player.SetPlayerControl (!displayPauseMenu);
	}
	
	void MattyProfile()
	{
		mattyProfile = !mattyProfile;
		one *= -1f;
		Time.timeScale += one;
		player.SetPlayerControl (!mattyProfile);
	}
	
	void LiquidProfile()
	{
		liquidProfile = !liquidProfile;
		one *= -1f;
		Time.timeScale += one;
		player.SetPlayerControl (!liquidProfile);
	}
	
	void SolidProfile()
	{
		solidProfile = !solidProfile;
		one *= -1f;
		Time.timeScale += one;
		player.SetPlayerControl (!solidProfile);
	}
	
	void GasProfile()
	{
		gasProfile = !gasProfile;
		one *= -1f;
		Time.timeScale += one;
		player.SetPlayerControl (!gasProfile);
	}
	
	void PlasmaProfile()
	{
		plasmaProfile = !plasmaProfile;
		one *= -1f;
		Time.timeScale += one;
		player.SetPlayerControl (!plasmaProfile);
	}
	
	public void OnEnterDarkCave (Collider collider)
	{
		if (darkCave)
			OnDarkCaveExit (collider);
		
		else
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
				
				RenderSettings.skybox = null;
			}
		}
	}
	
	public void OnDarkCaveExit (Collider collider)
	{
		if (!darkCave)
			OnEnterDarkCave (collider);
		else
			ResetDarkCave ();
	}
	
	public void ResetIcicles ()
	{
		foreach (IcicleBase icicle in iciclesList)
			icicle.Reset ();
	}
	
	public void ResetDarkCave ()
	{
		darkCave = false;
		RenderSettings.ambientLight = originalAmbientColor;
		RenderSettings.skybox = origSkybox;
		curDarkCave.Door1 = Vector2.zero;
		curDarkCave.Door2 = Vector2.zero;
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
			finishingPos = triggerMessages.IndexOf ("\n", position);
			if (finishingPos-position == 0) // If current position in string = last char 
			{	
				position ++; // Move position to the next line.
				finishingPos = triggerMessages.IndexOf ("\n", position);
			}
			else if (finishingPos == -1) // If current trigger is the very last in the file.
				finishingPos = triggerMessages.Length;
			
			// Hack to handle inconsistent line endings across platforms.
			if (triggerMessages [finishingPos-1] == '\r')
				finishingPos --;
				
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
	
	public void MoveToNextLevel()
	{
		int currentLevelNum = Application.loadedLevel;
		
		if (isSpeedRun)
			storeTime();
		
		if (currentLevelNum == lastLevel)
			Application.LoadLevel(1); // Load GameMenu scene
		else
			Application.LoadLevel((currentLevelNum + 1)); // Load the next level	
	}
	
	// used to keep track of the index of the scene that is currently loaded. This will tell Main Menu how many items to display 
	// in the Continue Submenu.
	public void cacheCurrentLevelIndex()
	{
		int lastLevel = PlayerPrefs.GetInt(Constants.LevelToLoadKey);
		int currentLevel = Application.loadedLevel;
		
		if (currentLevel > lastLevel)
			PlayerPrefs.SetInt(Constants.LevelToLoadKey, Application.loadedLevel);
	}
	
	public void PanTrigger (Collider collider)
	{
		PanTrigger pt = collider.gameObject.GetComponent<PanTrigger>();
		dest = pt.destination;
		panTriggerActive = true;
		currentIndex = 0;
		currentSubsectCamPos = pt.subsectionCameraPos;
		
		if (currentSubsectCamPos != null)
			subcam.transform.position = currentSubsectCamPos.transform.position;
		
		startSubsectCam = true;
	}
	
	public float GetMaxSpeedUp ()
	{
		return maximumSpeedUp;
	}
	
	// used to store the total time it took to complete this level.
	// will keep track of top 5 fastest attempts
	public void storeTime()
	{
		RecordManager rm = gameObject.GetComponent("RecordManager") as RecordManager;
		rm.addRecord(Application.loadedLevel,stopwatch.getTime());
	}
}
