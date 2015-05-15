using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class RandomMover : MonoBehaviour 
{
	public float speed;
	public float velocityChangeWait;
	public Boundary boundary;
	
	private float nextVelocityChange;


	void Start()
	{
		nextVelocityChange = Time.time;
	}

	void FixedUpdate()
	{
		//changing the direction the clone is moving if enough time has elapsed
		if(Time.time > nextVelocityChange)
		{
			Vector2 newVelocity = Random.insideUnitCircle * speed;
			GetComponent<Rigidbody>().velocity = new Vector3(newVelocity.x, 0, newVelocity.y);
			nextVelocityChange = Time.time + velocityChangeWait;
		}

		//check if the clone is trying to leave the viewport and redirect it if necessary
		Vector3 currentPosition = GetComponent<Rigidbody>().position;
		if(currentPosition.x < boundary.xMin || currentPosition.x > boundary.xMax)
		{
			Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
			newVelocity.x = - newVelocity.x;
			GetComponent<Rigidbody>().velocity = newVelocity;
			 
		}
		if(currentPosition.z < boundary.zMin || currentPosition.z > boundary.zMax)
		{
			Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
			newVelocity.z = - newVelocity.z;
			GetComponent<Rigidbody>().velocity = newVelocity;		
		}

	}




}
