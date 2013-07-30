using UnityEngine;
using System.Collections;

public class LDBegLev2 : LevelDirector 
{
	Director dir;
	// Use this for initialization
	void Start () 
	{
		dir = GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>();
		MainPlayerScript mpRef = GameObject.FindGameObjectWithTag ("Player").GetComponent<MainPlayerScript>();

		mpRef.CanChange (MainPlayerScript.State.Plasma, false);
		mpRef.CanChange (MainPlayerScript.State.Gas, false);
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
			dir.MoveToNextLevel ();
			return true;
		}
		return false;
	}
}
