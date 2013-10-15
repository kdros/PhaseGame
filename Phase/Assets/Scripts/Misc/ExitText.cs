using UnityEngine;
using System.Collections;

// ExitText defines the behavior of the exit text in the main menu
public class ExitText : TextScript 
{
	// Quit game
	public override void respondToClick()
	{
		Application.Quit();	
	}
}
