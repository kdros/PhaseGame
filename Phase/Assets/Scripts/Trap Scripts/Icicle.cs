using UnityEngine;
using System.Collections;

public class Icicle : IcicleBase 
{
//	public enum IcicleType { Cone, Cube, Cylinder}
//	Vector3[] particles;
	// Use this for initialization
	void Start () 
	{
		particles = new Vector3 [3];
		foreach (Transform child in transform)
		{
			if (child.name.Equals ("pCone1"))
				particles [0] = child.transform.localPosition;
			else if (child.name.Equals ("pCube1"))
				particles [1] = child.transform.localPosition;
			else if (child.name.Equals ("pCylinder1"))
				particles [2] = child.transform.localPosition;
		}
	
	}
	
	public override void detachIcicle (IcicleBase.IcicleType iceType)
	{
		Transform childObj = gameObject.transform;
		switch (iceType)
		{
		case IcicleBase.IcicleType.Cone:
			childObj = childObj.Find ("pCone1");
			break;
		case IcicleBase.IcicleType.Cube:
			childObj = childObj.Find ("pCube1");
			break;
		case IcicleBase.IcicleType.Cylinder:
			childObj = childObj.Find ("pCylinder1");
			break;
		}
		
//		childObj.parent = null;
		childObj.rigidbody.useGravity = true;
		childObj.rigidbody.isKinematic = false;
		childObj.rigidbody.AddForce (Vector3.down * 100f);
	}
	
	public override void Reset ()
	{
		foreach (Transform child in transform)
		{
			if (child.name.Equals ("pCone1"))
				child.transform.localPosition = particles [0];
			else if (child.name.Equals ("pCube1"))
				child.transform.localPosition = particles [1];
			else if (child.name.Equals ("pCylinder1"))
				child.transform.localPosition = particles [2];
		}
	}
}
