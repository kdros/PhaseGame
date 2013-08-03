using UnityEngine;
using System.Collections;

public class ExitText : TextScript 
{
	public override void respondToClick()
	{
		Application.Quit();	
	}
}
