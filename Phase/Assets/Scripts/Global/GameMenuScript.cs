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
		
		if(GUILayout.Button("Let's Play!"))
		{
			Application.LoadLevel("BeginnerLevel1Scene");
		}
		if(GUILayout.Button("Instructions"))
		{
			//Application.LoadLevel("");
		}
		if(GUILayout.Button("Exit"))
		{
			Application.Quit ();
				Debug.Log ("Application.Quit() only works in build, not in editor");
		}
		GUILayout.EndArea();
	}

}
