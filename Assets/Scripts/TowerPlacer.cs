using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPlacer : MonoBehaviour 
{

	public GameObject gm;
	public GameObject player;
	public LayerMask gridsOnly;
	
	public GameObject lastGridSquare;
	
	public GameObject towerBase, towerBaseValid, towerBaseBad;
	public List<GameObject> availableTowers;
	public float yBuffer;
	GameObject tb = null;
	bool buildOnClick = false;
	RaycastHit hit;
	
	// Use this for initialization
	void Start () 
	{
		gm = GameObject.Find("_GameManager");
		player = GameObject.Find("PlayerTD");
		lastGridSquare = Pathfinder.infinityGridSquare;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameManager.currentState == GameState.BuildPhase)
		{
			Ray lookAtRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width*0.5f, Screen.height*0.5f));
			if(Physics.Raycast(lookAtRay, out hit, 100, gridsOnly))
			{
				GameObject currentSquare = hit.collider.gameObject;
				if(currentSquare != lastGridSquare && currentSquare.GetComponent<GridSquare>().canBuild)
				{
					//print(currentSquare.name);
					if(tb != null)
					{
						Destroy(tb);
					}
					lastGridSquare = currentSquare;
					currentSquare.GetComponent<GridSquare>().canMove = false;
					Vector3 tempPos = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBuffer ,hit.collider.gameObject.transform.position.z);
					if(Pathfinder.FindPath())
					{
						//print ("Path good");
						tb = Instantiate(towerBaseValid, tempPos, currentSquare.transform.rotation) as GameObject;
						buildOnClick = true;
					}
					else
					{
						//print ("Path bad");
						tb = Instantiate(towerBaseBad, tempPos, currentSquare.transform.rotation) as GameObject;
						buildOnClick = false;
						
					}
					currentSquare.GetComponent<GridSquare>().canMove = true;
				}
				else if(currentSquare != lastGridSquare && !currentSquare.GetComponent<GridSquare>().canBuild)
				{
					LittleClearFunction();
				}
			}
			if(hit.collider == null)
			{
				buildOnClick = false;
				LittleClearFunction();
			}
			if(buildOnClick && Input.GetMouseButtonDown(0))
			{
				Destroy(tb);
				Vector3 tempPos = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBuffer ,hit.collider.gameObject.transform.position.z);
				Instantiate(towerBase, tempPos, hit.collider.gameObject.transform.rotation);
				hit.collider.gameObject.GetComponent<GridSquare>().canMove = false;
				hit.collider.gameObject.GetComponent<GridSquare>().canBuild = false;
			}
		}
		else
		{
			LittleClearFunction();
		}
	}
	
	void LittleClearFunction()
	{
		if(tb != null)
		{
			Destroy(tb);
			if(hit.collider != null && hit.collider.gameObject.GetComponent<GridSquare>().canBuild)
			{				
				hit.collider.gameObject.GetComponent<GridSquare>().canMove = true;
			}
			Pathfinder.FindPath();
			lastGridSquare = Pathfinder.infinityGridSquare;
		}
	}
	
}
