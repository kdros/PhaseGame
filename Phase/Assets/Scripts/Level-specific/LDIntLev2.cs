using UnityEngine;
using System.Collections;

public class LDIntLev2 : LevelDirector 
{
	public GameObject[] boulders;
	public Transform boulderSpawn;
	
	MainPlayerScript player;
	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < 3; i ++)
		{
			GameObject boulder = Instantiate (boulders [i], boulderSpawn.position, Quaternion.identity) as GameObject;
			boulder.transform.localScale = new Vector3 (2f, 2f, 2f);
		}
		
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<MainPlayerScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public override bool OnEventTrigger (string colliderName)
	{
		switch (colliderName)
		{
		case "Level_End":
			GameObject.FindGameObjectWithTag ("Director").GetComponent<Director>().MoveToNextLevel ();
			return true;
		case "ResetSpeed":
			player.ResetSpeed ();
			return false;
		}
		return false;
	}
}
