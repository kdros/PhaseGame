using UnityEngine;
using System.Collections;

// Used in the main menu. Rotate the camera with skybox slowly.
public class SkyboxCam : MonoBehaviour 
{
	public float degreesPerSecond;

	// Use this for initialization
	void Start () 
	{
		if (degreesPerSecond == 0)
			degreesPerSecond = 10;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(Vector2.up, degreesPerSecond * Time.deltaTime);
	}
}
