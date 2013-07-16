using UnityEngine;
using System.Collections;

public class IcicleTwoFloorCollider : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	void OnTriggerEnter (Collider collObj) 
	{
		string aName = gameObject.name;
		if (collObj.CompareTag ("Player"))
		{
			IcicleTwo.IcicleType type = new IcicleTwo.IcicleType ();
			if (aName == "IcicleFloorColliderCone")
				type = IcicleTwo.IcicleType.Cone;
			else if (aName == "IcicleFloorColliderCube")
				type = IcicleTwo.IcicleType.Cube;
			else if (aName == "IcicleFloorColliderCylinder")
				type = IcicleTwo.IcicleType.Cylinder;		
			
			if (gameObject.transform.parent != null)
			{
				gameObject.transform.parent.GetComponent<IcicleTwo>().detachIcicle (type);
				gameObject.transform.parent = null;
			}
		}
//		Debug.Log ("Buzzz!: "+aName);
	}
}
