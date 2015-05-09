using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPlacer : MonoBehaviour 
{
	public LayerMask gridsOnly;
	
	public GameObject lastGridSquare;
	
	public List<GameObject> availableTowers, towersGood;
	public GameObject towerBaseBad;
	int currentTower;
	public float yBufferTowerBase, yBufferTower;
	GameObject tb = null;
	GameObject tTemp = null;
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
		Vector3 tempPosBase = Vector3.zero;
		Vector3 tempPosTower = Vector3.zero;
		if(Physics.Raycast(lookAtRay, out hit, 100, gridsOnly))
		{
			GameObject currentSquare = hit.collider.gameObject;
			if(currentSquare != lastGridSquare && (currentSquare.GetComponent<GridSquare>().canBuild || currentSquare.GetComponent<GridSquare>().hasTowerBase))
			{
				//print(currentSquare.name);
				if(tb != null)
				{
					Destroy(tb);
				}
				if(tTemp != null)
				{
					Destroy(tTemp);
				}	
				lastGridSquare = currentSquare;
				currentSquare.GetComponent<GridSquare>().canMove = false;
				if(currentTower == 0 || !currentSquare.GetComponent<GridSquare>().hasTowerBase)
				{
					tempPosBase = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTowerBase ,hit.collider.gameObject.transform.position.z);
					if(Pathfinder.FindPath())
					{
						//print ("Path good");
						if(!currentSquare.GetComponent<GridSquare>().hasTowerBase)
						{
							tb = Instantiate(towersGood[0], tempPosBase, currentSquare.transform.rotation) as GameObject;
						}
						buildOnClick = true;
					}
					else
					{
						//print ("Path bad");
						if(!currentSquare.GetComponent<GridSquare>().hasTowerBase)
						{
							tb = Instantiate(towerBaseBad, tempPosBase, currentSquare.transform.rotation) as GameObject;
						}
						buildOnClick = false;
						
					}
					if(!currentSquare.GetComponent<GridSquare>().hasTowerBase)
					{
						currentSquare.GetComponent<GridSquare>().canMove = true;
					}
				}
				if(currentTower > 0 && towersGood[currentTower] != null)
				{
					tempPosTower = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTower ,hit.collider.gameObject.transform.position.z);
					tTemp = Instantiate(towersGood[currentTower], tempPosTower, currentSquare.transform.rotation) as GameObject;
				}
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
			if(tTemp != null)
			{
				tempPosTower = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTower ,hit.collider.gameObject.transform.position.z);
				Instantiate(availableTowers[currentTower], tempPosTower, hit.collider.gameObject.transform.rotation);
				hit.collider.gameObject.GetComponent<GridSquare>().hasTower = true;
				Destroy(tTemp);
			}
			tempPosBase = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTowerBase ,hit.collider.gameObject.transform.position.z);
			Instantiate(availableTowers[0], tempPosBase, hit.collider.gameObject.transform.rotation);
			hit.collider.gameObject.GetComponent<GridSquare>().canMove = false;
			hit.collider.gameObject.GetComponent<GridSquare>().canBuild = false;
			hit.collider.gameObject.GetComponent<GridSquare>().hasTowerBase = true;
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
			if(hit.collider != null && hit.collider.gameObject.GetComponent<GridSquare>().canBuild && !hit.collider.gameObject.GetComponent<GridSquare>().hasTowerBase)
			{				
				hit.collider.gameObject.GetComponent<GridSquare>().canMove = true;
			}
			Pathfinder.FindPath();
			lastGridSquare = Pathfinder.infinityGridSquare;
		}
		if(tTemp != null)
		{
			Destroy(tTemp);
		}		
	}
	
}
