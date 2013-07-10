using UnityEngine;
using System.Collections;

public class MaceScript : MonoBehaviour {
	
	public float timer;
	public float swingSpeed;
	
	// Use this for initialization
	void Start () {
		timer = 0;
		swingSpeed = 1.5f;
		
		gameObject.transform.eulerAngles = new Vector3(
			gameObject.transform.eulerAngles.x - 65,
			gameObject.transform.eulerAngles.y,
			gameObject.transform.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void FixedUpdate() {
		// Swinging maces will be moving passively; Player has no control
		timer += Time.deltaTime;
		
		if(timer >= 2.0)
		{
			timer = 0;
			swingSpeed = swingSpeed * -1.0f;
		}
		gameObject.transform.Rotate(swingSpeed, 0.0f, 0.0f);
	}
}
