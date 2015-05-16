using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPlacer : MonoBehaviour 
{
	public LayerMask buildStuff, gridSquaresOnly;
	
	public GameObject lastGridSquare;
	
	public List<GameObject> availableTowers, towersBad, towersGood;
	int currentTower;
	public float yBufferTowerBase, yBufferTower, yBufferTowerOffBase;
	GameObject tb = null;
	GameObject tTemp = null;
	bool buildOnClick = false;
	bool towerOnBaseBuild = false;
	bool upgradeIsGo = false;
	bool baseSellIsGo;
	bool selling = false;
	bool upgrading = false;
	float actionDelay, actionDelayTB;
	GameObject towerUpgradeSell;
	GameObject baseSell;
	RaycastHit hit;
	TDCharacterController player;
	
	// Use this for initialization
	void Start () 
	{
		lastGridSquare = Pathfinder.infinityGridSquare;
		player = gameObject.GetComponent<TDCharacterController>();
		GUIManager.ChangeCurrencyDisplay(player.currentCurrency);
		GUIManager.ChangeTowerBaseDisplay(player.currentTowerBases);
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
		towerOnBaseBuild = false;
		upgradeIsGo = false;
		baseSellIsGo = false;
		GUIManager.ShowTowerInterface(false);
		if(Physics.Raycast(lookAtRay, out hit, 100, buildStuff))
		{
			GameObject currentSquare = hit.collider.gameObject;
			if(hit.collider.gameObject.tag == "GridSquare")
			{				
				bool badBase = false;
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
						if((Pathfinder.FindPath() && player.currentTowerBases > 0) && (currentTower == 0 || player.currentCurrency >= availableTowers[currentTower].GetComponent<Tower>().cost))
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
								tb = Instantiate(towersBad[0], tempPosBase, currentSquare.transform.rotation) as GameObject;
								badBase = true;
							}
							buildOnClick = false;
							
						}
					}
					if(currentTower > 0 && towersGood[currentTower] != null && !currentSquare.GetComponent<GridSquare>().hasTower)
					{
						tempPosTower = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTower ,hit.collider.gameObject.transform.position.z);
						if(badBase || player.currentCurrency < availableTowers[currentTower].GetComponent<Tower>().cost)
						{
							tTemp = Instantiate(towersBad[currentTower], tempPosTower, currentSquare.transform.rotation) as GameObject;
							buildOnClick = false;
						}
						else
						{
							tTemp = Instantiate(towersGood[currentTower], tempPosTower, currentSquare.transform.rotation) as GameObject;
							buildOnClick = true;
						}						
					}
					Pathfinder.FindPath();
					if(!currentSquare.GetComponent<GridSquare>().hasTowerBase)
					{
						currentSquare.GetComponent<GridSquare>().canMove = true;
					}
				}
				else if(currentSquare != lastGridSquare && !currentSquare.GetComponent<GridSquare>().canBuild)
				{
					lastGridSquare = currentSquare;
					LittleClearFunction();
					buildOnClick = false;
				}
			}
			else if(hit.collider.gameObject.tag == "TowerBase")
			{
				buildOnClick = false;
				if(lastGridSquare.GetComponent<GridSquare>() != null && lastGridSquare.GetComponent<GridSquare>().canBuild)
				{
					lastGridSquare.GetComponent<GridSquare>().canMove = true;
					Pathfinder.FindPath();
				}
				if(currentSquare != lastGridSquare)
				{
					if(tb != null)
					{
						Destroy(tb);
					}
					if(tTemp != null)
					{
						Destroy(tTemp);
					}	
					RaycastHit tempHit;
					Physics.Raycast(hit.collider.transform.position, Vector3.down, out tempHit);
					if(currentTower > 0 && towersGood[currentTower] != null && !tempHit.collider.gameObject.GetComponent<GridSquare>().hasTower  && tempHit.collider.gameObject.GetComponent<GridSquare>().hasTowerBase)
					{
						if(player.currentCurrency >= availableTowers[currentTower].GetComponent<Tower>().cost)
						{
							tempPosTower = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTowerOffBase ,hit.collider.gameObject.transform.position.z);
							tTemp = Instantiate(towersGood[currentTower], tempPosTower, hit.collider.transform.rotation) as GameObject;
							towerOnBaseBuild = true;
						}
						else
						{
							tempPosTower = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTowerOffBase ,hit.collider.gameObject.transform.position.z);
							tTemp = Instantiate(towersBad[currentTower], tempPosTower, hit.collider.transform.rotation) as GameObject;
							towerOnBaseBuild = false;
						}
					}
				}
				RaycastHit tempHit2;
				Physics.Raycast(hit.collider.transform.position, Vector3.down, out tempHit2);
				if(!tempHit2.collider.gameObject.GetComponent<GridSquare>().hasTower)
				{
					baseSell = hit.collider.gameObject;
					GUIManager.ShowTowerInterface(true);
					GUIManager.UpdateTowerInterface("Tower Base", "None", false);
					baseSellIsGo = true;
				}
			}
			if(hit.collider.gameObject.tag == "Tower")
			{
				towerUpgradeSell = hit.collider.gameObject;
				GUIManager.ShowTowerInterface(true);
				GUIManager.UpdateTowerInterface(hit.collider.gameObject.GetComponent<Tower>().towerName, TypeToString(hit.collider.gameObject.GetComponent<Tower>().towerType), hit.collider.gameObject.GetComponent<Tower>().upgradeTower != null);
				upgradeIsGo = true;
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
				player.currentCurrency -= availableTowers[currentTower].GetComponent<Tower>().cost;
				GUIManager.ChangeCurrencyDisplay(player.currentCurrency);
				hit.collider.gameObject.GetComponent<GridSquare>().hasTower = true;
				Destroy(tTemp);
			}
			if(!hit.collider.gameObject.GetComponent<GridSquare>().hasTowerBase)
			{
				tempPosBase = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTowerBase ,hit.collider.gameObject.transform.position.z);
				Instantiate(availableTowers[0], tempPosBase, hit.collider.gameObject.transform.rotation);
				player.currentTowerBases --;
				GUIManager.ChangeTowerBaseDisplay(player.currentTowerBases);
				hit.collider.gameObject.GetComponent<GridSquare>().canMove = false;
				hit.collider.gameObject.GetComponent<GridSquare>().canBuild = false;
				hit.collider.gameObject.GetComponent<GridSquare>().hasTowerBase = true;
			}
			buildOnClick = false;
		}
		else if(towerOnBaseBuild && Input.GetMouseButtonDown(0))
		{
			//Destroy(tb);
			tempPosTower = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yBufferTowerOffBase ,hit.collider.gameObject.transform.position.z);
			Instantiate(availableTowers[currentTower], tempPosTower, hit.collider.gameObject.transform.rotation);
			player.currentCurrency -= availableTowers[currentTower].GetComponent<Tower>().cost;
			GUIManager.ChangeCurrencyDisplay(player.currentCurrency);
			RaycastHit tempHit;
			if(Physics.Raycast(hit.collider.transform.position, Vector3.down, out tempHit))
			{
				if(tempHit.collider.gameObject.tag == "GridSquare")
				{				
					tempHit.collider.gameObject.GetComponent<GridSquare>().hasTower = true;
				}
			}
			Destroy(tTemp);
		}
		if(upgradeIsGo)
		{
			if(Input.GetMouseButton(0) && towerUpgradeSell.GetComponent<Tower>().upgradeTower != null && towerUpgradeSell.GetComponent<Tower>().upgradeTower.GetComponent<Tower>().cost <= player.currentCurrency && !selling)
			{
				upgrading = true;
				actionDelay += Time.deltaTime;
				if(actionDelay < 2.5f)
				{
					GUIManager.MoveBar("Upgrade", actionDelay/2.5f);
				
				}
				else
				{
					upgrading = false;
					actionDelay = 0;
					GUIManager.MoveBar("Upgrade", actionDelay/2.5f);
					GameObject tempUp = Instantiate(towerUpgradeSell.GetComponent<Tower>().upgradeTower, towerUpgradeSell.transform.position, towerUpgradeSell.transform.rotation) as GameObject;
					Destroy(towerUpgradeSell);
					player.currentCurrency -= tempUp.GetComponent<Tower>().cost;
					GUIManager.ChangeCurrencyDisplay(player.currentCurrency);
				}
			}
			if(Input.GetMouseButton(1) && !upgrading)
			{
				selling = true;
				actionDelay += Time.deltaTime;
				if(actionDelay < 2.5f)
				{
					GUIManager.MoveBar("Sell", actionDelay/2.5f);
					
				}
				else
				{
					selling = false;
					actionDelay = 0;
					GUIManager.MoveBar("Sell", actionDelay/2.5f);
					player.currentCurrency += towerUpgradeSell.GetComponent<Tower>().totalValue;
					GUIManager.ChangeCurrencyDisplay(player.currentCurrency);
					RaycastHit tempHit;
					if(Physics.Raycast(towerUpgradeSell.transform.position, Vector3.down, out tempHit, 100, gridSquaresOnly))
					{	
						tempHit.collider.gameObject.GetComponent<GridSquare>().hasTower = false;
					}
					Destroy (towerUpgradeSell);
				}
			}
			if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				upgrading = selling = false;
				actionDelay = 0;
				GUIManager.MoveBar("Upgrade", actionDelay/2.5f);
				GUIManager.MoveBar("Sell", actionDelay/2.5f);
			}
		}
		else
		{
			actionDelay = 0;
			selling = false;
			upgrading = false;
		}
		
		if(baseSellIsGo)
		{
			if(Input.GetMouseButton(1))
			{
				actionDelayTB += Time.deltaTime;
				if(actionDelayTB < 2.5f)
				{
					GUIManager.MoveBar("Sell", actionDelayTB/2.5f);
					
				}
				else
				{
					actionDelayTB = 0;
					GUIManager.MoveBar("Sell", actionDelayTB/2.5f);
					player.currentTowerBases ++;
					GUIManager.ChangeTowerBaseDisplay(player.currentTowerBases);
					RaycastHit tempHit;
					if(Physics.Raycast(baseSell.transform.position, Vector3.down, out tempHit))
					{
						if(tempHit.collider.gameObject.tag == "GridSquare")
						{				
							tempHit.collider.gameObject.GetComponent<GridSquare>().hasTowerBase = false;
							tempHit.collider.gameObject.GetComponent<GridSquare>().canBuild = true;
							tempHit.collider.gameObject.GetComponent<GridSquare>().canMove = true;
						}
					}
					Destroy (baseSell);
				}
			}
			if(Input.GetMouseButtonUp(1))
			{
				actionDelayTB = 0;
				GUIManager.MoveBar("Sell", actionDelayTB/2.5f);
			}
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
		if(currentTower > availableTowers.Count - 1)
		{
			currentTower = 0;
		}
		lastGridSquare = Pathfinder.infinityGridSquare;
		BuildPhaseGO ();
	}
	
	void LittleClearFunction()
	{
		if(tb != null)
		{
			Destroy(tb);
			if(hit.collider != null && (hit.collider.gameObject.GetComponent<GridSquare>().canBuild || hit.collider.gameObject.GetComponent<GridSquare>().hasTowerBase))
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

	string TypeToString(TowerType t)
	{
		switch (t) 
		{
		case TowerType.Flame:
		{
			return "Flame";
		}
		case TowerType.Electric:
		{
			return "Electric";
		}
		case TowerType.Corrosive:
		{
			return "Corrosive";
		}
		case TowerType.Crystal:
		{
			return "Crystal";
		}
		case TowerType.Spook:
		{
			return "Spook";
		}
		default:
		{
			return "Normal";
		}
		}
	}
}
