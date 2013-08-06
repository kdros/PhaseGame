using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Utility class for managing the speed run records
// Stores a file in Save/records
public class RecordManager : MonoBehaviour {
	
	// Parses file for the record of all playable levels.
	// Each level will have maximum of 5 records
	List<Dictionary<string,double>> allRecords;	  
	
	// Use this for initialization
	void Start () 
	{
		if (System.IO.File.Exists (Constants.SpeedRunFilePath))
		{
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void readRecords()
	{
			
	}
	
	// Add the record to allRecords
	public void addRecord(int sceneIndex, double recondInSeconds)
	{
			
	}
	
	// write allRecords to Save/Records
	public void saveRecords()
	{
		
	}
}
