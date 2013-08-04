using UnityEngine;
using System.Collections;

public class LoadLevelText : TextScript
{
	public int LevelToLoad;
	
	public override void respondToClick ()
	{
		int startLevel = LevelToLoad;		
		PlayerPrefs.SetInt("LevelToLoad",LevelToLoad);
		Application.LoadLevel(LevelToLoad);		
	}
}
