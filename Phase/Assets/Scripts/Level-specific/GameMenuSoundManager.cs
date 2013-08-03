using UnityEngine;
using System.Collections;

public class GameMenuSoundManager : MonoBehaviour {
	
	public AudioClip hoverSound;
	public AudioClip clickSound;
	
	// Use this for initialization
	void Start () 
	{
		GameObject[] texts = GameObject.FindGameObjectsWithTag("MenuText");
		
		for (int i = 0 ; i < texts.Length ; i++)
		{
			TextScript ts = texts[i].GetComponent<TextScript>();
			ts.hoverSound = hoverSound;
			ts.clickSound = clickSound;
		}		
	}
}
