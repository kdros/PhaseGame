using UnityEngine;
using System.Collections;

public class LDBegLev3 : LevelDirector
{
	public Transform boulderSpawn;
	public GameObject boulder;
	
	public override void OnEventTrigger (string triggerName)
	{
		switch (triggerName)
		{
		case "BegLev3_EventTrigger1":
			for (int i = 0; i < 4; i++)
				Invoke ("makeRockLeftDown", 1+i);
			break;
		case "BegLev3_EventTrigger2":
			GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>().DisplayMessage 
				("Falling Boulders! TAKE COVER!");
			break;
		case "BegLev3_EventTrigger3":
			GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>().DisplayMessage 
				("Whew.. That was close!");
			break;
		}
	}
	
	void makeRockLeftDown ()
	{
		GameObject aRock = Instantiate (boulder, boulderSpawn.position, Quaternion.identity) as GameObject;
		aRock.rigidbody.AddForce (Vector3.left*500f);
		aRock.rigidbody.AddForce (Vector3.down*500f);
	}
}
