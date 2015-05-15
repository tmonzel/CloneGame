using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloneSelector : MonoBehaviour 
{
	private List<Vector3> selectedRegion;
	private List<GameObject> selectedClones;
	private LineRenderer line;

	public GameObject clone;

	void Start () 
	{
		selectedRegion = new List<Vector3> ();
		selectedClones = new List<GameObject> ();
		
		line = GetComponent<LineRenderer> ();
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			selectedRegion.Clear ();
			selectedClones.Clear ();
		}
		
		if(Input.GetMouseButton(0)) {
			drawSelectionPoint();
		}
		
		if (Input.GetMouseButtonUp(0)) 
		{
			invokeSelection();
			line.SetVertexCount(0);
		}
	}

	Vector3 calculateCenterPosition()
	{
		Vector3 addedUpPositions = new Vector3(0.0f, 0.0f, 0.0f);
		
		foreach(GameObject obj in selectedClones)
		{
			addedUpPositions += obj.GetComponent<Rigidbody>().position;
		}
		
		Vector3 centerVector = new Vector3( addedUpPositions.x / selectedClones.Count,
		                                   addedUpPositions.y / selectedClones.Count,
		                                   addedUpPositions.z / selectedClones.Count);
		
		return centerVector;
	}

	private void invokeSelection() 
	{
		GameObject[] clones = GameObject.FindGameObjectsWithTag("Klon");
		
		foreach(GameObject obj in clones) 
		{
			if(ContainsPoint(selectedRegion.ToArray(), obj.transform.position)) 
			{
				selectedClones.Add(obj);
			}
		}

		if (selectedClones.Count > 1) 
		{
			Vector3 spawnPosition = calculateCenterPosition();

			foreach(GameObject obj in selectedClones)
			{
				Destroy(obj);
			}

			Instantiate(clone, spawnPosition, Quaternion.identity);
		}
	}

	private Vector3 calcMousePoint() 
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6));
	}

	private void drawSelectionPoint ()
	{
		Vector3 point = calcMousePoint ();
		
		if (!selectedRegion.Contains (point)) 
		{                 
			selectedRegion.Add (point);
			line.SetVertexCount (selectedRegion.Count);
			line.SetPosition (selectedRegion.Count - 1, (Vector3)selectedRegion [selectedRegion.Count - 1]);        
		}
	}

	private bool ContainsPoint (Vector3[] polyPoints, Vector3 p) 
	{ 
		int j = polyPoints.Length-1; 
		bool inside = false;
		
		for (int i = 0; i < polyPoints.Length; j = i++) 
		{ 
			if ( ((polyPoints[i].z <= p.z && p.z < polyPoints[j].z) || (polyPoints[j].z <= p.z && p.z < polyPoints[i].z)) && 
			    (p.x < (polyPoints[j].x - polyPoints[i].x) * (p.z - polyPoints[i].z) / (polyPoints[j].z - polyPoints[i].z) + polyPoints[i].x)) 
				inside = !inside; 
		} 
		
		return inside; 
	}
}
