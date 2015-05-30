using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour 
{

	static GUIManager instance_;
	public static GUIManager Instance
	{
		get
		{
			if (instance_ == null) {
				instance_ = new GUIManager();
			}
			return instance_;
		}
	}
	
	public static List<GameObject> towerChoices = new List<GameObject>();
	public static List<GameObject> towerSelectors = new List<GameObject>();
	public static GameObject towerInterface;
	public static GameObject buildPhaseGUI;
	public static GameObject miniMapCamera, mapCamera;
	public static GameObject buildReadyDisplay, defendReadyDisplay, nextWaveEnemies;
	
	// Use this for initialization
	void Awake () 
	{
		GameObject[] tc = GameObject.FindGameObjectsWithTag("TowerChoose");
		towerChoices.AddRange(tc);
		towerChoices.Sort(CompareListByName);
		GameObject[] tch = GameObject.FindGameObjectsWithTag("TowerChooseHighlight");
		towerSelectors.AddRange(tch);
		towerSelectors.Sort (CompareListByName);
		towerInterface = GameObject.Find("TowerInterface");
		buildPhaseGUI = GameObject.Find("BuildPhaseGUI");
		buildReadyDisplay = GameObject.Find("BuildReadyText");
		defendReadyDisplay = GameObject.Find("DefendReadyText");
		nextWaveEnemies = GameObject.Find("NextWaveEnemies");
		ShowTowerInterface (false);
		miniMapCamera = GameObject.Find("MiniMapCamera");
		mapCamera = GameObject.Find("MapCamera");
		mapCamera.GetComponent<Camera>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if(Input.GetKeyDown(KeyCode.M))
		{
			ToggleMaps();
		}
	}
	
	public static void ShowCrosshair()
	{
		GameObject.Find("Crosshair").GetComponent<Image>().enabled = true;
	}
	
	public static void HideCrosshair()
	{
		GameObject.Find("Crosshair").GetComponent<Image>().enabled = false;
	}
	
	public static void ShowTowerChoices()
	{
		buildPhaseGUI.SetActive(true);
		for(int i = 0; i < towerChoices.Count; i++)
		{	
			if(i > GameManager.currentPlayer.GetComponent<TowerPlacer>().availableTowers.Count-1)
			{
				break;
			}	
			towerChoices[i].GetComponent<Image>().sprite = GameManager.currentPlayer.GetComponent<TowerPlacer>().availableTowers[i].GetComponent<Tower>().sprite;
			towerSelectors[i].SetActive(false);
		}
		towerSelectors[0].SetActive(true);
	}
	
	public static void HideTowerChoices()
	{
		buildPhaseGUI.SetActive(false);
	}
	
	public static void ChangeSelectedTower(int t)
	{
		for(int i = 0; i < towerChoices.Count; i++)
		{
			towerSelectors[i].SetActive(false);
		}
		towerSelectors[t].SetActive(true);
	}

	public static void ChangeCurrencyDisplay(float c)
	{
		GameObject cc = buildPhaseGUI.transform.FindChild("CurrentCurrency").gameObject;
		cc.GetComponent<Text>().text = "*v^*: " + c;
	}
	
	public static void ChangeTowerBaseDisplay(float c)
	{
		GameObject cc = buildPhaseGUI.transform.FindChild("TowerBaseAvailableDisplay").gameObject;
		cc.GetComponent<Text>().text = "" + c;
	}
	
	public static void ShowTowerInterface(bool b)
	{
		towerInterface.SetActive (b);
	}
	
	public static void ShowBuildReadyText(bool b)
	{
		buildReadyDisplay.SetActive(b);
	}
	
	public static void ShowDefendReadyText(bool b)
	{
		defendReadyDisplay.SetActive(b);
	}

	public static void UpdateTowerInterface(string name, string type, bool upgrade)
	{
		towerInterface.transform.FindChild ("TowerName").GetComponent<Text> ().text = name;
		towerInterface.transform.FindChild ("TypeDisplay").GetComponent<Text> ().text = type;
		towerInterface.transform.FindChild("UpgradeBarBack").gameObject.SetActive(upgrade);
		towerInterface.transform.FindChild("UpgradeBarFill").gameObject.SetActive(upgrade);
		towerInterface.transform.FindChild("Upgrade").gameObject.SetActive(upgrade);
	}
	
	public static void ShowNextWaveEnemies(bool b)
	{
		nextWaveEnemies.SetActive(b);
		if(b)
		{
			if(GameManager.spawnerOfWaves.bruisersInNextWave > 0)
			{
				nextWaveEnemies.transform.FindChild("BruiserImage").gameObject.SetActive(true);
				nextWaveEnemies.transform.FindChild("BruiserImage").transform.FindChild("Count").GetComponent<Text>().text = GameManager.spawnerOfWaves.bruisersInNextWave.ToString();
			}
			else
			{
				nextWaveEnemies.transform.FindChild("BruiserImage").gameObject.SetActive(false);
			}
			if(GameManager.spawnerOfWaves.bulwarksInNextWave > 0)
			{
				nextWaveEnemies.transform.FindChild("BulwarkImage").gameObject.SetActive(true);
				nextWaveEnemies.transform.FindChild("BulwarkImage").transform.FindChild("Count").GetComponent<Text>().text = GameManager.spawnerOfWaves.bulwarksInNextWave.ToString();
			}
			else
			{
				nextWaveEnemies.transform.FindChild("BulwarkImage").gameObject.SetActive(false);
			}
			if(GameManager.spawnerOfWaves.dashersInNextWave > 0)
			{
				nextWaveEnemies.transform.FindChild("DasherImage").gameObject.SetActive(true);
				nextWaveEnemies.transform.FindChild("DasherImage").transform.FindChild("Count").GetComponent<Text>().text = GameManager.spawnerOfWaves.dashersInNextWave.ToString();
			}
			else
			{
				nextWaveEnemies.transform.FindChild("DasherImage").gameObject.SetActive(false);
			}
			if(GameManager.spawnerOfWaves.sprintersInNextwave > 0)
			{
				nextWaveEnemies.transform.FindChild("SprinterImage").gameObject.SetActive(true);
				nextWaveEnemies.transform.FindChild("SprinterImage").transform.FindChild("Count").GetComponent<Text>().text = GameManager.spawnerOfWaves.sprintersInNextwave.ToString();
			}
			else
			{
				nextWaveEnemies.transform.FindChild("SprinterImage").gameObject.SetActive(false);
			}
			if(GameManager.spawnerOfWaves.tanksInNextWave > 0)
			{
				nextWaveEnemies.transform.FindChild("TankImage").gameObject.SetActive(true);
				nextWaveEnemies.transform.FindChild("TankImage").transform.FindChild("Count").GetComponent<Text>().text = GameManager.spawnerOfWaves.tanksInNextWave.ToString();
			}
			else
			{
				nextWaveEnemies.transform.FindChild("TankImage").gameObject.SetActive(false);
			}
		}		
	}
	
	public static void ToggleMaps()
	{
		if(miniMapCamera.GetComponent<Camera>().enabled)
		{
			miniMapCamera.GetComponent<Camera>().enabled = false;
			mapCamera.GetComponent<Camera>().enabled = true;
		}
		else if(mapCamera.GetComponent<Camera>().enabled)
		{
			miniMapCamera.GetComponent<Camera>().enabled = false;
			mapCamera.GetComponent<Camera>().enabled = false;
		}
		else if(!miniMapCamera.GetComponent<Camera>().enabled && !mapCamera.GetComponent<Camera>().enabled)
		{
			miniMapCamera.GetComponent<Camera>().enabled = true;
			mapCamera.GetComponent<Camera>().enabled = false;
		}
	}
	
	public static void MoveBar(string bar, float amount)
	{
		towerInterface.transform.FindChild(bar+"BarFill").GetComponent<Image>().fillAmount = amount;
	}
	
	private static int CompareListByName(GameObject i1, GameObject i2)
	{
		return i1.name.CompareTo(i2.name); 
	}
}
