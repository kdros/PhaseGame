using UnityEngine;
using System.Collections;

public class Global_BegLev1 : MonoBehaviour {
	
	public float timer;
	public GameObject PressS;
	public GameObject PressS_Bkgd;
	
	void Start ()
	{
		timer = 0.0f;
		
		if (System.IO.File.Exists ("Save/currentSave"))
			System.IO.File.Delete ("Save/currentSave");
	
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if (timer > 1.75f)
		{
			Destroy(PressS);
			Destroy(PressS_Bkgd);
		}
	}
}
