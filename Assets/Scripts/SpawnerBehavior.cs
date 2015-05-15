using UnityEngine;
using System.Collections;

public class SpawnerBehavior : MonoBehaviour 
{
	public GameObject clone;
	public float spawnRate;
	public int spawnLimit;

	private Vector3[] spawnPositions;

	// Use this for initialization
	void Start () 
	{
		Rigidbody rb = GetComponent<Rigidbody>();

		//The possible spawnPositions are the four sides of the Spawner
		//              z (1.26)
		//          ---------
		//          |       |
		// (-1.26) x|       |x (1.26)
		//          |       |
		//          --------- 
		//              z (-1.26)
		//
		//Storing these positions (relative to the spawners position) here in an Array
		spawnPositions = new Vector3[] {new Vector3(rb.position.x - 1.26f, 0.5f, rb.position.z),
										new Vector3(rb.position.x + 1.26f, 0.5f, rb.position.z),
										new Vector3(rb.position.x, 0.5f, rb.position.z + 1.26f),
										new Vector3(rb.position.x, 0.5f, rb.position.z - 1.26f)};

		StartCoroutine(spawnClones());	
	}

	//spawns new clones from the spawner until it has reached its spwanLimit
	IEnumerator spawnClones()
	{
		yield return new WaitForSeconds(spawnRate);
		
		while(spawnLimit > 0)
		{
			//chosing one of the four possible spawnPositions for the next clone
			int random = (int)Mathf.Floor(Random.Range(0,4));

			Instantiate(clone, spawnPositions[random], Quaternion.identity);

			spawnLimit--;
			
			yield return new WaitForSeconds(spawnRate);
		}

		Destroy(gameObject);
	}

}
