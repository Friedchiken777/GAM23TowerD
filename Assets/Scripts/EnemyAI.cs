using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour 
{
	public static List<GameObject> enemyPath;
	public int pathIndex = 0;
	public float speed;
	public GameObject player;
	bool stayOnPath;
	public LayerMask noPlayer;
	
	
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
			//transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
			//Vector3 modifiedTargetPosition = new Vector3(target.x, transform.position.y, target.z);
			transform.LookAt(target);
			gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Force);
			if(Vector3.Distance(target, transform.position) < 0.1f)
			{
				pathIndex++;
			}
		}
		if (Vector3.Distance (transform.position, player.transform.position) < 2) {
			stayOnPath = false;
			RaycastHit hit;
			if(Physics.Raycast(transform.position, player.transform.position, out hit, 10, noPlayer))
			{
				if(hit.collider.gameObject.tag == "GridSquare")
				{
					Vector3 modifiedPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
					transform.LookAt(modifiedPosition);
					gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Force);
					//Vector3 modifiedPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
					//transform.position = Vector3.MoveTowards (transform.position, modifiedPosition, speed * Time.deltaTime);
				}
			}
		}
		else
		{
			stayOnPath = true;
		}
	}	
}
