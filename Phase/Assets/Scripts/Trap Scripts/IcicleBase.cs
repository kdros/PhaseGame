using UnityEngine;
using System.Collections;

public class IcicleBase : MonoBehaviour 
{
	public enum IcicleType { Cone, Cube, Cylinder}
	protected Vector3[] particles;
	
	public virtual void detachIcicle (IcicleType iceType)
	{	;	}
	public virtual void Reset ()
	{	;	}
}

