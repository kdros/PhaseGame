using UnityEngine;
using System.Collections;

public class LDBegLev4 : LevelDirector 
{
	public Transform boulderSpawn;
	public GameObject boulder;
	float curTime;
	// Use this for initialization
	void Start () 
	{
		curTime = 0f;	
	}
	
	// Update is called once per frame
	void Update () 
	{
//		curTime += Time.deltaTime;
//		if (curTime > 0.5f)
//		{
//			GameObject aRock = Instantiate (boulder, boulderSpawn.position, Quaternion.identity) as GameObject;
//			aRock.rigidbody.AddForce (Vector3.down*100f);
//			curTime = 0f;
//		}
	
	}
	
	public override bool OnEventTrigger (string triggerName)
	{
		switch (triggerName)
		{
		case "Level_End":
			GameObject.Find ("Director").GetComponent<Director>().MoveToNextLevel ();
//			Physics.IgnoreCollision (
		return true;
		}
		
		return false;				
	}
	
	public override bool OnEventTrigger (string triggerName, string colliderName)
	{
		return false;
	}
}
