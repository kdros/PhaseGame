using UnityEngine;
using System.Collections;

// Main script for setting up the character profile scene.
public class CharacterProfile : MonoBehaviour 
{
	// Character profile textures
	public Texture2D blackBackground;
	public Texture2D mattyDescription;
	public Texture2D liquidDescription;
	public Texture2D solidDescription;
	public Texture2D gasDescription;
	public Texture2D plasmaDescription;
	
	public Font menuFont;
	
	bool mattyProfile = false;
	bool liquidProfile = false;
	bool solidProfile = false;
	bool gasProfile = false;
	bool plasmaProfile = false;
	
	float one = 1f;
	
	// Use this for initialization
	void Start () 
	{
		//Time.timeScale = one;
		mattyProfile = true;
		liquidProfile = false;
		solidProfile = false;
		gasProfile = false;
		plasmaProfile = false;

	}

	// Display gui information depending on which character profile is currently active
	void OnGUI ()
	{
		GUIStyle newStyle = new GUIStyle ();		
		newStyle = GUI.skin.button;
		newStyle.font = menuFont;
		newStyle.alignment = TextAnchor.MiddleCenter;
		GUI.skin.button = newStyle;
		
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
	
	// Load default matty profile
	void loadMattyProfile()
	{
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
	
	// load liquid matty profile
	void loadLiquidProfile()
	{
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
	
	// load solid matty profile
	void loadSolidProfile()
	{
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
	
	// load gas matty profile
	void loadGasProfile()
	{		
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
	
	// load plasma matty profile
	void loadPlasmaProfile()
	{
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
	
	// Switches for the current state of the GUI
	void MattyProfile()
	{
		mattyProfile = !mattyProfile;
	}
	
	void LiquidProfile()
	{
		liquidProfile = !liquidProfile;
	}
	
	void SolidProfile()
	{
		solidProfile = !solidProfile;
	}
	
	void GasProfile()
	{
		gasProfile = !gasProfile;
	}
	
	void PlasmaProfile()
	{
		plasmaProfile = !plasmaProfile;
	}
}
