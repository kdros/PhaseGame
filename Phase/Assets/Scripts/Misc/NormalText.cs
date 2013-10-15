using UnityEngine;
using System.Collections;


// NormalText defines the behavior of the normal text in the main menu
public class NormalText : TextScript
{
	private GameMenu gm;
	
	void Start ()
	{	
		gm = GameObject.FindGameObjectWithTag("GameMenu").GetComponent<GameMenu>();
	}
	
	// Switch the current game mode to normal mode
	public override void respondToClick()
	{
		PlayerPrefs.SetString (Constants.GameModeKey,Constants.GameModeNormal);
		selectText();
		gm.unselectSpeedRunModeText();
	}
}
