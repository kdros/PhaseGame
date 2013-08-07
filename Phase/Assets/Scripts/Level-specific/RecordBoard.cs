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
	
	private bool transitionDone;
	private int totalLevels;
	private int currentLevel; 								// level ordering (1~7)
	private int last;	
	
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
		if (spacing == 0)
			spacing = 0.5f;
		
		
		instantiateTexts();
		initTextScripts();
		initTransitionValues();
		initIsDoneTransition();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentLevel != last || !transitionDone)
		{
			
		}
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
			float baseSpeed = 20.0f;
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
						TextMesh levelText = level.GetComponent("TextMesh") as TextMesh;
						int l = i + 1;
						levelText.text = "Level "+l.ToString();
						allTexts[i][j] = level;
					}	
					else
					{
						Vector3 position = HideAnchorRight.transform.position;
						position = position - new Vector3(0, j*spacing, 0);
						GameObject record = Instantiate (baseText, position, Quaternion.identity) as GameObject;
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
		if (currentLevel == 1)
			return true;
		else
		{
			bool ret = true;
			
			return ret;
		}
		
	}
	
	// shift display and the next level to the right
	public bool toNext()
	{
		if (currentLevel == Constants.lastPlayableSceneIndex - Constants.introSceneIndex)
			return true;
		else
		{
			bool ret = true;
			
			return ret;
		}
		
		
	}
}
