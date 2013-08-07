using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

// Utility class for managing the speed run records
// Stores a file in Save/records
public class RecordManager : MonoBehaviour {
	
	// Parses file for the record of all playable levels.
	// Each level will have maximum of 5 records
	List<List<double>> allRecords; 
	int totalScenes;
	int numEntries;
	
	// Use this for initialization
	void Start () 
	{
		totalScenes = Constants.lastPlayableSceneIndex - Constants.introSceneIndex;
		allRecords = new List<List<double>>();
		numEntries = Constants.numRecordEntries;
		
		for (int i = 0 ; i < totalScenes ; i++)
		{
			List<double> entry = new List<double>();
			allRecords.Add (entry);
		}
				
		if (System.IO.File.Exists (Constants.SpeedRunFilePath))
		{
			readRecords();
		}
		else
		{
			createNewRecord();
			readRecords();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void readRecords()
	{
		StreamReader reader = new StreamReader(Constants.SpeedRunFilePath);
		string allData = reader.ReadToEnd();
		char[] delimiters = new char[] { '\r', '\n', '\t'};
		string[] data = allData.Split (delimiters, StringSplitOptions.RemoveEmptyEntries);
		
		for (int i = 0 ; i < data.Length ; i++)
		{
			int sceneIndex = i / 5;
			double entry = Convert.ToDouble (data[i]);
			allRecords[sceneIndex].Add(entry);
		}
		
		reader.Close();
	}
	
	private void createNewRecord()
	{
		StreamWriter writer = new StreamWriter(Constants.SpeedRunFilePath);
		string defaultRecord = Constants.defaultSpeedRecord.ToString();
	
		string line = "";
		for (int j = 0 ; j < numEntries ; j++) // allow for 5 entries per scene
		{
			line = line + defaultRecord + "\t";
		}
		
		
		for (int i = 0 ; i < totalScenes ; i++)
		{
			writer.Write(line+System.Environment.NewLine);
		}
		
		writer.Close ();
	}
	
	// Add the record to allRecords
	public void addRecord(int sceneIndex, double recInSec)
	{
		int index = sceneIndex - (Constants.introSceneIndex+1);
		double slowest = allRecords[index][numEntries-1];
		
		if (slowest > recInSec)
		{
			allRecords[index][numEntries-1] = recInSec;
			allRecords[index].Sort();
			saveRecords();
		}		
	}
	
	// return record in --H--M--S format
	private string getFormattedRecord(double sec)
	{
		string[] result = new string[3];
		TimeSpan ts = TimeSpan.FromSeconds(sec);
		result[0] = ts.Hours.ToString().PadLeft(2,'0');
		result[1] = ts.Minutes.ToString().PadLeft(2,'0');
		result[2] = ts.Seconds.ToString().PadLeft(2,'0');
		string timeString = result[0] + " H " + result[1] + " M " + result[2] +" S";
		return timeString;	
	}
	
	public List<string> getLevelRecords(int sceneIndex)
	{
		List<string> ret = new List<string>();
		int index = sceneIndex - (Constants.introSceneIndex+1);
		
		for (int i = 0 ; i < numEntries ; i++)
		{
			double sec = allRecords[index][i];
			string secString = getFormattedRecord(sec);
			ret.Add (secString);
		}
		
		return ret;
	}
	
	// write allRecords to Save/Records
	private void saveRecords()
	{
		File.Delete(Constants.SpeedRunFilePath);
		StreamWriter writer = new StreamWriter(Constants.SpeedRunFilePath);
		
		for (int i = 0 ; i < totalScenes ; i++)
		{
			
			string line = "";
			
			for (int j = 0 ; j < numEntries ; j++)
			{
				line = line + allRecords[i][j] + "\t";
			}
			
			writer.Write(line+System.Environment.NewLine);
		}
		
		writer.Close ();		
	}
}
