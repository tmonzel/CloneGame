using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public float spawnRate;
	public float startWait;
	public Vector3 spawnBoundary;
	public GameObject spawner;

	
	void Start () 
	{
		StartCoroutine(spawnSpawner());
	}
	
	IEnumerator spawnSpawner()
	{
		yield return new WaitForSeconds(startWait);

		//creating the new Spawners in the Level and positioning them inside the given Boundaries
		while(true)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(-spawnBoundary.x, spawnBoundary.x),
			                                    0.75f,
			                                    Random.Range(-spawnBoundary.y, spawnBoundary.y));

			Instantiate(spawner, spawnPosition, Quaternion.identity);

			yield return new WaitForSeconds(spawnRate);
		}	

	}

}
