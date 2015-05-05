using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour 
{
	public static List<GameObject> enemyPath;
	public int pathIndex = 0;
	public float speed;
	public float speedLimit;
	public GameObject player;
	bool stayOnPath;
	public LayerMask noPlayer;
	bool followingPlayer = false;
	
	
	// Use this for initialization
	void Start () 
	{
		enemyPath = Pathfinder.currentPath;
		player = GameObject.Find ("PlayerTD");
		stayOnPath = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(enemyPath.Count > pathIndex && stayOnPath)
		{
			Vector3 target = enemyPath[pathIndex].GetComponent<GridSquare>().pathMarker.transform.position;
			transform.LookAt(target);
			gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Force);
			if(Vector3.Distance(target, transform.position) < 0.1f)
			{
				pathIndex++;
			}
		}
		if (Vector3.Distance (transform.position, player.transform.position) < 2) 
		{
			RaycastHit hit;
			if(Physics.Raycast(player.transform.position, Vector3.down, out hit, 2))
			{
				if(hit.collider.gameObject.tag == "GridSquare")
				{
					stayOnPath = false;
					followingPlayer = true;
					EnemyAttackBehaviorRushMelee();
				}
				else
				{
					followingPlayer = false;
					//FindNewPath();
					stayOnPath = true;
				}
			}
		}
		else
		{
			if(followingPlayer)
			{
				//FindNewPath();				
			}
			followingPlayer = false;
			stayOnPath = true;
		}
		//print (GetComponent<Rigidbody>().velocity.magnitude);
		if(GetComponent<Rigidbody>().velocity.magnitude > speedLimit)
		{
			Vector3 newSpeed = GetComponent<Rigidbody>().velocity;
			newSpeed.Normalize();
			GetComponent<Rigidbody>().velocity = newSpeed * speedLimit;			
		}
	}
	
	void FindNewPath()
	{
		GameObject CurrentGrid;
		RaycastHit hit;
		Debug.LogError ("5");
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 2))
		{
			if(hit.collider.gameObject.tag == "GridSquare")
			{
				CurrentGrid = hit.collider.gameObject;
				enemyPath = Pathfinder.GetCorrectionPath(CurrentGrid);
				pathIndex = 0;
			}
		}
		
	}
	
	void EnemyAttackBehaviorRushMelee()
	{
		Vector3 modifiedPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		transform.LookAt(modifiedPosition);
		gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Force);
	}	
}
