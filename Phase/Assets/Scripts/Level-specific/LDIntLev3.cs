using UnityEngine;
using System.Collections;

public class LDIntLev3 : LevelDirector {

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
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
