using UnityEngine;
using System.Collections;

public class IcicleFloorCollider : MonoBehaviour 
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
			Icicle.IcicleType type = new Icicle.IcicleType ();
			if (aName == "IcicleFloorColliderCone")
				type = Icicle.IcicleType.Cone;
			else if (aName == "IcicleFloorColliderCube")
				type = Icicle.IcicleType.Cube;
			else if (aName == "IcicleFloorColliderCylinder")
				type = Icicle.IcicleType.Cylinder;		
			
			if (gameObject.transform.parent != null)
			{
				gameObject.transform.parent.GetComponent<Icicle>().detachIcicle (type);
				gameObject.transform.parent = null;
			}
		}
//		Debug.Log ("Buzzz!: "+aName);
	}
}
