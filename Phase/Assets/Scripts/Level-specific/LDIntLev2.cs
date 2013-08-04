using UnityEngine;
using System.Collections;

public class LDIntLev2 : LevelDirector 
{

	public override bool OnEventTrigger (string triggerName)
	{
		switch (triggerName)
		{
			case "Level_End":
				GameObject.Find ("Director").GetComponent<Director>().MoveToNextLevel ();
			return true;
		}
		
		return false;				
	}
	
	public override bool OnEventTrigger (string triggerName, string colliderName)
	{
		return false;
	}
}
