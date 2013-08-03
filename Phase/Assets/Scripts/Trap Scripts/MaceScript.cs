using UnityEngine;
using System.Collections;

public class MaceScript : MonoBehaviour {
	
	public float timer = 0;
	public float swingSpeed = 1.0f;
	public float maxAngle = 45f;
	
	float curTimer;
	// Use this for initialization
	void Start () {
//		timer;
//		swingSpeed;
		if (swingSpeed == 0f)
			swingSpeed = 1f;
		if (maxAngle == 0f)
			maxAngle = 45f;
		curTimer = 0f;
//		gameObject.transform.eulerAngles = new Vector3(
//			gameObject.transform.eulerAngles.x - 45,
//			gameObject.transform.eulerAngles.y,
//			gameObject.transform.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void FixedUpdate() 
	{
		// Swinging maces will be moving passively; Player has no control
		curTimer += Time.deltaTime;
		
		if(curTimer >= timer)
		{
			if ((gameObject.transform.localEulerAngles.x > maxAngle) && (gameObject.transform.localEulerAngles.x < (360f-maxAngle)))
				swingSpeed = swingSpeed * -1.0f;
			gameObject.transform.Rotate(swingSpeed, 0.0f, 0.0f);
		}
	}
}
