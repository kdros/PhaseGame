using UnityEngine;
using System.Collections;

public class NormalText : TextScript
{
	public override void respondToClick()
	{
		PlayerPrefs.SetString ("GameMode","Normal");
	}
}