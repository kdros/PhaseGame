using UnityEngine;
using System.Collections;

public class SpeedRunText : TextScript
{
	private GameMenu gm;
	
	void Start ()
	{	
		gm = GameObject.FindGameObjectWithTag("GameMenu").GetComponent<GameMenu>();
	}

	public override void respondToClick()
	{
		Debug.Log ("Set as speed run");
		PlayerPrefs.SetString (Constants.GameModeKey, Constants.GameModeSpeedRun);
		selectText ();
		gm.unselectNormalModeText();
	}
}
