using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSquare : MonoBehaviour 
{
	public bool isPathStart;
	public bool isTarget;	
	
	public bool canMove; //valid for pathfinding
	public bool canBuild; // valid for building
	
	public List<GameObject> neighbors;
	public GameObject pathMarker;
	
	//Pathfinding variables
	public float hValue;
	public float gValue;
	public float fValue;
	public GameObject parent;
	
	void Awake () 
	{
		pathMarker = transform.FindChild("EnemyPathMarker").gameObject;
		SetNeighbors();
		SetCanBuildDefaults();
		if(isPathStart)
		{
			gValue = 0;
		}
		else
		{
			gValue = 10;
		}
		if(!canMove)
		{
			gValue = 100;
		}
		ColorNoMoveRed();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void SetNeighbors()
	{
		Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.5f);
		int i = 0;
		while (i < hitColliders.Length) 
		{
			if(hitColliders[i].gameObject != gameObject && hitColliders[i].gameObject.tag == "GridSquare")
			{
				neighbors.Add(hitColliders[i].gameObject);
			}
			i++;
		}
	}
	
	public void SetCanBuildDefaults()
	{
		Vector3 up = transform.TransformDirection(Vector3.up);
		if (Physics.Raycast(transform.position, up, 5))
		{
			canBuild = canMove = false;
		}
	}
	
	public void ColorNoMoveRed()
	{
		if(!canMove)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
	}
	
	public void ColorMoveWhite()
	{
		if(canMove)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
	}
	
	public void ColorStartGreen()
	{
		if(isPathStart)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.green;
		}
	}
	
	public void ColorEndBlue()
	{
		if(isTarget)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		}
	}
}
