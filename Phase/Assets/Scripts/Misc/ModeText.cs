using UnityEngine;
using System.Collections;

public class ModeText : TextScript 
{
	public override void respondToClick ()
	{
		if (this.enabled)
		{
			Debug.Log("need to switch to mode menu");
			GameObject gm = GameObject.FindGameObjectWithTag ("GameMenu");
			GameMenu gameMenu = gm.GetComponent<GameMenu>();
			gameMenu.toModeOptions();
		}
	}
}
