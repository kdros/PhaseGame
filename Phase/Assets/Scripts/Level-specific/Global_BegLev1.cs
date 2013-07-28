using UnityEngine;
using System.Collections;

public class Global_BegLev1 : LevelDirector {
	
	public float timer;
	
	void Start ()
	{
		timer = 0.0f;
		
//		if (System.IO.File.Exists ("Save/currentSave"))
//			System.IO.File.Delete ("Save/currentSave");
	
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
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
}
