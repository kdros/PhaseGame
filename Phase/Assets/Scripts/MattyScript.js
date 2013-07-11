#pragma strict

function Start () {
	// Set all animations to loop
   animation.wrapMode = WrapMode.Loop;
   // except jumping
   animation["Jump"].wrapMode = WrapMode.Once;
   
   animation.Play("Idle", PlayMode.StopAll);
}

function Update () {
	
	if(Input.GetKeyDown("up"))
 	{
  		// Plays the reload animation - stops all other animations
  		animation.Play("Jump", PlayMode.StopAll);
 	}
}