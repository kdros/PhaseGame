using UnityEngine;
using System.Collections;

public class MotorScript : MonoBehaviour 
{
	float speed;
	CharacterController charControl;
	// Use this for initialization
	void Start () 
	{
		speed = 3.0f;
		charControl = GetComponent<CharacterController>(); 	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float sidewaysMove = Input.GetAxis ("Horizontal") * speed;
		float verticalMove = Input.GetAxis ("Vertical") * speed;
		Vector3 forwardMove	= transform.TransformDirection (Vector3.forward) * sidewaysMove;
		Vector3 upwardMove	= transform.TransformDirection (Vector3.up) * verticalMove;
		charControl.Move (forwardMove+upwardMove);
	}
	
	void OnControllerColliderHit (ControllerColliderHit hittingObj)
	{
		Collider collCollider = hittingObj.collider;
		//hittingObj.gameObject.tag
		if (hittingObj.gameObject.name == "Plane")
			Physics.IgnoreCollision (GetComponent<Collider>(), collCollider);
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
