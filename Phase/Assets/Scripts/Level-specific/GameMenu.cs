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
	
	/// <summary>
	/// Things to set in the inspector panel
	/// </summary>
	
	public Transform HideAnchorRight;
	public Transform HideAnchorLeft;
	public Transform DisplayCol1Anchor;
	public Transform DisplayCol2Anchor;
	public Transform DisplayCol3Anchor;
	
	public GameObject[] MainMenuOptions;			
	public GameObject[] ContinueMenuOptions;
	public GameObject[] ModeMenuOptions;
	
	/// <summary>
	/// Private member variables
	/// </summary>
	
	private List<TextScript> MainMenuOptionScripts;
	private List<string> MainMenuNames;
	
	private List<TextScript> ContinueMenuOptionScripts;
	private List<string> ContinueMenuNames;
	
	private List<TextScript> ModeMenuOptionScripts;
	private List<string> ModeMenuNames;
	
	private int sceneToLoad;							// continue menu should show options up to this
	private int display;								// current menu that is being displayed
	private int last;									// last menu that was displayed
	private bool transitionDone;						// whether all menu options are done transitioning or not
	private int fromSubMenu;							// keep track of which back button was clicked
	
	private Dictionary<string,bool> isDoneTransition; 	// see if transitioning of a particular text is done or not
														// use the name of the text as key and true for done, false otherwise
	
	private enum MenuState {MainMenuOptions, ModeOptions, ContinueOptions};
	
	// Use this for initialization
	void Start () 
	{
		if (!PlayerPrefs.HasKey(Constants.LevelToLoadKey))
		{
			PlayerPrefs.SetInt(Constants.LevelToLoadKey,0);
			sceneToLoad = 0;
		}
		else
		{
			sceneToLoad = PlayerPrefs.GetInt(Constants.LevelToLoadKey);	
		}
		
		// default to normal mode
		PlayerPrefs.SetString (Constants.GameModeKey,Constants.GameModeNormal);		
		
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
			TextScript ts = ContinueMenuOptions[i].GetComponent<TextScript>(); // TODO: Only add levels that we should have access to
			
			if(ts.textName.Equals(Constants.continueBackButton))
				ContinueMenuOptionScripts.Add(ts);
			else if (ts.textName.Equals(Constants.loadRecordLevel))
				ContinueMenuOptionScripts.Add(ts);
			else if (ts.textName.Equals(Constants.loadCharProfiles))
				ContinueMenuOptionScripts.Add(ts);
			else if (int.Parse(ts.textName) <= sceneToLoad)
				ContinueMenuOptionScripts.Add(ts);
		}
		
		for (int i = 0 ; i < ModeMenuOptions.Length ; i++)
		{
			ModeMenuOptionScripts.Add (ModeMenuOptions[i].GetComponent<TextScript>());
		}
		
		initTransitionValues();
		initIsDoneTransition();
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
						isDoneTransition[name] = false;	
					}
					
					foreach (string name in ModeMenuNames)
					{
						isDoneTransition[name] = false;	
					}
					
					// disable the texts that causes transitions
					disableTranslationTexts();				
					
					// since back text was used to return to main menu, need to distinguish between the 
					// back button in Mode Options and Continue
					fromSubMenu = (int)MenuState.ModeOptions;
				}
				else if (display != last && last == (int)MenuState.ContinueOptions)
				{
					// to begin transition process
					foreach (string name in MainMenuNames)
					{
						isDoneTransition[name] = false;	
					}
					
					foreach (string name in ContinueMenuNames)
					{
						isDoneTransition[name] = false;	
					}
					
					// disable the texts that causes transitions
					disableTranslationTexts();
					
					fromSubMenu = (int)MenuState.ContinueOptions;
				}
				
				// transitioning from Mode Options
				if (fromSubMenu == (int)MenuState.ModeOptions)
				{
					bool hideModeDone = hideModeOptions();
					bool showMainMenuOptionsDone = showMainMenuOptions();
				
					transitionDone = hideModeDone && showMainMenuOptionsDone;
				}
				else if (fromSubMenu == (int)MenuState.ContinueOptions)
				{
					bool hideContinueDone = hideContinueOptions();
					bool showMainMenuOptionsDone = showMainMenuOptions();
					
					transitionDone = hideContinueDone && showMainMenuOptionsDone;
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
						isDoneTransition[name] = false;	
					}
					
					foreach (string name in ModeMenuNames)
					{
						isDoneTransition[name] = false;	
					}
					
					// disable the texts that causes transitions					
					disableTranslationTexts();
				}
				
				bool showModeDone = showModeOptions();
				bool hideMainMenuOptionsDone = hideMainMenuOptions();
				
				transitionDone = showModeDone && hideMainMenuOptionsDone;
			}
			else if (display == (int)MenuState.ContinueOptions)
			{
				Debug.Log ("Go back to Continue Options");
				
				if (display != last)
				{
					// to begin transition process
					foreach (string name in MainMenuNames)
					{
						isDoneTransition[name] = false;	
					}
					
					foreach (string name in ContinueMenuNames)
					{
						isDoneTransition[name] = false;	
					}
					
					// disable the texts that causes transitions					
					disableTranslationTexts();
				}
				
				bool showContinueDone = showContinueOptions();
				bool hideMainMenuOptionsDone = hideMainMenuOptions();
				
				transitionDone = showContinueDone && hideMainMenuOptionsDone;
			}
			else 
			{
				Debug.LogError ("GameMenu reached an unrecognized menu state");		
			}
		}
		
		if (transitionDone)
		{
			// re-enable texts that causes transitions		
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
			
			foreach (TextScript ts in ContinueMenuOptionScripts)
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
		float modeMenuBaseSpeed = 12.0f;
		textIndex = 0;
		foreach (TextScript ts in ModeMenuOptionScripts)
		{
			ts.setDistance(Mathf.Abs(DisplayCol1Anchor.position.x - ModeMenuOptions[textIndex].transform.position.x));
			ts.setSpeed(modeMenuBaseSpeed);
			
			modeMenuBaseSpeed = modeMenuBaseSpeed - 1.0f;
			textIndex++;
		}
		
		float continueMenuBaseSpeed1 = 15.0f;
		float continueMenuBaseSpeed2 = 12.0f;
		float continueMenuBaseSpeed3 = 9.0f;
		textIndex = 0;
		foreach (TextScript ts in ContinueMenuOptionScripts)
		{
			if (ts.columnNumber == 1)
			{
				ts.setDistance(Mathf.Abs (DisplayCol1Anchor.position.x - ContinueMenuOptions[textIndex].transform.position.x));
				ts.setSpeed (continueMenuBaseSpeed1);
				continueMenuBaseSpeed1 = continueMenuBaseSpeed1 - 1.0f;
			}
			else if (ts.columnNumber == 2)
			{
				float distance = Mathf.Abs (DisplayCol2Anchor.position.x - ContinueMenuOptions[textIndex].transform.position.x);
				ts.setDistance(distance);
				ts.setSpeed (continueMenuBaseSpeed2);
				continueMenuBaseSpeed2 = continueMenuBaseSpeed2 - 1.0f;				
			}
			else if (ts.columnNumber == 3)
			{
				float origX = 0.0f;
				
				for (int i = 0 ; i < ContinueMenuOptions.Length ; i++)
				{
					if (ts.textName.Equals(ContinueMenuOptions[i].GetComponent<TextScript>().textName))
					{
						origX = ContinueMenuOptions[i].transform.position.x;
						break;
					}
				}
				
				
				float distance = Mathf.Abs (DisplayCol3Anchor.position.x - origX);
				ts.setDistance(distance);
				ts.setSpeed (continueMenuBaseSpeed3);
				continueMenuBaseSpeed3 = continueMenuBaseSpeed3 - 1.0f;
			}
			else
			{
				Debug.LogWarning("GameMenu.cs: Invalid column number received");
			}
			
			textIndex++;
		}
	}
	
	private void disableTranslationTexts()
	{
		// re-enable texts that causes transitions		
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
		
		foreach (TextScript ts in ContinueMenuOptionScripts)
		{
			if (ts.isTransitionText)
				ts.enabled = false;
		}	
	}
		
	private void initIsDoneTransition ()
	{
		isDoneTransition = new Dictionary<string, bool>();
		
		foreach (TextScript ts in MainMenuOptionScripts)
		{
			MainMenuNames.Add (ts.textName);
			isDoneTransition.Add (ts.textName, true);
		}
		
		foreach (TextScript ts in ContinueMenuOptionScripts)
		{
			ContinueMenuNames.Add (ts.textName);
			isDoneTransition.Add (ts.textName, true);	
		}
		
		foreach (TextScript ts in ModeMenuOptionScripts)
		{
			ModeMenuNames.Add (ts.textName);
			isDoneTransition.Add (ts.textName, true);
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
			if (!isDoneTransition[name])
			{
				isDoneTransition[name] = MainMenuOptionScripts[index].translateText(showDirection);
				ret = ret && isDoneTransition[name];
			}
			
			index++;
		}
		
		return ret;
	}
	
	public bool hideMainMenuOptions ()
	{
		Vector3 hideDirection = new Vector3(-1,0,0);
		bool ret = true;
		
		int index = 0;
		foreach (string name in MainMenuNames)
		{
			if (!isDoneTransition[name])
			{
				isDoneTransition[name] = MainMenuOptionScripts[index].translateText(hideDirection);
				ret = ret && isDoneTransition[name];
			}
			
			index++;
		}
		
		return ret;
	}
	
	public bool showModeOptions ()
	{
		Vector3 showDirection = new Vector3(-1,0,0);
		bool ret = true;
		
		int index = 0;
		foreach (string name in ModeMenuNames)
		{
			if (!isDoneTransition[name])
			{
				isDoneTransition[name] = ModeMenuOptionScripts[index].translateText(showDirection);
				ret = ret && isDoneTransition[name];
			}
			
			index++;
		}
		
		return ret;
	}
	
	public bool hideModeOptions ()
	{
		Vector3 hideDirection = new Vector3(1,0,0);
		bool ret = true;
		
		int index = 0;
		foreach (string name in ModeMenuNames)
		{
			if (!isDoneTransition[name])
			{
				isDoneTransition[name] = ModeMenuOptionScripts[index].translateText(hideDirection);
				ret = ret && isDoneTransition[name];
			}
			
			index++;
		}
		
		return ret;
	}
	
	public bool showContinueOptions ()
	{
		Vector3 showDirection = new Vector3(-1,0,0);
		bool ret = true;
		
		int index = 0;
		foreach (string name in ContinueMenuNames)
		{
			if (!isDoneTransition[name])
			{
				isDoneTransition[name] = ContinueMenuOptionScripts[index].translateText(showDirection);
				ret = ret && isDoneTransition[name];
			}
			
			index++;
		}
		
		return ret;
	}
	
	public bool hideContinueOptions ()
	{
		Vector3 hideDirection = new Vector3(1,0,0);
		bool ret = true;
		
		int index = 0;
		foreach (string name in ContinueMenuNames)
		{
			if (!isDoneTransition[name])
			{
				isDoneTransition[name] = ContinueMenuOptionScripts[index].translateText(hideDirection);
				ret = ret && isDoneTransition[name];
			}
			
			index++;
		}
		
		return ret;
	}
}
