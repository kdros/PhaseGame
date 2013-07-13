using UnityEngine;
using System.Collections;

public class MattySolidScript : MonoBehaviour {

	public float timer;
	public float stopForceTime;
	public float facingRight_yDegrees;
	public float facingLeft_yDegrees;
	
	void Start () {
		timer = 0;
		facingRight_yDegrees = gameObject.transform.eulerAngles.y;
		facingLeft_yDegrees = gameObject.transform.eulerAngles.y + 180;
		
		// Set Idle and Walk to loop
	   gameObject.animation["Idle"].wrapMode = WrapMode.Loop;
	   gameObject.animation["Walk"].wrapMode = WrapMode.Loop;
	   
	   // Set Jump to only play once
	   gameObject.animation["Jump"].wrapMode = WrapMode.Once;
	   
	   gameObject.animation.Play("Idle", PlayMode.StopAll);
	}

	void Update () {
	}

	void FixedUpdate() {
		timer += Time.deltaTime;
		
		if(Input.GetKeyDown("up"))
	 	{
	  		gameObject.animation.CrossFade("Jump");
	 	}
	 	if(Input.GetKeyDown("right"))
	 	{
			stopForceTime = timer;
			gameObject.animation.CrossFade("Walk");
			
			// Make sure Matty is facing to the right
			gameObject.transform.eulerAngles = new Vector3(
			gameObject.transform.eulerAngles.x,
			facingRight_yDegrees,
			gameObject.transform.eulerAngles.z);
	  		
			gameObject.rigidbody.AddForce (15.0f,0.0f,0.0f);
	 	}
		if(Input.GetKeyDown("left"))
	 	{
			stopForceTime = timer;
			gameObject.animation.CrossFade("Walk");
			
			gameObject.transform.eulerAngles = new Vector3(
			gameObject.transform.eulerAngles.x,
			facingLeft_yDegrees,
			gameObject.transform.eulerAngles.z);
			
			gameObject.rigidbody.AddForce (-15.0f, 0.0f,0.0f);
	 	}
		
		if(timer > (stopForceTime + 1.2f))
		{
			//gameObject.animation.CrossFade("Idle");
			gameObject.rigidbody.velocity = Vector3.zero;
			timer = 0;
			stopForceTime = 0;
		}
	}
}
