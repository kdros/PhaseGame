using UnityEngine;
using System.Collections;

public class PanTrigger : MonoBehaviour 
{
	public Transform subsectionCameraPos; // camera position for viewing the subarea covered by the pan trigger
	
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
