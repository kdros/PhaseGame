using UnityEngine;
using System.Collections;

/**
 * Attached to a game object that is to be translated from its current position in the specified direction
 * and speed. Once the object has traveled a distance of maxDistance, it will reverse directions and return 
 * to its original position. 
 */
public class TranslateObject : MonoBehaviour 
{
	public Vector3 direction;
	public float speed;
	public float maxDistance;
	
	private float m_curDistance;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	void FixedUpdate () 
	{
		Vector3 newPos = transform.position;
		Vector3 deltaD = direction * speed * Time.deltaTime;
		newPos = newPos + deltaD;
		transform.position = newPos;
		
		m_curDistance += deltaD.magnitude;
		if (m_curDistance >= maxDistance)
		{
			speed *= -1;
			m_curDistance = 0;
		}
	}
}