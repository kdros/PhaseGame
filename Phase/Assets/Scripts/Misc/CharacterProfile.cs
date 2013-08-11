using UnityEngine;
using System.Collections;

public class CharacterProfile : MonoBehaviour {
	
	// Character profile textures
	public Texture2D blackBackground;
	public Texture2D mattyDescription;
	public Texture2D liquidDescription;
	public Texture2D solidDescription;
	public Texture2D gasDescription;
	public Texture2D plasmaDescription;
	
	bool mattyProfile = false;
	bool liquidProfile = false;
	bool solidProfile = false;
	bool gasProfile = false;
	bool plasmaProfile = false;
	
	float one = 1f;
	
	// Use this for initialization
	void Start () 
	{
		mattyProfile = true;
		liquidProfile = false;
		solidProfile = false;
		gasProfile = false;
		plasmaProfile = false;

	}
	
	// Update is called once per frame
	void Update () 
	{	

	}
		
	void OnGUI ()
	{
		if (mattyProfile)
		{
			loadMattyProfile ();
		}
		if (liquidProfile)
		{
			loadLiquidProfile ();
		}
		if (solidProfile)
		{
			loadSolidProfile ();
		}
		if (gasProfile)
		{
			loadGasProfile ();
		}
		if (plasmaProfile)
		{
			loadPlasmaProfile ();
		}
	}
	
	void loadMattyProfile()
	{

		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), mattyDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Liquid"))
		{
			MattyProfile();
			LiquidProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Solid"))
		{
			MattyProfile();
			SolidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Gas"))
		{
			MattyProfile();
			GasProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Plasma"))
		{
			MattyProfile();
			PlasmaProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			MattyProfile();
			Application.LoadLevel(1);
		}
		
		GUI.EndGroup();	
	}
	
	void loadLiquidProfile()
	{
		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), liquidDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Matty"))
		{
			LiquidProfile();
			MattyProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Solid"))
		{
			LiquidProfile();
			SolidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Gas"))
		{
			LiquidProfile();
			GasProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Plasma"))
		{
			LiquidProfile();
			PlasmaProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			LiquidProfile();
			Application.LoadLevel(1);
		}
		
		GUI.EndGroup();
		
	}
	
	void loadSolidProfile()
	{
		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), solidDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Matty"))
		{
			SolidProfile();
			MattyProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Liquid"))
		{
			SolidProfile();
			LiquidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Gas"))
		{
			SolidProfile();
			GasProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Plasma"))
		{
			SolidProfile();
			PlasmaProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			SolidProfile();
			Application.LoadLevel(1);
		}
		
		GUI.EndGroup();
		
	}
	
	void loadGasProfile()
	{
		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), gasDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Matty"))
		{
			GasProfile();
			MattyProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Liquid"))
		{
			GasProfile();
			LiquidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Solid"))
		{
			GasProfile();
			SolidProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Plasma"))
		{
			GasProfile();
			PlasmaProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			GasProfile();
			Application.LoadLevel(1);
		}
		
		GUI.EndGroup();
		
	}
	
	void loadPlasmaProfile()
	{
		Time.timeScale = 0.0f;
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 300, 0, Screen.width, Screen.height), plasmaDescription);
		
		if(GUI.Button(new Rect(55, 30, 95, 30), "Matty"))
		{
			PlasmaProfile();
			MattyProfile();
		}
		if(GUI.Button(new Rect(155, 30, 95, 30), "Liquid"))
		{
			PlasmaProfile();
			LiquidProfile();
		}
		if(GUI.Button(new Rect(255, 30, 95, 30), "Solid"))
		{
			PlasmaProfile();
			SolidProfile();
		}
		if(GUI.Button(new Rect(355, 30, 95, 30), "Gas"))
		{
			PlasmaProfile();
			GasProfile();
		}
		if(GUI.Button(new Rect(455, 30, 95, 30), "BACK"))
		{
			PlasmaProfile();
			Application.LoadLevel(1);
		}
		
		GUI.EndGroup();
		
	}
	
	void MattyProfile()
	{
		mattyProfile = !mattyProfile;
		one *= -1f;
		Time.timeScale += one;
	}
	
	void LiquidProfile()
	{
		liquidProfile = !liquidProfile;
		one *= -1f;
		Time.timeScale += one;
	}
	
	void SolidProfile()
	{
		solidProfile = !solidProfile;
		one *= -1f;
		Time.timeScale += one;
	}
	
	void GasProfile()
	{
		gasProfile = !gasProfile;
		one *= -1f;
		Time.timeScale += one;
	}
	
	void PlasmaProfile()
	{
		plasmaProfile = !plasmaProfile;
		one *= -1f;
		Time.timeScale += one;
	}
}
