using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour 
{
	public static List<GameObject> enemyPath;
	public int pathIndex = 0;
	public float speed;
	
	
	// Use this for initialization
	void Start () 
	{
		enemyPath = Pathfinder.currentPath;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(enemyPath.Count > pathIndex)
		{
			Vector3 target = enemyPath[pathIndex].GetComponent<GridSquare>().pathMarker.transform.position;
			transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
			if(Vector3.Distance(target, transform.position) < 0.01f)
			{
				pathIndex++;
			}
		}
	}
}
