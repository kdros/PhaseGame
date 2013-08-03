using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Main Screen Script
 * Handles the transition between each option.
 * PlayerPref keys - values: LevelToLoad - [list of texts containing the levels to show]
 *							 GameMode - [Normal, SpeedRun] 					
 */
public class GameMenu : MonoBehaviour 
{
	public Transform HideAnchorRight;
	public Transform HideAnchorLeft;
	public Transform DisplayCol1Anchor;
	public Transform DisplayCol2Anchor;
	public Transform DisplayCol3Anchor;
	
	public GameObject[] MainMenuOptions;
	public GameObject[] ContinueMenuOptions;
	public GameObject[] ModeMenuOptions;
	
	private List<TextScript> MainMenuOptionScripts;
	private List<string> MainMenuNames;
	
	private List<TextScript> ContinueMenuOptionScripts;
	private List<string> ContinueMenuNames;
	
	private List<TextScript> ModeMenuOptionScripts;	
	private List<string> ModeMenuNames;
	
//	public GameObject NewGameText;						// game object representing "New Game"
//	public GameObject ContinueText;						// game object representing "Continue"
//	public GameObject ModeText;							// game object representing "Mode"
//	public GameObject ExitText;							// game object representing "Exit"
//	public GameObject SpeedRunText;						// game object representing "Mode-Speed Run"
//	public GameObject NormalText;						// game object representing "Mode-Normal"
//	public GameObject BackText;							// game object representing "Mode-Back"
//	
//	private LoadLevelText newGameScript;
//	private ContinueText continueScript;
//	private ModeText modeScript;
//	private ExitText exitScript;
//	private NormalText normalScript;
//	private SpeedRunText speedRunScript;
//	private BackText backScript;
	
	private int display;								// current menu that is being displayed
	private int last;									// last menu that was displayed
	private bool transitionDone;						// whether all menu options are done transitioning or not
	private int fromSubMenu;							// keep track of which back button was clicked
	
	private Dictionary<string,bool> textTransStat; 		// see if transitioning of a particular text is done or not
														// use the name of the text as key and true for done, false otherwise
	
	private enum MenuState {MainMenuOptions, ModeOptions, ContinueOptions};
	
