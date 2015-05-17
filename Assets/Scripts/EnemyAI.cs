using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour 
{
	public List<GameObject> enemyPath;
	public int pathIndex = 0;
	public float speed;
	public float speedLimit;
    public float gateDistance;
	public GameObject player;
    public GameObject gate;
	bool stayOnPath;
	public LayerMask noPlayer;
	bool followingPlayer = false;
	bool findingNewPath;
	public float nudgeDistance;
	
	
	// Use this for initialization
	void Start () 
	{
		enemyPath = Pathfinder.currentPath;
		player = GameManager.currentPlayer;
        gate = GameObject.FindGameObjectWithTag("Gate");
		stayOnPath = true;
		findingNewPath = false;
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
        if (Vector3.Distance(transform.position, gate.transform.position) < gateDistance)
        {
            gate.gameObject.GetComponent<Gate>().isDamaged = true;
        }
        if (Vector3.Distance(transform.position, gate.transform.position) > gateDistance)
        {
            gate.gameObject.GetComponent<Gate>().isDamaged = false;
        }
		if(Vector3.Distance (transform.position, player.transform.position) < 1)
		{
			gameObject.GetComponent<Enemy>().isAttacking = true;
            player.gameObject.GetComponent<TDCharacterController>().isDamaged = true;
		}
        else if (Vector3.Distance(transform.position, player.transform.position) > 1)
        {
            gameObject.GetComponent<Enemy>().isAttacking = false;
            player.gameObject.GetComponent<TDCharacterController>().isDamaged = false;
        }
		if (Vector3.Distance (transform.position, player.transform.position) < 2 && Vector3.Distance (transform.position, player.transform.position) > 0.5) 
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
					
					if(followingPlayer && !findingNewPath)
					{
						StartCoroutine(FindNewPath());
					}
					followingPlayer = false;
					stayOnPath = true;
				}
			}
		}
		else
		{
			if(followingPlayer && !findingNewPath)
			{
				StartCoroutine(FindNewPath());				
			}
			followingPlayer = false;
			stayOnPath = true;
		}
		if(GetComponent<Rigidbody>().velocity.magnitude > speedLimit)
		{
			Vector3 newSpeed = GetComponent<Rigidbody>().velocity;
			newSpeed.Normalize();
			GetComponent<Rigidbody>().velocity = newSpeed * speedLimit;			
		}
	}
	
	IEnumerator FindNewPath()
	{
		findingNewPath = true;
		List<GameObject> sidePath = new List<GameObject>();
		GameObject CurrentGrid;
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 2))
		{
			if(hit.collider.gameObject.tag == "GridSquare")
			{
				CurrentGrid = hit.collider.gameObject;
				GameObject[] temp = GameObject.FindGameObjectsWithTag("GridSquare");
				Pathfinder.PopulateValidNodeList();
				Pathfinder.openList.Clear();
				Pathfinder.closedList.Clear();
				Pathfinder.proposedPath.Clear();
				Pathfinder.CalculateParent(CurrentGrid);
				GameObject nextNode = Pathfinder.CalculateNextNode();
				for(int i = 0; i < Pathfinder.validNodeList.Count; i++)
				{
					if(!Pathfinder.openList.Contains(Pathfinder.end))
					{
						Pathfinder.CalculateParent(nextNode);
						nextNode = Pathfinder.CalculateNextNode();
					}
					else
					{
						i = Pathfinder.validNodeList.Count + 1;
					}
				}
				GameObject backtrack = Pathfinder.end;
				Pathfinder.proposedPath.Add(backtrack);
				List<GameObject> traversed = new List<GameObject>();
				while(backtrack.GetComponent<GridSquare>().parent != null)
				{
					if(!traversed.Contains(backtrack.GetComponent<GridSquare>().parent))
					{
						backtrack = backtrack.GetComponent<GridSquare>().parent;
						Pathfinder.proposedPath.Add(backtrack);
					}
					else
					{
						break;
					}
					traversed.Add(backtrack);
				}
				yield return new WaitForSeconds(1);
				for(int i = Pathfinder.proposedPath.Count - 1; i >= 0; i--)
				{
					sidePath.Add(Pathfinder.proposedPath[i]);
				}
				enemyPath = sidePath;
				pathIndex = 0;
			}
		}
		findingNewPath = false;
		yield return null;
		
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == "Enemy")
		{
			Vector3 push = new Vector3(transform.position.x + nudgeDistance, transform.position.y, transform.position.z + nudgeDistance);
			transform.position = push;
		}
	}
	
	void EnemyAttackBehaviorRushMelee()
	{
		Vector3 modifiedPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		transform.LookAt(modifiedPosition);
		gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Force);
	}	
}
