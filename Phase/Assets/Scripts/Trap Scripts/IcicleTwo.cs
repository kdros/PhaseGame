using UnityEngine;
using System.Collections;

public class IcicleTwo : MonoBehaviour 
{
	public enum IcicleType { Cone, Cube, Cylinder}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	public void detachIcicle (IcicleType iceType)
	{
		Transform childObj = gameObject.transform;
		switch (iceType)
		{
		case IcicleType.Cone:
			childObj = childObj.Find ("IcicleCone");
			break;
		case IcicleType.Cube:
			childObj = childObj.Find ("IcicleCube");
			break;
		case IcicleType.Cylinder:
			childObj = childObj.Find ("IcicleCylinder");
			break;
		}
		
		childObj.parent = null;
		childObj.rigidbody.useGravity = true;
		childObj.rigidbody.isKinematic = false;
		childObj.rigidbody.AddForce (Vector3.down * 100f);
	}
}
