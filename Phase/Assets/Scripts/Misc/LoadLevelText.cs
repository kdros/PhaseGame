using UnityEngine;
using System.Collections;

// LoadLevelText defines the behavior of the  in the main menu
public class LoadLevelText : TextScript
{
	public bool newGameButton;
	public int LevelToLoad;
	
	// load the user selected level
	public override void respondToClick ()
	{
		if (newGameButton)
			PlayerPrefs.SetInt(Constants.LevelToLoadKey, Constants.introSceneIndex);
			
		Application.LoadLevel(LevelToLoad);
	}
}