	// Use this for initialization
	void Start () 
	{
		display = (int)MenuState.MainMenuOptions;
		last = display;
		transitionDone = true;
		
		MainMenuOptionScripts = new List<TextScript>();
		ContinueMenuOptionScripts = new List<TextScript>();
		ModeMenuOptionScripts = new List<TextScript>();
		
		MainMenuNames = new List<string>();
		ContinueMenuNames = new List<string>();
		ModeMenuNames = new List<string>();
		
		// retrieve classes associated with each text
		for (int i = 0 ; i < MainMenuOptions.Length ; i++)
		{			
			MainMenuOptionScripts.Add(MainMenuOptions[i].GetComponent<TextScript>());	
		}
		
		for (int i = 0 ; i < ContinueMenuOptions.Length ; i++)
		{
			ContinueMenuOptionScripts.Add(ContinueMenuOptions[i].GetComponent<TextScript>());	
		}
		
		for (int i = 0 ; i < ModeMenuOptions.Length ; i++)
		{
			ModeMenuOptionScripts.Add (ModeMenuOptions[i].GetComponent<TextScript>());
		}
		
//		newGameScript = NewGameText.GetComponent<LoadLevelText>();
//		continueScript = ContinueText.GetComponent<ContinueText>();
//		modeScript = ModeText.GetComponent<ModeText>();
//		exitScript = ExitText.GetComponent<ExitText>();
//		normalScript = NormalText.GetComponent<NormalText>();
//		speedRunScript = SpeedRunText.GetComponent<SpeedRunText>();
//		backScript = BackText.GetComponent<BackText>();
		
		initTransitionValues();
		initTextTransStat();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (display != last || !transitionDone)
		{
			if (display == (int)MenuState.MainMenuOptions)
			{
				Debug.Log ("Go back to Main Menu Options");
				if (display != last && last == (int)MenuState.ModeOptions)
				{
					// to begin transition process
					foreach (string name in MainMenuNames)
					{
						textTransStat[name] = false;	
					}
					
					foreach (string name in ModeMenuNames)
					{
						textTransStat[name] = false;	
					}
					
//					textTransStat["NewGameText"] = false;
//					textTransStat["ContinueText"] = false;
//					textTransStat["ModeText"] = false;
//					textTransStat["ExitText"] = false;
//					textTransStat["NormalText"] = false;
//					textTransStat["SpeedRunText"] = false;
//					textTransStat["BackText"] = false;
					
					// disable the texts that causes transitions
					foreach (TextScript ts in MainMenuOptionScripts)
					{
						if (ts.isTransitionText)	
							ts.enabled = false;
					}
					
					foreach (TextScript ts in ModeMenuOptionScripts)
					{
						if (ts.isTransitionText)
							ts.enabled = false;
					}					
					
//					backScript.enabled = false;
//					continueScript.enabled = false;
//					modeScript.enabled = false;	
					
					// since back text was used to return to main menu, need to distinguish between the 
					// back button in Mode Options and Continue
					fromSubMenu = (int)MenuState.ModeOptions;
				}
				
				// transitioning from Mode Options
				if (fromSubMenu == (int)MenuState.ModeOptions)
				{
					bool hideModeDone = hideModeOptions();
					bool showMainMenuOptionsDone = showMainMenuOptions();
				
					transitionDone = hideModeDone && showMainMenuOptionsDone;
				}
			}
			else if (display == (int)MenuState.ModeOptions)
			{
				Debug.Log ("Go to Mode Options");
				if (display != last)
				{
					// to begin transition process
					
					foreach (string name in MainMenuNames)
					{
						textTransStat[name] = false;	
					}
					
					foreach (string name in ModeMenuNames)
					{
						textTransStat[name] = false;	
					}
					
					foreach (TextScript ts in MainMenuOptionScripts)
					{
						if (ts.isTransitionText)	
							ts.enabled = false;
					}
					
					foreach (TextScript ts in ModeMenuOptionScripts)
					{
						if (ts.isTransitionText)
							ts.enabled = false;
					}
					
//					textTransStat["NewGameText"] = false;
//					textTransStat["ContinueText"] = false;
//					textTransStat["ModeText"] = false;
//					textTransStat["ExitText"] = false;
//					textTransStat["NormalText"] = false;
//					textTransStat["SpeedRunText"] = false;
//					textTransStat["BackText"] = false;
					
					// disable the texts that causes transitions
//					backScript.enabled = false;
//					continueScript.enabled = false;
//					modeScript.enabled = false;		
				}
				
				bool showModeDone = showModeOptions();
				bool hideMainMenuOptionsDone = hideMainMenuOptions();
				
				transitionDone = showModeDone && hideMainMenuOptionsDone;
			}
			else if (display == (int)MenuState.ContinueOptions)
			{
				Debug.Log ("Go back to Continue Options");
			}
			else 
			{
				Debug.LogError ("GameMenu reached an unrecognized menu state");		
			}
		}
		
		if (transitionDone)
		{
			// re-enable texts that causes transitions
//			backScript.enabled = true;
//			continueScript.enabled = true;
//			modeScript.enabled = true;
			
			foreach (TextScript ts in MainMenuOptionScripts)
			{
				if (ts.isTransitionText)	
					ts.enabled = true;
			}
					
			foreach (TextScript ts in ModeMenuOptionScripts)
			{
				if (ts.isTransitionText)
					ts.enabled = true;
			}
			
		}
		
		last = display;
	}
	
