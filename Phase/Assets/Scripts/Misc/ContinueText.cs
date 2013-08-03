using UnityEngine;
using System.Collections;

public class ContinueText : TextScript {
	
	public override void respondToClick ()
	{
		if (this.enabled)
		{
			Debug.Log("need to switch to continue menu");
			GameObject gm = GameObject.FindGameObjectWithTag ("GameMenu");
			GameMenu gameMenu = gm.GetComponent<GameMenu>();
			gameMenu.toContinueOptions();
		}
	}
	
}
