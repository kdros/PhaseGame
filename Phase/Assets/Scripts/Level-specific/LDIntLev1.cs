using UnityEngine;
using System.Collections;

public class LDIntLev1 : LevelDirector {
	
	public Transform boulderSpawn1;
	public Transform boulderSpawn2;
	public Transform boulderSpawn3;
	public Transform boulderSpawn4;
	public GameObject boulder;
	bool bouldersCanSpawn;
	public float timer;
	

	Director dir;
	
	void Start ()
	{
		dir = GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>();
		
		bouldersCanSpawn = false;
		timer = 0.0f;
	}
	
	void Update ()
	{
		timer += Time.deltaTime;
		if ((bouldersCanSpawn == true) && (timer >= 0.5f))
		{
    		Instantiate (boulder, boulderSpawn1.position, Quaternion.identity);
			Instantiate (boulder, boulderSpawn2.position, Quaternion.identity);
			Instantiate (boulder, boulderSpawn3.position, Quaternion.identity);
			Instantiate (boulder, boulderSpawn4.position, Quaternion.identity);
			timer = 0.0f;
		}
			
	}
	
	public override bool OnEventTrigger (string triggerName)
	{
		switch (triggerName)
		{
			case "IntLev1_EventTrigger1":
				Debug.Log ("First trigger hit!");
				bouldersCanSpawn = true;
				return false;
			case "IntLev1_EventTrigger2":
				Debug.Log ("Second trigger hit!");
				if(bouldersCanSpawn == false)
					bouldersCanSpawn = true;
				else
					bouldersCanSpawn = false;
			
				return false;
			case "Level_End":
				dir.MoveToNextLevel ();
				return true;
		}
		return false;
	}
}
