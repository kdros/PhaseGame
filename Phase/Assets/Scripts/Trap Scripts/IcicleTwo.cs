using UnityEngine;
using System.Collections;

public class IcicleTwo : IcicleBase 
{
//	public enum IcicleType { Cone, Cube, Cylinder}
	
//	Vector3[] particles;
	// Use this for initialization
	void Start () 
	{
		particles = new Vector3 [3];
		foreach (Transform child in transform)
		{
			if (child.name.Equals ("IcicleCone"))
				particles [0] = child.transform.localPosition;
			else if (child.name.Equals ("IcicleCube"))
				particles [1] = child.transform.localPosition;
			else if (child.name.Equals ("IcicleCylinder"))
				particles [2] = child.transform.localPosition;
		}
	}
	
	public override void detachIcicle (IcicleType iceType)
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
		
//		childObj.parent = null;
		childObj.rigidbody.useGravity = true;
		childObj.rigidbody.isKinematic = false;
		childObj.rigidbody.AddForce (Vector3.down * 100f);
	}
	
	public override void Reset ()
	{
		foreach (Transform child in transform)
		{
			if (child.name.Equals ("IcicleCone"))
				child.transform.localPosition = particles [0];
			else if (child.name.Equals ("IcicleCube"))
				child.transform.localPosition = particles [1];
			else if (child.name.Equals ("IcicleCylinder"))
				child.transform.localPosition = particles [2];
		}
	}
}
