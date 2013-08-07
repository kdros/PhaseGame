using UnityEngine;
using System.Collections;

public class PreviousReocrdsText : TextScript 
{
	public override void respondToClick ()
	{
		if (this.enabled)
		{
			Debug.Log("clicked to go to previous set of records");
			GameObject rb = GameObject.FindGameObjectWithTag ("RecordBoard");
			RecordBoard recordBoard = rb.GetComponent<RecordBoard>();
			recordBoard.toPrevious();
		}
	}
}
