using UnityEngine;
using System.Collections;

public class LDIntro : LevelDirector 
{
	Director dir;
	GameObject trigger;
	
	float curTime = 0f, displayTime = 3f;
	string displayMessage, displayOnKeyPressMessage;
	bool timedDisplay = false, displayOnKey = false;
	KeyCode onKey;
	
	void Start ()
	{
		dir = GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>();
		trigger = GameObject.Find ("Messages");
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
					DisplayOnKeyPress ("Excellent! Now continue to the next beacon.", KeyCode.Q);
				}
				else if (onKey == KeyCode.Alpha2)
				{
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
			return true;
		case "Intro_Message2":
			dir.ShowTriggerText (triggerN);
			SetTimedDisplay ("");
			return true;
		case "Intro_Message3":
			dir.ShowTriggerText (triggerN);
			SetTimedDisplay ("Press 1 to zoom out.");
			DisplayOnKeyPress ("Good. Now Press Q to zoom in.", KeyCode.Alpha1);
			return true;
		case "Intro_Message4":
			dir.ShowTriggerText (triggerN);
			SetTimedDisplay ("Press 2 to increase height.");
			DisplayOnKeyPress ("That's it! Now Press W to decrease it.", KeyCode.Alpha2);
			return true;
		case "Intro_Message5":
			dir.ShowTriggerText (triggerN);
			return true;
		case "Intro_Message6":
			dir.ShowTriggerText (triggerN);
			return true;
		case "Level_End":
			dir.MoveToNextLevel ();
			break;
		}
		
		return false;
	}
}
