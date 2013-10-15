using UnityEngine;
using System.Collections;

// BackText inherits TextScript and is responsible for defining the behavior of the back text in the game menu
public class BackText : TextScript 
{
	// Go back to the main menu if the back text is clicked
	public override void respondToClick ()
	{
		if (this.enabled)
		{
			//Debug.Log("need to switch to mode menu");
			GameObject gm = GameObject.FindGameObjectWithTag ("GameMenu");
			GameMenu gameMenu = gm.GetComponent<GameMenu>();
			gameMenu.toMainMenu();
		}
	}
}
