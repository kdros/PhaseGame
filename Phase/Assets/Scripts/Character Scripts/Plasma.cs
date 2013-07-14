using UnityEngine;
using System.Collections;

public class Plasma : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI ()
	{
		Event e = Event.current;
		switch (e.keyCode)
		{
		case KeyCode.LeftArrow: 
			break;
		case KeyCode.RightArrow: 
			break;
		case KeyCode.UpArrow: 
			break;
/*		case KeyCode.DownArrow: 
			break;
		case KeyCode.LeftArrow: 
			break;		*/
		}
	}
}
