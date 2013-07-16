using UnityEngine;
using System.Collections;

public class MotorScript : MonoBehaviour 
{
	public float lavaMaxTime = 10f;
	
	float speed, jumpSpeed;
	InteractiveCloth clothManip;
	ParticleSystem	embers;
	Light glow;
	ClothRenderer clothRenderer;
	CharacterController charControl;
	Color originalColour, origAmbient;
	
	Vector3 center, r;
	bool grounded, isOnLava;
	float curTime;
	
//	CharacterController charControl;
	// Use this for initialization
	void Start () 
	{
		speed = 5.0f;
		jumpSpeed = 50f;//25000f;
		clothManip = GetComponent<InteractiveCloth>();
		clothRenderer = GetComponent<ClothRenderer>();
		embers = GetComponent<ParticleSystem>();
		charControl = GetComponent<CharacterController>();
		
		embers.Stop ();
		
		curTime = 0f; lavaMaxTime = 10f;
		center = transform.position;
		Vector3 centerPlane = transform.position + new Vector3 (0, 0, transform.localScale [2]/2.0f);
		grounded = isOnLava = true;
//		float radius = vertices [0].
//		int i = 0;
//		for (i = 0; i < vertices.Length; i ++)
//			if (vertices [i].x == center.x)
////				if (vertices [i].y == center.y)
//					break;
//		Vector3 centerPlane = center;
//		centerPlane.x = vertices [i].x;
		r = center - centerPlane;
		glow = transform.Find ("Glow").GetComponent<Light>();
		originalColour = glow.color;
		//		center = GetComponent<MeshFilter>().mesh.		 	
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
//		rigidbody.AddForce (forwardMove);
//		if ((grounded) && verticalMove > 0)
//			rigidbody.AddForce (Vector3.up*jumpSpeed*8f);
//		rigidbody.AddTorque (forwardTorque*sidewaysMove);
		charControl.SimpleMove (forwardMove);
		if ((grounded) && verticalMove > 0)
			charControl.SimpleMove (Vector3.up*jumpSpeed*8f);
		clothManip.pressure = Mathf.Abs (sidewaysMove / speed) * 50.0f;
		
		Color lightColour = glow.color;
		if (curTime > 0f)
		{
			if (isOnLava)
			{
				if (curTime < lavaMaxTime)
					curTime += Time.deltaTime;
				else
					curTime = lavaMaxTime;
			}
			else
				curTime -= Time.deltaTime;
			
//			lightColour = Color.Lerp (lightColour, Color.red, curTime/lavaMaxTime);
			glow.color = Color.Lerp (originalColour, Color.red, curTime/lavaMaxTime);
			clothRenderer.material.SetColor ("_Color", glow.color);//.Lerp (originalMat, LavaMat, curTime/lavaMaxTime);
		}
		else
		{
			curTime = 0f;
//			if (!lightColour.Equals (originalColour))
//				lightColour = originalColour;
			if (!glow.color.Equals (originalColour))
				glow.color = originalColour;
			if (!clothRenderer.material.GetColor ("_Color").Equals (glow.color))
				clothRenderer.material.SetColor ("_Color", glow.color);
		}
		
//		glow.color = lightColour;
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
//		else if (hittingObj.gameObject.CompareTag ("Lava"))
//		{
//			embers.loop = true;
//			embers.Play ();
//			curTime += Time.deltaTime;
//			isOnLava = true;
//		}
		else if (hittingObj.gameObject.CompareTag ("Grate"))
		{
			//rigidbody.AddForce (new Vector3 (0f, -1000f, 0f));
//			transform.Translate (new Vector3 (0f, -5f, 0f));
			Physics.IgnoreCollision (GetComponent<Collider>(), collCollider);
			Debug.Log ("You die too easily!");
		}
	}
		
	void OnCollisionExit (Collision hittingObj)
	{
		Collider collCollider = hittingObj.collider;
		if (hittingObj.gameObject.CompareTag ("Ground"))
			grounded = false;
//		else if (hittingObj.gameObject.CompareTag ("Lava"))
//		{
//			embers.loop = false;
//			isOnLava = false;
//		}
	}
	
	void OnTriggerEnter (Collider collCollider)
	{
//		Collider collCollider = hittingObj.collider;
		
		if (collCollider.CompareTag ("Lava"))
		{
			embers.loop = true;
			embers.Play ();
			curTime += Time.deltaTime;
			isOnLava = true;
		}
		
		else if (collCollider.CompareTag ("DarkCaveEnter"))
		{
			origAmbient = RenderSettings.ambientLight;
			RenderSettings.ambientLight = Color.black;
		}
		
		else if (collCollider.CompareTag ("DarkCaveExit"))
		{
			RenderSettings.ambientLight = origAmbient;
		}
	}
		
	void OnTriggerExit (Collider collCollider)
	{
//		Collider collCollider = hittingObj.collider;
		if (collCollider.CompareTag ("Lava"))
		{
			embers.loop = false;
			isOnLava = false;
		}
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
