using UnityEngine;
using System.Collections;

public class MattySolidScript : MatterScript {
	
	public MatterScript matter_solid;
	
	void Start()
	{
		matter_solid = new MatterScript();
	}
	
    void Update()
	{
	
    }
	
	public override void CheckpointCollisionResolution()
	{
		
	}
	
	//public override void FallingBouldersCollisionResolution()
	//{
	//}
	
	//public override void FlamePillarCollisionResolution()
	//{	
	//}
	
	//public override void GrateCollisionResolution()
	//{
	//}
	
	public override void IceCeilingCollisionResolution()
	{
		// Shatters ice ceiling
	}
	
	public override void IcyFloorCollisionResolution()
	{
		// Slides on ice floor
	}
	
	//public override void LavaCollisionResolution()
	//{
	//}
	
	//public override void PitfallCollisionResolution()
	//{	
	//}
	
	//public override void SpikeCollisionResolution()
	//{	
	//}
	
	//public override void SwingingMaceCollisionResolution()
	//{
	//}
	
	//public override void WindTunnelCollisionResolution()
	//{
	//}
	
	public void Die ()
	{
		//GameObject obj = GameObject.Find("GlobalObject_BegLev1");
		//Global_BegLev1 g = obj.GetComponent<Global_BegLev1>();
		//g.mattySolidDeath = true;
		
		
    	Destroy(gameObject);
	}
}
