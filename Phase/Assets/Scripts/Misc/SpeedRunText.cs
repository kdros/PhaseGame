using UnityEngine;
using System.Collections;

public class SpeedRunText : TextScript
{
	public override void respondToClick()
	{
		Debug.Log ("Set as speed run");
		PlayerPrefs.SetString (Constants.GameModeKey, Constants.GameModeSpeedRun);
	}
}
