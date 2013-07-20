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
			IcicleBase.IcicleType type = new IcicleBase.IcicleType ();
			if (aName == "IcicleFloorColliderCone")
				type = IcicleBase.IcicleType.Cone;
			else if (aName == "IcicleFloorColliderCube")
				type = IcicleBase.IcicleType.Cube;
			else if (aName == "IcicleFloorColliderCylinder")
				type = IcicleBase.IcicleType.Cylinder;		
			
			if (gameObject.transform.parent != null)
			{
				gameObject.transform.parent.GetComponent<IcicleBase>().detachIcicle (type);
//				gameObject.transform.parent = null;
			}
		}
//		Debug.Log ("Buzzz!: "+aName);
	}
}