	// set up speed and distances for each text
	private void initTransitionValues()
	{
		// TODO: ensure that the transforms of all texts start from the appropriate column coordinates
		// right now, this is done by manual positioning.
		
		// MainMenuOptions are initially in display. Therefore, need to compute distance to hiding point
		float mainMenuBaseSpeed = 6.5f;
		int textIndex = 0;
		foreach (TextScript ts in MainMenuOptionScripts)
		{
			// assume that MainMenuOptions hides to the left
			ts.setDistance(Mathf.Abs(HideAnchorLeft.position.x - MainMenuOptions[textIndex].transform.position.x));
			ts.setSpeed(mainMenuBaseSpeed);
			
			mainMenuBaseSpeed = mainMenuBaseSpeed - 1.0f;
			textIndex++;
		}
		
		// Mode Menu Optinos are initially hiding. Therefore, need to compute distance to display point
		// Mode Menu Options are all in column one.
		float modeMenuBaseSpeed = 8.0f;
		textIndex = 0;
		foreach (TextScript ts in ModeMenuOptionScripts)
		{
			ts.setDistance(Mathf.Abs(DisplayCol1Anchor.position.x - ModeMenuOptions[textIndex].transform.position.x));
			ts.setSpeed(modeMenuBaseSpeed);
			
			modeMenuBaseSpeed = modeMenuBaseSpeed - 1.0f;
			textIndex++;
		}
		
		
		
//		newGameScript.setDistance(2.0f);
//		newGameScript.setSpeed(6.5f);
//		
//		continueScript.setDistance(2.0f);
//		continueScript.setSpeed(5.5f);
//		
//		modeScript.setDistance(2.0f);
//		modeScript.setSpeed(4.5f);
//		
//		exitScript.setDistance(2.0f);
//		exitScript.setSpeed(3.5f);
//		
//		// Mode - Normal & New Game are aligned
//		normalScript.setDistance(NormalText.transform.position.x - NewGameText.transform.position.x);
//		normalScript.setSpeed (8.0f);
//		
//		// Mode - Speed Run & Continue are aligned
//		speedRunScript.setDistance(SpeedRunText.transform.position.x - ContinueText.transform.position.x);
//		speedRunScript.setSpeed(7.0f);
//		
//		// Mode - Back & Mode are aligned
//		backScript.setDistance(BackText.transform.position.x - ModeText.transform.position.x);
//		backScript.setSpeed (6.0f);
	}
	
	private void initTextTransStat ()
	{
		textTransStat = new Dictionary<string, bool>();
//		textTransStat.Add ("NewGameText", true);
//		textTransStat.Add ("ContinueText", true);
//		textTransStat.Add ("ModeText", true);
//		textTransStat.Add ("ExitText", true);
//		textTransStat.Add ("SpeedRunText", true);
//		textTransStat.Add ("NormalText", true);
//		textTransStat.Add ("BackText", true);
		
		foreach (TextScript ts in MainMenuOptionScripts)
		{
			MainMenuNames.Add (ts.textName);
			textTransStat.Add (ts.textName, true);
		}
		
		foreach (TextScript ts in ContinueMenuOptionScripts)
		{
			ContinueMenuNames.Add (ts.textName);
			textTransStat.Add (ts.textName, true);	
		}
		
		foreach (TextScript ts in ModeMenuOptionScripts)
		{
			ModeMenuNames.Add (ts.textName);
			textTransStat.Add (ts.textName, true);
		}
	}
	
	/////////////
	// The following functions are called by various derived TextScript classes
	/////////////
	
	public void toModeOptions ()
	{
		display = (int)MenuState.ModeOptions;
	}
	
	public void toMainMenu ()
	{
		display = (int)MenuState.MainMenuOptions;
	}
	
	public void toContinueOptions ()
	{
		display = (int)MenuState.ContinueOptions;	
	}
	
	/////////////
	// The following functions will either show or hide the menu options
	// They all return true if the process is complete, false otherwise	
	/////////////
	
