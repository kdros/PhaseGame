using UnityEngine;
using System.Collections;

public class NormalText : TextScript
{
	public override virtual void respondToClick()
	{
		PlayerPrefs.SetString ("GameMode","Normal");
	}
}
