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
		if(GUILayout.Button("Instructions"))
		{
			Debug.Log ("No Instructions Yet!");
			//Application.LoadLevel("");
		}
		if(GUILayout.Button("Let's Play!"))
		{
			Application.LoadLevel("BeginnerLevel1Scene");
		}
		if(GUILayout.Button("Beginner Level 1"))
		{
			Application.LoadLevel("BeginnerLevel1Scene");
		}
		if(GUILayout.Button("Beginner Level 2"))
		{
			Application.LoadLevel("BeginnerLevel2");
		}
		if(GUILayout.Button("Beginner Level 3"))
		{
			Application.LoadLevel("BeginnerLevel3");
		}
		if(GUILayout.Button("Beginner Level 4"))
		{
			Application.LoadLevel("BeginnerLevel4");
		}
		if(GUILayout.Button("Exit"))
		{
			Application.Quit ();
				Debug.Log ("Application.Quit() only works in build, not in editor");
		}
		GUILayout.EndArea();
	}

}
