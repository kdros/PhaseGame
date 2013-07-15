#pragma strict

var target : GameObject;
var zpos : float;

function Update()
{
	transform.position.y = target.transform.position.y;
	transform.position.x = target.transform.position.x;
	transform.position.z = zpos;
}