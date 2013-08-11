using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour 
{
	public static int introSceneIndex = 2;								// index of the introductory scene (first playable level)
	public static int lastPlayableSceneIndex = 9;						// index of the last playable scene
	public static int numRecordEntries = 5;								// number of record entries
	public static double defaultSpeedRecord = 86399;					// speed run records are initialized to this value
	public static string LevelToLoadKey = "LevelToLoad";				// key for playerpref used for choosing which scene to load
	public static string GameModeKey = "GameMode";						// key for playerpref used for switching game modes
	public static string GameModeNormal = "Normal";						// value for playerpref's GameMode key that represents normal game mode
	public static string GameModeSpeedRun = "Speed Run";				// value for playerpref's GameMode key that represents Speed run game mode
	public static string loadRecordLevel = "Records";					// name of the text in main menu that represents the link to the record scene
	public static string loadCharProfiles = "mattyProfiles";			// name of the text in main menu that represents the link to the character profile scene
	public static string continueBackButton = "ContinueBack";			// name of the text in main menu that represents the continue option's back button
	public static string menuTextItemTag = "MenuText";					// name of the tag for each hoverable / clickable menu text. Used by GameMenuSoundManager.
	public static string level1SpeedRunDataKey = "lev1SpeedRun";		// key for playerpref used for keeping track of level 1 speed run data
	public static string level2SpeedRunDataKey = "lev2SpeedRun";		// key for playerpref used for keeping track of level 2 speed run data
	public static string level3SpeedRunDataKey = "lev3SpeedRun";		// key for playerpref used for keeping track of level 3 speed run data
	public static string level4SpeedRunDataKey = "lev4SpeedRun";		// key for playerpref used for keeping track of level 4 speed run data
	public static string level5SpeedRunDataKey = "lev5SpeedRun";		// key for playerpref used for keeping track of level 5 speed run data
	public static string level6SpeedRunDataKey = "lev6SpeedRun";		// key for playerpref used for keeping track of level 6 speed run data
	public static string level7SpeedRunDataKey = "lev7SpeedRun";		// key for playerpref used for keeping track of level 7 speed run data
	public static string SpeedRunFilePath = "Save/Record";				// path to the file that speed run records are being stored
}
