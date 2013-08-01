using UnityEngine;
using System.Collections;

public class GameMenuScript : MonoBehaviour {

	private GUIStyle buttonStyle;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		GUILayout.BeginArea (new Rect(10,Screen.height/2 + 100,Screen.width-10, 200));
		
		//Load the main scene
		//The scene needs to be added into build setting to be loaded!
		//if(GUILayout.Button("Instructions"))
		//{
			//Debug.Log ("No Instructions Yet!");
			//Application.LoadLevel("");
		//}
		if(GUILayout.Button("Let's Play!"))
		{
			Application.LoadLevel(2);
		}
		if(GUILayout.Button("Beginner Level 1"))
		{
			Application.LoadLevel(2);
		}
		if(GUILayout.Button("Beginner Level 2"))
		{
			Application.LoadLevel(3);
		}
		if(GUILayout.Button("Beginner Level 3"))
		{
			Application.LoadLevel(4);
		}
		if(GUILayout.Button("Beginner Level 4"))
		{
			Application.LoadLevel(5);
		}
		if(GUILayout.Button("Intermediate Level 1"))
		{
			Application.LoadLevel(6);
		}
		if(GUILayout.Button("Intermediate Level 2"))
		{
			Application.LoadLevel(7);
		}
		if(GUILayout.Button("Intermediate Level 3"))
		{
			Application.LoadLevel(8);
		}
		if(GUILayout.Button("Exit"))
		{
			Application.Quit ();
				Debug.Log ("Application.Quit() only works in build, not in editor");
		}
		GUILayout.EndArea();
	}

}
