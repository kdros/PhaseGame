using UnityEngine;
using System.Collections;

public class LDBegLev3 : LevelDirector
{
	public Transform boulderSpawn;
	public GameObject boulder;
	
	Director dir;
	
	void Start ()
	{
		dir = GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>();
	}
	
	public override bool OnEventTrigger (string triggerName)
	{
		switch (triggerName)
		{
		case "BegLev3_EventTrigger1":
			for (int i = 0; i < 4; i++)
				Invoke ("makeRockLeftDown", 1+i);
			return true;
		case "BegLev3_EventTrigger2":
			dir.DisplayMessage ("Falling Boulders! TAKE COVER!");
			return true;
		case "BegLev3_EventTrigger3":
			GameObject trigger = GameObject.Find ("EventTriggers");
			Transform triggerT = trigger.transform.Find ("BegLev3_EventTrigger4");
			if (triggerT.gameObject.activeSelf == false)
			{
				dir.DisplayMessage ("That seems to have done the trick!");
				return true;
			}
			else
				return false;
		case "BegLev3_EventTrigger4":
			int boulderCount = 0;
			GameObject[] boulders = GameObject.FindGameObjectsWithTag ("FallingBoulders");
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			foreach (GameObject rock in boulders)
				if ((rock.transform.position.y >= player.transform.position.y-1.5f) &&
					(rock.transform.position.y <= player.transform.position.y+1.0f))
					boulderCount ++;
			if (boulderCount == 4)
			{
				dir.DisplayMessage ("Whew.. That was close! Oh no, it seems the boulders have blocked our path. " +
					"Could they be destroyed by extreme heat from the lava?");
				return true;
			}
			
			return false;
			
		case "Level_End":
			dir.MoveToNextLevel ();
			return true;
		}
		
		return false;
	}
	
	void makeRockLeftDown ()
	{
		GameObject aRock = Instantiate (boulder, boulderSpawn.position, Quaternion.identity) as GameObject;
		aRock.rigidbody.AddForce (Vector3.left*400f);
		aRock.rigidbody.AddForce (Vector3.down*100f);
	}
}
