using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPlacer : MonoBehaviour 
{
	public LayerMask gridsOnly;
	
	public GameObject lastGridSquare;
	
	public List<GameObject> availableTowers, towersBad, towersGood;
	int currentTower;
	public float yBufferTowerBase, yBufferTower;
	GameObject tb = null;
	bool buildOnClick = false;
	RaycastHit hit;
	
	// Use this for initialization
	void Start () 
	{
		lastGridSquare = Pathfinder.infinityGridSquare;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameManager.currentState == GameState.BuildPhase)
		{
			BuildPhaseGO();
			UpdateTowerSelection();
		}
		else
		{
			LittleClearFunction();
			Pathfinder.UnlightUpMarkers();
		}
	}
	
	void BuildPhaseGO()
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
				Vector3 tempPos = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTowerBase ,hit.collider.gameObject.transform.position.z);
				if(Pathfinder.FindPath())
				{
					//print ("Path good");
					tb = Instantiate(towersGood[0], tempPos, currentSquare.transform.rotation) as GameObject;
					buildOnClick = true;
				}
				else
				{
					//print ("Path bad");
					tb = Instantiate(towersBad[0], tempPos, currentSquare.transform.rotation) as GameObject;
					buildOnClick = false;
					
				}
				currentSquare.GetComponent<GridSquare>().canMove = true;
			}
			else if(currentSquare != lastGridSquare && !currentSquare.GetComponent<GridSquare>().canBuild)
			{
				lastGridSquare = currentSquare;
				LittleClearFunction();
				buildOnClick = false;
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
			Vector3 tempPos = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTowerBase ,hit.collider.gameObject.transform.position.z);
			Instantiate(availableTowers[0], tempPos, hit.collider.gameObject.transform.rotation);
			hit.collider.gameObject.GetComponent<GridSquare>().canMove = false;
			hit.collider.gameObject.GetComponent<GridSquare>().canBuild = false;
		}
	}
	
	void UpdateTowerSelection()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
		{
			currentTower = 0;
			GUIManager.ChangeSelectedTower(currentTower);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
		{
			currentTower = 1;
			GUIManager.ChangeSelectedTower(currentTower);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
		{
			currentTower = 2;
			GUIManager.ChangeSelectedTower(currentTower);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
		{
			currentTower = 3;
			GUIManager.ChangeSelectedTower(currentTower);
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
