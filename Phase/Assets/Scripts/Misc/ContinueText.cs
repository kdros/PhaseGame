using UnityEngine;
using System.Collections;

// ContinueText inherits TextScript and is responsible for defining the behavior of the continue text in the game menu
public class ContinueText : TextScript 
{
	// Move the menu to the continue options
	public override void respondToClick ()
	{
		if (this.enabled)
		{
			//Debug.Log("need to switch to continue menu");
			GameObject gm = GameObject.FindGameObjectWithTag ("GameMenu");
			GameMenu gameMenu = gm.GetComponent<GameMenu>();
			gameMenu.toContinueOptions();
		}
	}
}
