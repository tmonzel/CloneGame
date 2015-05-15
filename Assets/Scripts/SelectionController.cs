using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionController : MonoBehaviour 
{
	public GameObject clone;

	public static List<GameObject> selectedUnits = new List<GameObject>();
	public static bool selectionHasStarted = false;
	public static GameObject selectionMesh = null;


	void Update()
	{
		//If these coinditions are true that means, that every selected Clone has been added to "selectedUnits"
		//This is true because if the reference to "selectionMesh" is null that means, that it has already been 
		//destroyed and so must have added every selected Clone to "selectedUnits"
		if(selectionHasStarted == true && selectionMesh == null)
		{
			//handleSelection();
		}
	}

	//This destroys all selected Clones and creates a new one in the center of the selection
	void handleSelection()
	{
		if(selectedUnits.Count > 1)
		{
			Vector3 spawnPosition = calculateCenterPosition();

			foreach(GameObject obj in selectedUnits)
			{
				Destroy(obj);
			}

			Instantiate(clone, spawnPosition, Quaternion.identity);

			resetSelection();
		}
		else
		{
			resetSelection();
		}
	}

	//Gives the Center of all selected Clones as retunr value 
	Vector3 calculateCenterPosition()
	{
		Vector3 addedUpPositions = new Vector3(0.0f, 0.0f, 0.0f);
		
		foreach(GameObject obj in selectedUnits)
		{
			addedUpPositions += obj.GetComponent<Rigidbody>().position;
		}

		Vector3 centerVector = new Vector3( addedUpPositions.x / selectedUnits.Count,
		                                    addedUpPositions.y / selectedUnits.Count,
		                                    addedUpPositions.z / selectedUnits.Count);

		return centerVector;
	}

	//Resets all members to their default-value so we can detect the next selection
	void resetSelection()
	{
		selectedUnits.Clear();
		selectionHasStarted = false;
		selectionMesh = null;
	}
	
}
