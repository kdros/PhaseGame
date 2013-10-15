using UnityEngine;
using System.Collections;

// ModeText defines the behavior of the mode text in the main menu
public class ModeText : TextScript 
{
	// Go to the mode selection submenu in the main menu
	public override void respondToClick ()
	{
		if (this.enabled)
		{
			//Debug.Log("need to switch to mode menu");
			GameObject gm = GameObject.FindGameObjectWithTag ("GameMenu");
			GameMenu gameMenu = gm.GetComponent<GameMenu>();
			gameMenu.toModeOptions();
		}
	}
}
