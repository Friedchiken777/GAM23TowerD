using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMeshEnemy : MonoBehaviour 
{
	private int targetIndex = 0;
	private NavMeshAgent navMeshAgent;
	public List<GameObject> navTargets;
	public float navTargetDistanceThreshold = 1.0f;
	public float aggroRadius = 5.0f;
	private GameObject player;
	public bool isMovement = true;

	// Use this for initialization
	void Start () 
	{
		navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		if (navMeshAgent == null)
		{
			navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
		}
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isMovement != false)
		{
			if (navMeshAgent == null)
				return;
			if (navTargets.Count == 0)
				return;
			if (player == null)
				return;
			if (Vector3.Distance (transform.position, player.transform.position) <= aggroRadius)
			{
				navMeshAgent.destination = player.transform.position;
			}
			else
			{
				navMeshAgent.destination = navTargets[targetIndex].transform.position;
				if (Vector3.Distance (transform.position, navMeshAgent.destination) <= navTargetDistanceThreshold)
				{
					targetIndex++;
					if (targetIndex >= navTargets.Count)
					{
						targetIndex = 0;
					}
				}
			}
		}
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Projectile")
		{
			isMovement = false;
		}
	}
}
