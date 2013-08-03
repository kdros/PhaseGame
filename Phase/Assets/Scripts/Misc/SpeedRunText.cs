using UnityEngine;
using System.Collections;

public class SpeedRunText : TextScript
{
	public override virtual void respondToClick()
	{
		PlayerPrefs.SetString ("GameMode", "SpeedRun");
	}
}
