using UnityEngine;
using System.Collections;

public class MattyScript : MonoBehaviour {
	
	public float timer;
	public float stopForceTime;
	public float facingRight_yDegrees;
	public float facingLeft_yDegrees;
	
	void Start ()
	{
		timer = 0;
		
		// Set the correct directions for right and left
		facingRight_yDegrees = gameObject.transform.eulerAngles.y;
		facingLeft_yDegrees = gameObject.transform.eulerAngles.y + 180;
		
		// Set Idle and Walk to loop
		gameObject.animation["Walk"].wrapMode = WrapMode.Loop;
		gameObject.animation["Idle"].wrapMode = WrapMode.Loop;
	   
		// Set Jump to only play once
		gameObject.animation["Jump"].wrapMode = WrapMode.Once;
	   
		// Set start motion to Idle
		gameObject.animation.Play("Idle", PlayMode.StopAll);
	}

	void Update ()
	{
	}

	void FixedUpdate()
	{
		if(timer > (stopForceTime + 1.0f))
		{
			gameObject.rigidbody.velocity = Vector3.zero;
			timer = 0;
			stopForceTime = 0;
		}
	}
	
	void OnGUI ()
	{
		timer += Time.deltaTime;
		
		Event e = Event.current;
		switch (e.keyCode)
		{
			case KeyCode.LeftArrow:
			{
				stopForceTime = timer;
				gameObject.animation.CrossFade("Walk");
				
				gameObject.transform.eulerAngles = new Vector3(
				gameObject.transform.eulerAngles.x,
				facingLeft_yDegrees,
				gameObject.transform.eulerAngles.z);
				
				gameObject.rigidbody.AddForce (-15.0f, 0.0f,0.0f);
				break;
			}
			case KeyCode.RightArrow:
			{
				stopForceTime = timer;
				gameObject.animation.CrossFade("Walk");
				
				// Make sure Matty is facing to the right
				gameObject.transform.eulerAngles = new Vector3(
				gameObject.transform.eulerAngles.x,
				facingRight_yDegrees,
				gameObject.transform.eulerAngles.z);
		  		
				gameObject.rigidbody.AddForce (15.0f,0.0f,0.0f);
				break;
			}
			case KeyCode.UpArrow:
			{
				gameObject.animation.CrossFade("Jump");
				break;
			}
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
		Collider collider = collision.collider;
		
		if(collider.CompareTag ("Wall"))
		{
			Debug.Log ("Collided with Wall");
			gameObject.rigidbody.velocity = Vector3.zero;
		}
	}
}
