using UnityEngine;
using System.Collections;

public class MotorScript : MonoBehaviour 
{
	float speed, jumpSpeed;
	InteractiveCloth clothManip;
	Vector3 center, r;
	bool grounded;
//	CharacterController charControl;
	// Use this for initialization
	void Start () 
	{
		speed = 40.0f;
		jumpSpeed = 500f;//25000f;
		clothManip = GetComponent<InteractiveCloth>();
		center = transform.position;
		Vector3 centerPlane = transform.position + new Vector3 (0, 0, transform.localScale [2]/2.0f);
		grounded = true;
//		float radius = vertices [0].
//		int i = 0;
//		for (i = 0; i < vertices.Length; i ++)
//			if (vertices [i].x == center.x)
////				if (vertices [i].y == center.y)
//					break;
//		Vector3 centerPlane = center;
//		centerPlane.x = vertices [i].x;
		r = center - centerPlane;
		
		//		center = GetComponent<MeshFilter>().mesh.
//		charControl = GetComponent<CharacterController>(); 	
	}
	
	void FixedUpdate () 
	{
		float sidewaysMove = Input.GetAxis ("Horizontal") * speed;
		float verticalMove = Input.GetAxis ("Vertical");
		Vector3 forwardMove	= Vector3.forward * sidewaysMove;
		Vector3 upwardMove	= Vector3.up * verticalMove;
		Vector3 forwardTorque = Vector3.forward;
		forwardTorque = Quaternion.AngleAxis (-45f, Vector3.right)*forwardTorque;
		//		charControl.Move (forwardMove+upwardMove);
		forwardTorque = Vector3.Cross (r, forwardTorque);
//		if (grounded)
//		{
			rigidbody.AddForce (forwardMove);
		if ((grounded) && verticalMove > 0)
			rigidbody.AddForce (Vector3.up*jumpSpeed*16f);
			rigidbody.AddTorque (forwardTorque*sidewaysMove);
			clothManip.pressure = Mathf.Abs (sidewaysMove / speed) * 2.0f;
//		}
	}
	
//	void OnControllerColliderHit (ControllerColliderHit hittingObj)
	void OnCollisionEnter (Collision hittingObj)
	{
		Collider collCollider = hittingObj.collider;
		if (hittingObj.gameObject.CompareTag ("Ground"))
			grounded = true;
		else if (hittingObj.gameObject.CompareTag ("Icicle"))
		{	
			Debug.Log ("You die too easily!");
		}
	}
		
	void OnCollisionExit (Collision hittingObj)
	{
		Collider collCollider = hittingObj.collider;
		if (hittingObj.gameObject.CompareTag ("Ground"))
			grounded = false;
	}	
	
//	void OnGUI ()
//	{
//		Event anEvent = Event.current;
//		if (anEvent.isKey)
//		{
//			switch (anEvent.keyCode)
//			{
//			case KeyCode.LeftArrow:
//				break;
//			case KeyCode.RightArrow:
//				break;
//			case KeyCode.UpArrow:
//				break;
//			}
//		}
//	}
}
