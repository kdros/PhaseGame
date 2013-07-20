using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour 
{
	public struct DarkCave
	{
		public Vector2 Door1;
		public Vector2 Door2;
	};
	
	Color originalAmbientColor;
	bool darkCave;
	
	System.Collections.Generic.List<IcicleBase> iciclesList;
	System.Collections.Generic.List<DarkCave> darkCavesList;
	
	// Use this for initialization
	void Start () 
	{
		if (System.IO.File.Exists ("Save/currentSave"))
			System.IO.File.Delete ("Save/currentSave");
		
		originalAmbientColor = RenderSettings.ambientLight;
		
		iciclesList = new System.Collections.Generic.List<IcicleBase> ();
		GameObject[] icicleBaseArray = GameObject.FindGameObjectsWithTag ("IceCeiling");
		foreach (GameObject icicle in icicleBaseArray)
			iciclesList.Add (icicle.GetComponent<IcicleBase>());
		
//		if (fadeUntoBlack == 0f)
//			fadeUntoBlack = 3f;
		darkCave = false;
		darkCavesList = new System.Collections.Generic.List<DarkCave>();
		icicleBaseArray = GameObject.FindGameObjectsWithTag ("DarkCave");
		foreach (GameObject darkCaveEnterX in icicleBaseArray)
		{
			DarkCave newCave;
			Transform dcTransform = darkCaveEnterX.GetComponent<Transform>();
			newCave.Door1 = dcTransform.Find ("DarkCaveEnter").position;
			newCave.Door2 = dcTransform.Find ("DarkCaveExit").position;
			darkCavesList.Add (newCave);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
