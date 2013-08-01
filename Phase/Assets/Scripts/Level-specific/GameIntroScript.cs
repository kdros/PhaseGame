using UnityEngine;
using System.Collections;

public class GameIntroScript : MonoBehaviour {
	
	public float timer;
	
	// Use this for initialization
	void Start ()
	{
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		
		if (timer >= 0.5f)
			Application.LoadLevel(1); // Loads GameMenu scene
	}
}
