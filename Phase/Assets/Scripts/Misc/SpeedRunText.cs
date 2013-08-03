using UnityEngine;
using System.Collections;

public class SpeedRunText : TextScript
{
	public override void respondToClick()
	{
		PlayerPrefs.SetString ("GameMode", "SpeedRun");
	}
}
