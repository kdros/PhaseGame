using UnityEngine;
using System.Collections;

public class Global_BegLev1 : MonoBehaviour {
	
	//public AudioClip backgroundMusic;
	
	void Start ()
	{
		if (System.IO.File.Exists ("Save/currentSave"))
			System.IO.File.Delete ("Save/currentSave");
		
		// Play background music (Audio source is set to loop in inspector)
		//AudioSource.PlayClipAtPoint(backgroundMusic, gameObject.transform.position);
	}
	
	
	void Update ()
	{
		//if(!audio.isPlaying)
		//{
		//	Debug.Log ("Sound IS NOT PLAYING");		
		//	AudioSource.PlayClipAtPoint(backgroundMusic, gameObject.transform.position);
		//}
	}
}
