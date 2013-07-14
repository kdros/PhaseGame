using UnityEngine;
using System.Collections;

public class Icicle : MonoBehaviour 
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
			childObj = childObj.Find ("pCone1");
			break;
		case IcicleType.Cube:
			childObj = childObj.Find ("pCube1");
			break;
		case IcicleType.Cylinder:
			childObj = childObj.Find ("pCylinder1");
			break;
		}
		
		childObj.parent = null;
		childObj.rigidbody.useGravity = true;
		childObj.rigidbody.AddForce (Vector3.down * 10f);
	}
}
