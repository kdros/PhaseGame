using UnityEngine;
using System.Collections;

public class LDIntro : LevelDirector 
{
	Director dir;
	GameObject trigger;
	GameObject[] triggerList;
	
	float curTime = 0f, displayTime = 3f;
	string displayMessage, displayOnKeyPressMessage;
	bool timedDisplay = false, displayOnKey = false;
	KeyCode onKey;
	
	// To indicate which trigger (in triggerList order) the player has crossed.
	int triggerCrossed = -1;
	
	void Start ()
	{
		dir = GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>();
		trigger = GameObject.Find ("Messages");
		triggerList = new GameObject [trigger.transform.childCount + 1];
		int  i = 0;
		foreach (Transform child in trigger.transform)
		{
			switch (child.name)
			{
			case "Intro_Message1":
				i = 0;
				break;
			case "Intro_Message2":
				i = 1;
				break;
			case "Intro_Message6":
				i = 2;
				break;
			case "Intro_Message3":
				i = 3;
				break;
			case "Intro_Message4":
				i = 4;
				break;
			case "Intro_Message5":
				i = 5;
				break;
			case "Intro_Message7":
				i = 6;
				break;	
			}
			// Store each trigger in the order they appear in game.
			triggerList [i] = child.gameObject;
		}
		// And finally, store the Level_End trigger.
		triggerList [trigger.transform.childCount] = GameObject.Find ("Level_End");
		
		// Set all beacons other than the first one inactive.
		for (i = 1; i < triggerList.Length; i ++)
			triggerList [i].SetActive (false);
		
		// Set the previously crossed trigger to -1 (invalid).
		triggerCrossed = -1;
		timedDisplay = false;
		displayOnKey = false;
		
		MainPlayerScript mpRef = GameObject.FindGameObjectWithTag ("Player").GetComponent<MainPlayerScript>();
		mpRef.CanChange (MainPlayerScript.State.Solid, false);
		mpRef.CanChange (MainPlayerScript.State.Liquid, false);
		mpRef.CanChange (MainPlayerScript.State.Plasma, false);
		mpRef.CanChange (MainPlayerScript.State.Gas, false);
	}
	
	void Update ()
	{
		if (timedDisplay)
		{
			curTime += Time.deltaTime;
			if (curTime > displayTime)
			{
				curTime = 0f;
				dir.DisplayMessage (displayMessage);
				timedDisplay = false;
			}
		}
		
		// Set the next beacon active.
		if (!timedDisplay && !displayOnKey)
			if (triggerCrossed > -1)
				if (!triggerList [triggerCrossed].activeSelf)
					// If previous trigger is inactive, set current one to active.
					triggerList [triggerCrossed + 1].SetActive (true);
		
		if ((triggerCrossed + 1) == 6)
			triggerCrossed = 6;
	}
	
	void SetTimedDisplay (string message, float time = -1f)
	{
		timedDisplay = true;
		displayMessage = message;
		if (time > 0f)
			displayTime = time;
	}
	
	void DisplayOnKeyPress (string message, KeyCode key)
	{
		displayOnKeyPressMessage = message;
		onKey = key;
		displayOnKey = true;
	}
		
	void OnGUI ()
	{
		Event current = Event.current;
		if (current.isKey)
		{
			if ((current.keyCode == onKey) && displayOnKey)
			{
				dir.DisplayMessage (displayOnKeyPressMessage);
				displayOnKey = false;
				if (onKey == KeyCode.Alpha1)
				{
					if (timedDisplay)
					{
						timedDisplay = false;
						displayMessage = "";
					}
					DisplayOnKeyPress ("Excellent! Now continue to the next beacon.", KeyCode.Q);
				}
				else if (onKey == KeyCode.Alpha2)
				{
					if (timedDisplay)
					{
						timedDisplay = false;
						displayMessage = "";
					}
					DisplayOnKeyPress ("Great going! Now continue to the next beacon.", KeyCode.W);
				}
			}
		}
	}
	
	public override bool OnEventTrigger (string triggerN)
	{
		switch (triggerN)
		{
		case "Intro_Message1":
			dir.ShowTriggerText (triggerN);
			SetTimedDisplay ("Use the arrow keys to move left or right. Try it now. Go touch the beacon up ahead.");
			triggerCrossed = 0;
			return true;
		case "Intro_Message2":
			dir.ShowTriggerText (triggerN);
			triggerCrossed = 1;
			return true;
		case "Intro_Message3":
			dir.ShowTriggerText (triggerN);
			SetTimedDisplay ("Press 1 to zoom out.");
			DisplayOnKeyPress ("Good. Now Press Q to zoom in.", KeyCode.Alpha1);
			triggerCrossed = 3;
			return true;
		case "Intro_Message4":
			dir.ShowTriggerText (triggerN);
			SetTimedDisplay ("Press 2 to increase height.");
			DisplayOnKeyPress ("That's it! Now Press W to decrease it.", KeyCode.Alpha2);
			triggerCrossed = 4;
			return true;
		case "Intro_Message5":
			dir.ShowTriggerText (triggerN);
			triggerCrossed = 5;
			return true;
		case "Intro_Message6":
			dir.ShowTriggerText (triggerN);
			triggerCrossed = 2;
			return true;
		case "Level_End":
			foreach (GameObject someObj in triggerList)
				someObj.SetActive (false);
			dir.MoveToNextLevel ();
			break;
		}
		
		return false;
	}
}
