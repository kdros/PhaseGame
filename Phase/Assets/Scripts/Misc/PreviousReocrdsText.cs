using UnityEngine;
using System.Collections;

// PreviousReocrdsText inherits TextScript and is responsible for defining the behavior of the 
// Previous text in the SpeedRunRecord Scene
public class PreviousReocrdsText : TextScript 
{
	// Go back to the records for the previous stage
	public override void respondToClick ()
	{
		if (this.enabled)
		{
			//Debug.Log("clicked to go to previous set of records");
			GameObject rb = GameObject.FindGameObjectWithTag ("RecordBoard");
			RecordBoard recordBoard = rb.GetComponent<RecordBoard>();
			recordBoard.toPrevRecords();
		}
	}
}
