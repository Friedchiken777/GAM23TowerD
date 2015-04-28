using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour 
{
	public List<GameObject> validNodeList;
	
	public GameObject start, end;
	public int startcount, endcount;
	
	public List<GameObject> openList;
	public List<GameObject> closedList;
	
	public List<GameObject> currentPath, proposedPath;
	
	GameObject infinityGridSquare;

	// Use this for initialization
	void Start () 
	{
		infinityGridSquare = Resources.Load("InfinityGridSquare") as GameObject;
		if(PopulateValidNodeListDebugColors())
		{
			CalculateHValues();
			if(FindPath ())
			{
				SetPath();
			}
		}		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			if(FindPath ())
			{
				SetPath();
			}
		}
	}
	
	bool ValidLevelCheck()
	{
		bool r = true;
		if(start == null)
		{
			Debug.LogError("Invalid Map, No Path Start Detected");
			r = false;
		}
		if(end == null)
		{
			Debug.LogError("Invalid Map, No Path Destination Detected");
			r = false;
		}
		if(startcount > 1)
		{
			Debug.LogError("Invalid Map, Too Many Path Starts Detected. Total Found: " + startcount);
			r = false;
		}
		if(endcount > 1)
		{
			Debug.LogError("Invalid Map, Too Many Path Destinations Detected. Total Found: " + endcount);
			r = false;
		}
		return r;
	}
	
	void ClearLists()
	{
		validNodeList.Clear();
		openList.Clear();
		closedList.Clear();
		for(int i = 0; i < currentPath.Count; i++)
		{
			currentPath[i].GetComponent<GridSquare>().pathMarker.SetActive(false);
			currentPath[i].GetComponent<GridSquare>().gValue = 0;
			currentPath[i].GetComponent<GridSquare>().parent = null;
		}
		currentPath.Clear();
		proposedPath.Clear();
	}
	
	bool PopulateValidNodeList()
	{
		startcount = endcount = 0;
		GameObject[] temp = GameObject.FindGameObjectsWithTag("GridSquare");
		for(int i = 0; i < temp.Length; i++)
		{
			if(temp[i].GetComponent<GridSquare>().canMove)
			{
				validNodeList.Add(temp[i]);
			}
			if(temp[i].GetComponent<GridSquare>().isPathStart)
			{
				start = temp[i];
				startcount++;
			}
			if(temp[i].GetComponent<GridSquare>().isTarget)
			{
				end = temp[i];
				endcount++;
			}
		}
		return ValidLevelCheck();
	}
	
	bool PopulateValidNodeListDebugColors()
	{
		startcount = endcount = 0;
		GameObject[] temp = GameObject.FindGameObjectsWithTag("GridSquare");
		for(int i = 0; i < temp.Length; i++)
		{
			if(temp[i].GetComponent<GridSquare>().canMove)
			{
				validNodeList.Add(temp[i]);
				temp[i].GetComponent<GridSquare>().ColorMoveWhite();
			}
			else
			{
				temp[i].GetComponent<GridSquare>().ColorNoMoveRed();
			}
			if(temp[i].GetComponent<GridSquare>().isPathStart)
			{
				start = temp[i];
				startcount++;
				temp[i].GetComponent<GridSquare>().ColorStartGreen();
			}
			if(temp[i].GetComponent<GridSquare>().isTarget)
			{
				end = temp[i];
				endcount++;
				temp[i].GetComponent<GridSquare>().ColorEndBlue();
			}
		}
		return ValidLevelCheck();
	}
	
	void CalculateHValues()
	{
		float startx;
		float startz;
		float endx = end.transform.position.x;
		float endz = end.transform.position.z;
		for(int i = 0; i < validNodeList.Count; i++)
		{
			startx = validNodeList[i].transform.position.x;
			startz = validNodeList[i].transform.position.z;
			validNodeList[i].GetComponent<GridSquare>().hValue = (Mathf.Abs((startx - endx)) + Mathf.Abs((startz - endz)));
		}
	}
	
	void CalculateParent(GameObject node)
	{
		if(!closedList.Contains(node))
		{
			closedList.Add(node);
			//print ("Closred List Added: " +node);
		}
		if(openList.Contains(node))
		{
			openList.Remove(node);
			//print ("Open List Removed: " +node);
		}
		for(int i = 0; i < node.GetComponent<GridSquare>().neighbors.Count; i++)
		{
			if(!openList.Contains(node.GetComponent<GridSquare>().neighbors[i]) && 
			  		!closedList.Contains(node.GetComponent<GridSquare>().neighbors[i]) && 
						node.GetComponent<GridSquare>().neighbors[i].GetComponent<GridSquare>().canMove)
			{
				node.GetComponent<GridSquare>().neighbors[i].GetComponent<GridSquare>().parent = node;
				openList.Add(node.GetComponent<GridSquare>().neighbors[i]);	
				node.GetComponent<GridSquare>().neighbors[i].GetComponent<GridSquare>().gValue = 
					node.GetComponent<GridSquare>().gValue + node.GetComponent<GridSquare>().neighbors[i].GetComponent<GridSquare>().gValue;
				node.GetComponent<GridSquare>().neighbors[i].GetComponent<GridSquare>().fValue =
					node.GetComponent<GridSquare>().neighbors[i].GetComponent<GridSquare>().hValue + node.GetComponent<GridSquare>().neighbors[i].GetComponent<GridSquare>().gValue;		
			}
		}
	}
	
	GameObject CalculateNextNode()
	{
		GameObject nextNode = infinityGridSquare;
		for(int i = 0; i < openList.Count; i++)
		{
			if(openList[i].GetComponent<GridSquare>().fValue < nextNode.GetComponent<GridSquare>().fValue)
			{
				nextNode = openList[i];
			}
		}
		return nextNode;
	}
	
	void LightUpMarkers()
	{
		for(int i = 0; i < currentPath.Count; i++)
		{
			currentPath[i].GetComponent<GridSquare>().pathMarker.SetActive(true);
		}
	}
	
	public bool FindPath()
	{
		ClearLists();
		if(PopulateValidNodeListDebugColors())
		{
			CalculateParent(start);
			GameObject nextNode = CalculateNextNode();
			for(int i = 0; i < validNodeList.Count; i++)
			{
				if(!openList.Contains(end))
				{
					CalculateParent(nextNode);
					nextNode = CalculateNextNode();
				}
				else
				{
					i = validNodeList.Count + 1;
				}
			}
			GameObject backtrack = end;
			proposedPath.Add(backtrack);
			while(backtrack.GetComponent<GridSquare>().parent != null)
			{
				backtrack = backtrack.GetComponent<GridSquare>().parent;
				proposedPath.Add(backtrack);
			}
		}
		if(proposedPath.Count > 1)
		{
			return true;
		}
		else
		{
			//Debug.LogError("No Path Detected");
			return false;
		}
	}
	
	public void SetPath()
	{		
		for(int i = proposedPath.Count - 1; i >= 0; i--)
		{
			currentPath.Add(proposedPath[i]);
		}
		LightUpMarkers();		
	}
}
