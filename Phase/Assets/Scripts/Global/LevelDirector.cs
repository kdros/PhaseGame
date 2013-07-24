using UnityEngine;
using System.Collections;

public abstract class LevelDirector : MonoBehaviour 
{
	public abstract bool OnEventTrigger (string triggerName);
}
