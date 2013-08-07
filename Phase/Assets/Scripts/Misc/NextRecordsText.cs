using UnityEngine;
using System.Collections;

public class NextRecordsText : TextScript 
{
	public override void respondToClick ()
	{
		if (this.enabled)
		{
			Debug.Log("go to the next set of records");
			GameObject rb = GameObject.FindGameObjectWithTag ("RecordBoard");
			RecordBoard recordBoard = rb.GetComponent<RecordBoard>();
			recordBoard.toNext();
		}
	}
}
