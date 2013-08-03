using UnityEngine;
using System.Collections;

public class SpeedRunText : TextScript
{
	public virtual void respondToClick()
	{
		PlayerPrefs.SetString ("GameMode", "SpeedRun");
	}
}
