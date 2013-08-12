using UnityEngine;
using System.Collections;

public class NormalText : TextScript
{
	private GameMenu gm;
	
	void Start ()
	{	
		gm = GameObject.FindGameObjectWithTag("GameMenu").GetComponent<GameMenu>();
	}
	
	public override void respondToClick()
	{
		PlayerPrefs.SetString (Constants.GameModeKey,Constants.GameModeNormal);
		selectText();
		gm.unselectSpeedRunModeText();
	}
}
