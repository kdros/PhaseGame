using UnityEngine;
using System.Collections;

public class Global_BegLev1 : MonoBehaviour {
	
	public AudioClip jumpSound;
	
	void Start ()
	{
		if (System.IO.File.Exists ("Save/currentSave"))
			System.IO.File.Delete ("Save/currentSave");
	
	}
	
	
	void Update ()
	{

	}
}
