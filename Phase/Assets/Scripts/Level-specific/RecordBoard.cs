using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecordBoard : MonoBehaviour {
	
	public Transform HideAnchorRight;
	public Transform HideAnchorLeft;
	public Transform DisplayAnchor;
	
	public GameObject backButton;
	public GameObject nextButton;
	
	public GameObject baseText;
	public float spacing;
	
	private TextScript backButtonTextScript;
	private TextScript nextButtonTextScript;
	
	private bool transitionDone;
	private int totalLevels;
	private int currentLevel; 								// level ordering (1~7)
	private int last;										// keep track of the current level that is being displayed and the last
	private int display;									// and the last level that was displayed
	
	private List<GameObject[]> allTexts;					// each entry of the list represents all records for a level
	private List<TextScript[]> allTextScripts;
	private List<string[]> allTextNames;					// used as keys of isTransitionDone
	private Dictionary<string,bool> isTransitionDone;
	private RecordManager recordManager;
	
	// Use this for initialization
	void Start () 
	{
		transitionDone = true;
		totalLevels = Constants.lastPlayableSceneIndex - Constants.introSceneIndex;	
		recordManager = gameObject.GetComponent("RecordManager") as RecordManager;
		currentLevel = 1;
		last = currentLevel;
		display = last;
		
		if (spacing == 0)
			spacing = 0.5f;
		
		backButtonTextScript = backButton.GetComponent("TextScript") as TextScript;
		nextButtonTextScript = nextButton.GetComponent("TextScript") as TextScript;
		
		instantiateTexts();
		initTextScripts();
		initTransitionValues();
		initIsDoneTransition();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (display != last || !transitionDone)
		{
			// want to display previous level records
			if (display < currentLevel)
			{
				// to begin transition process
				if (display != last)
				{
					for (int i = currentLevel - 2 ; i < currentLevel ; i++)
					{
						for (int j = 0 ; j < Constants.numRecordEntries + 1 ; j++)
						{
							string name = allTextNames[i][j];
							isTransitionDone[name] = false;
						}
					}
					
					nextButtonTextScript.enabled = false;
					backButtonTextScript.enabled = false;
				}
				
				transitionDone = toPrevious();	
			}
			else if (display > currentLevel)
			{
				// to begin transition process
				if (display != last)
				{
					for (int i = currentLevel - 1 ; i < currentLevel + 1 ; i++)
					{
						for (int j = 0 ; j < Constants.numRecordEntries + 1 ; j++)
						{
							string name = allTextNames[i][j];
							isTransitionDone[name] = false;
						}
					}
					
					nextButtonTextScript.enabled = false;
					backButtonTextScript.enabled = false;
				}
				
				transitionDone = toNext();								
			}			
		}
		
		if (transitionDone)
		{
			currentLevel = display;
			nextButtonTextScript.enabled = true;
			backButtonTextScript.enabled = true;
		}
		
		last = display;
	}
	
	private void initIsDoneTransition()
	{
		isTransitionDone = new Dictionary<string, bool>();
		allTextNames = new List<string[]>();
		
		for (int i = 0 ; i < totalLevels ; i++)
		{
			allTextNames.Add (new string[Constants.numRecordEntries + 1]);
			for (int j = 0 ; j < Constants.numRecordEntries + 1 ; j++)
			{
				allTextNames[i][j] = allTextScripts[i][j].textName;
				isTransitionDone.Add (allTextScripts[i][j].textName, true);
			}
		}	
	}
	
	private void initTextScripts()
	{
		allTextScripts = new List<TextScript[]>();
		
		for (int i = 0 ; i < totalLevels ; i++)
		{
			allTextScripts.Add (new TextScript[Constants.numRecordEntries + 1]);
			for (int j = 0 ; j < Constants.numRecordEntries + 1 ; j++)
			{
				allTextScripts[i][j] = allTexts[i][j].GetComponent("TextScript") as TextScript;
			}
		}
	}
	
	// set up speed and distances for each text
	private void initTransitionValues()
	{
		for (int i = 0 ; i < allTextScripts.Count ; i++)
		{
			float baseSpeed = 40.0f;
			for (int j = 0 ; j < allTextScripts[i].Length ; j++)
			{
				TextScript ts = allTextScripts[i][j];
				ts.setSpeed(baseSpeed);
				baseSpeed = baseSpeed - 2.0f;
				ts.setDistance(Mathf.Abs (HideAnchorRight.transform.position.x - DisplayAnchor.transform.position.x));
				int lev = i+1;
				ts.textName = "level"+lev.ToString()+"rec"+j.ToString();
			}
		}
	}
	
	void instantiateTexts()
	{
		allTexts = new List<GameObject[]>();
		
		// first instantiate the texts that will be displayed		
		for (int i = 0 ; i < totalLevels ; i++)
		{
			allTexts.Add (new GameObject[Constants.numRecordEntries + 1]);
			List<string> texts = recordManager.getLevelRecords(i+Constants.introSceneIndex+1);
			
			for (int j = 0 ; j < Constants.numRecordEntries + 1; j++)
			{
				// display the first level's records
				if (i == 0)
				{
					// level text
					if (j == 0)
					{
						GameObject level = Instantiate (baseText, DisplayAnchor.transform.position, Quaternion.identity) as GameObject;
						level.transform.tag = Constants.menuTextItemTag;
						TextMesh levelText = level.GetComponent("TextMesh") as TextMesh;
						int l = i + 1;
						levelText.text = "Level "+l.ToString();
						allTexts[i][j] = level;
					}	
					else
					{
						Vector3 position = DisplayAnchor.transform.position;
						position = position - new Vector3(0, j*spacing, 0);
						GameObject record = Instantiate (baseText, position, Quaternion.identity) as GameObject;
						record.transform.tag = Constants.menuTextItemTag;
						TextMesh recorddText = record.GetComponent("TextMesh") as TextMesh;
						recorddText.text = texts[j-1];
						allTexts[i][j] = record;
					}
				}
				// 
				else
				{
					// level text
					if (j == 0)
					{
						GameObject level = Instantiate (baseText, HideAnchorRight.transform.position, Quaternion.identity) as GameObject;
						level.transform.tag = Constants.menuTextItemTag;
						TextMesh levelText = level.GetComponent("TextMesh") as TextMesh;
						int l = i + 1;
						if (l == totalLevels)	
							levelText.text = "Final";
						else
							levelText.text = "Level "+l.ToString();
						
						allTexts[i][j] = level;
					}	
					else
					{
						Vector3 position = HideAnchorRight.transform.position;
						position = position - new Vector3(0, j*spacing, 0);
						GameObject record = Instantiate (baseText, position, Quaternion.identity) as GameObject;
						record.transform.tag = Constants.menuTextItemTag;
						TextMesh recorddText = record.GetComponent("TextMesh") as TextMesh;
						recorddText.text = texts[j-1];
						allTexts[i][j] = record;
					}
				}
			}
		}
	}
	
	// shift display and previous level to the left
	public bool toPrevious()
	{
		Vector3 direction = new Vector3(1,0,0);
		
		if (currentLevel == 1)
			return true;
		else
		{
			bool ret = true;
		
			for (int i = currentLevel - 2 ; i < currentLevel ; i++)
			{
				for (int j = 0 ; j < Constants.numRecordEntries + 1 ; j++)
				{
					TextScript ts = allTextScripts[i][j];
					if (!isTransitionDone[ts.textName])
						isTransitionDone[ts.textName] = ts.translateText(direction);
					ret = ret && isTransitionDone[ts.textName];
				}
			}
			
			return ret;
		}
		
	}
	
	// shift display and the next level to the right
	public bool toNext()
	{
		Vector3 direction = new Vector3(-1,0,0);
		if (currentLevel == Constants.lastPlayableSceneIndex - Constants.introSceneIndex)
			return true;
		else
		{
			bool ret = true;
			
			for (int i = currentLevel - 1 ; i < currentLevel+1 ; i++)
			{
				for (int j = 0 ; j < Constants.numRecordEntries + 1 ; j++)
				{
					TextScript ts = allTextScripts[i][j];
					if (!isTransitionDone[ts.textName])
						isTransitionDone[ts.textName] = ts.translateText(direction);
					ret = ret && isTransitionDone[ts.textName];
				}
			}
			
			return ret;
		}
	}
	
	public void toNextRecords()
	{
		if (display < Constants.lastPlayableSceneIndex - Constants.introSceneIndex)
			display++;
	}
	
	public void toPrevRecords()
	{
		if (display > 1)
			display--;
	}
}
