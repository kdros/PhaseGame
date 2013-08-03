using UnityEngine;
using System.Collections;

public class NormalText : TextScript
{
	public virtual void respondToClick()
	{
		PlayerPrefs.SetString ("GameMode","Normal");
	}
}
