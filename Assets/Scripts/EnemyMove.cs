using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMove : MonoBehaviour 
{
	Pathfinder pf;
	
	List<GameObject> path;
	
	// Use this for initialization
	void Start () 
	{
		//pf = GameObject.Find ("_GameManager").GetComponent<Pathfinder>();
		//path = pf.currentPath;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
