using UnityEngine;
using System.Collections;

public class PanTrigger : MonoBehaviour 
{
	[System.Serializable]
	public class PanDestination
	{
		public Transform position;
		public string message;
		public float waitTime;
		public float panTime;
	}
	
	public PanDestination[] destination;
}