	public bool showMainMenuOptions ()
	{
		// TODO: Generalize this.
		// For now, assume main menu will show if we move it to the right.
		Vector3 showDirection = new Vector3(1,0,0);
		bool ret = true;
		
		int index = 0;
		foreach (string name in MainMenuNames)
		{
			if (!textTransStat[name])
			{
				textTransStat[name] = MainMenuOptionScripts[index].translateText(showDirection);
				ret = ret && textTransStat[name];
			}
			
			index++;
		}
		
		return ret;
		
//		if (!textTransStat["NewGameText"])
//			textTransStat["NewGameText"] = newGameScript.translateText(showDirection);
//		if (!textTransStat["ContinueText"])
//			textTransStat["ContinueText"] = continueScript.translateText(showDirection);
//		if (!textTransStat["ModeText"])
//			textTransStat["ModeText"] = modeScript.translateText(showDirection);
//		if (!textTransStat["ExitText"])
//			textTransStat["ExitText"] = exitScript.translateText(showDirection);
//						
//		return textTransStat["NewGameText"] && textTransStat["ContinueText"] && textTransStat["ModeText"] && textTransStat["ExitText"];
	}
	
	public bool hideMainMenuOptions ()
	{
		Vector3 hideDirection = new Vector3(-1,0,0);
		bool ret = true;
		
		int index = 0;
		foreach (string name in MainMenuNames)
		{
			if (!textTransStat[name])
			{
				textTransStat[name] = MainMenuOptionScripts[index].translateText(hideDirection);
				ret = ret && textTransStat[name];
			}
			
			index++;
		}
		
		return ret;
		
//		if (!textTransStat["NewGameText"])
//			textTransStat["NewGameText"] = newGameScript.translateText(hideDirection);
//		if (!textTransStat["ContinueText"])
//			textTransStat["ContinueText"] = continueScript.translateText(hideDirection);
//		if (!textTransStat["ModeText"])
//			textTransStat["ModeText"] = modeScript.translateText(hideDirection);
//		if (!textTransStat["ExitText"])
//			textTransStat["ExitText"] = exitScript.translateText(hideDirection);
//						
//		return textTransStat["NewGameText"] && textTransStat["ContinueText"] && textTransStat["ModeText"] && textTransStat["ExitText"];
	}
	
	public bool showModeOptions ()
	{
		Vector3 showDirection = new Vector3(-1,0,0);
		bool ret = true;
		
		int index = 0;
		foreach (string name in ModeMenuNames)
		{
			if (!textTransStat[name])
			{
				textTransStat[name] = ModeMenuOptionScripts[index].translateText(showDirection);
				ret = ret && textTransStat[name];
			}
			
			index++;
		}
		
		return ret;
		
//		if (!textTransStat["NormalText"])
//			textTransStat["NormalText"] = normalScript.translateText(showDirection);
//		if (!textTransStat["SpeedRunText"])
//			textTransStat["SpeedRunText"] = speedRunScript.translateText(showDirection);
//		if (!textTransStat["BackText"])
//			textTransStat["BackText"] = backScript.translateText(showDirection);
//		
//		return textTransStat["NormalText"] && textTransStat["SpeedRunText"] && textTransStat["BackText"];
	}
	
	public bool hideModeOptions ()
	{
		Vector3 hideDirection = new Vector3(1,0,0);
		bool ret = true;
		
		int index = 0;
		foreach (string name in ModeMenuNames)
		{
			if (!textTransStat[name])
			{
				textTransStat[name] = ModeMenuOptionScripts[index].translateText(hideDirection);
				ret = ret && textTransStat[name];
			}
			
			index++;
		}
		
		return ret;
		
//		if (!textTransStat["NormalText"])
//			textTransStat["NormalText"] = normalScript.translateText(hideDirection);
//		if (!textTransStat["SpeedRunText"])
//			textTransStat["SpeedRunText"] = speedRunScript.translateText(hideDirection);
//		if (!textTransStat["BackText"])
//			textTransStat["BackText"] = backScript.translateText(hideDirection);
//		
//		return textTransStat["NormalText"] && textTransStat["SpeedRunText"] && textTransStat["BackText"];
	}
	
	public bool showContinueOptions ()
	{
		return true;
	}
	
	public bool hideContinueOptions ()
	{
		return true;
	}
}
