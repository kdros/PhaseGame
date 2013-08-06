using UnityEngine;
using System.Collections;

public class LoadLevelText : TextScript
{
	public bool newGameButton;
	public int LevelToLoad;
	public override void respondToClick ()
	{
		int startLevel = LevelToLoad;
		
		if (newGameButton)
			PlayerPrefs.SetInt(Constants.LevelToLoadKey, Constants.introSceneIndex);
			
		Application.LoadLevel(LevelToLoad);
	}
}
