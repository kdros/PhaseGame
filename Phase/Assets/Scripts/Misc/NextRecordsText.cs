using UnityEngine;
using System.Collections;

// NextRecordsText inherits TextScript and is responsible for defining the behavior of the 
// Next text in the SpeedRunRecord Scene
public class NextRecordsText : TextScript 
{
	// Go back to the records for the next stage
	public override void respondToClick ()
	{
		if (this.enabled)
		{
			//Debug.Log("go to the next set of records");
			GameObject rb = GameObject.FindGameObjectWithTag ("RecordBoard");
			RecordBoard recordBoard = rb.GetComponent<RecordBoard>();
			recordBoard.toNextRecords();
		}
	}
}
