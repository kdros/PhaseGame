using UnityEngine;
using System.Collections;

// SpeedRunText defines the behavior of the speed run text in the main menu
public class SpeedRunText : TextScript
{
	private GameMenu gm;
	
	void Start ()
	{	
		gm = GameObject.FindGameObjectWithTag("GameMenu").GetComponent<GameMenu>();
	}
	
	// set the current game mode to speed run mode.
	public override void respondToClick()
	{
		//Debug.Log ("Set as speed run");
		PlayerPrefs.SetString (Constants.GameModeKey, Constants.GameModeSpeedRun);
		selectText ();
		gm.unselectNormalModeText();
	}
}
