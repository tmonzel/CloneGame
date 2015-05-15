using UnityEngine;
using System.Collections;

public class SelectionBehavior : MonoBehaviour 
{

	private Rigidbody rb;
	
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		rb.mass = 0;
		rb.useGravity = false;

		//Tells the SelectionController that a selection-process has happened
		//and gives a Reference to the MeshCollider
		SelectionController.selectionHasStarted = true;
		SelectionController.selectionMesh = gameObject;
	}

	void Update () 
	{
		rb.position += Vector3.down * 0.99f;
	}

	void OnCollisionEnter(Collision col) 
	{
		//Tells the SelectionController which clones have been selected by the user
		if(!SelectionController.selectedUnits.Contains(col.gameObject))
		{
			SelectionController.selectedUnits.Add (col.gameObject);
		}
	    
		//destroying the selectionMesh and in doing so triggering the SelectionController to get active;
		Destroy(rb.gameObject);
	}
}
