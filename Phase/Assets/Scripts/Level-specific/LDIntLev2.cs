using UnityEngine;
using System.Collections;

public class LDIntLev2 : LevelDirector 
{
	public GameObject[] boulders;
	public GameObject 	blockBoulder;
	
	public Transform boulderSpawn;
	public Transform breakPformPos;
	public Transform blockBoulderPos;
	
	Vector3 blockBoulderPosition;
	Vector3 blockBoulderScale;
	Quaternion blockBoulderRotation;
	
	MainPlayerScript player;
	System.Collections.Generic.List<GameObject> instantiatedBoulders;
	
	// Use this for initialization
	void Start () 
		
	{
		instantiatedBoulders = new System.Collections.Generic.List<GameObject>();
		for (int i = 0; i < 3; i ++)
		{
			GameObject boulder = Instantiate (boulders [i], boulderSpawn.position, Quaternion.identity) as GameObject;
			boulder.transform.localScale = new Vector3 (2f, 2f, 2f);
			instantiatedBoulders.Add (boulder);
		}
		
//		Instantiate (breakPform, breakPformPos.position, Quaternion.identity);
//		Instantiate (blockBoulder, blockBoulderPos.position, Quaternion.identity);
		blockBoulderPosition = new Vector3 (blockBoulderPos.position.x, blockBoulderPos.position.y, 
											blockBoulderPos.position.z);
		blockBoulderRotation = new Quaternion (blockBoulderPos.rotation.x, blockBoulderPos.rotation.y, 
											   blockBoulderPos.rotation.z, blockBoulderPos.rotation.w);
		blockBoulderScale = new Vector3 (blockBoulderPos.localScale.x, blockBoulderPos.localScale.y, 
										 blockBoulderPos.localScale.z);
		
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<MainPlayerScript>();
	}
	
	public void ResetPuzzle () 
	{
		bool reset = true;
		if (breakPformPos.childCount > 0)
		{	
			foreach (Transform platformChild in breakPformPos)
				if (!platformChild.rigidbody.isKinematic)
				{
					reset = false;
					break;
				}
		}
		else
			reset = false;
		
		if (reset)
		{
			reset = false;
			for (int i = 0; i < instantiatedBoulders.Count; i ++)
				if (instantiatedBoulders [i] == null)
				{	
					reset = true;
					break;
				}
			
			if (reset)
			{
				if (blockBoulderPos != null)
					Destroy (blockBoulderPos.gameObject);
				for (int i = 0; i < instantiatedBoulders.Count; i ++)
				{
					if (instantiatedBoulders [i] != null)	
						Destroy (instantiatedBoulders [i]);
					else 								// instantiatedBoulders [i] pretends to be null. See below for details.
						instantiatedBoulders [i] = null; 
				}
				instantiatedBoulders.Clear ();	// Reference to all destroyed instantiatedBoulders [i] removed,
												// it will be garbage collected at some point from now.
				// See http://answers.unity3d.com/questions/377510/does-destroy-remove-all-instances.html for details.
				
				
				GameObject temp = Instantiate (blockBoulder, blockBoulderPosition, Quaternion.identity) as GameObject;
				temp.transform.rotation = blockBoulderRotation;
				temp.transform.localScale = blockBoulderScale;
				blockBoulderPos = temp.transform; // Reference to destroyed blockBoulder removed; it will be 
				for (int i = 0; i < 3; i ++)	  // garbage collected sometime from now.	
				{
					GameObject boulder = Instantiate (boulders [i], boulderSpawn.position, Quaternion.identity) as GameObject;
					boulder.transform.localScale = new Vector3 (2f, 2f, 2f);
					instantiatedBoulders.Add (boulder);
				}
			}
		}
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
		case "PLAYER_DEAD":
			ResetPuzzle ();
			return true;
		}
		return false;
	}
}
