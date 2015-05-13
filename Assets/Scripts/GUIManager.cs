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
	public static Camera miniMapCamera;
	
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
		ShowTowerInterface (false);
		//miniMapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{		
		
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

	public static void UpdateTowerInterface(string name, string type, bool upgrade)
	{
		towerInterface.transform.FindChild ("TowerName").GetComponent<Text> ().text = name;
		towerInterface.transform.FindChild ("TypeDisplay").GetComponent<Text> ().text = type;
		towerInterface.transform.FindChild("UpgradeBarBack").gameObject.SetActive(upgrade);
		towerInterface.transform.FindChild("UpgradeBarFill").gameObject.SetActive(upgrade);
		towerInterface.transform.FindChild("Upgrade").gameObject.SetActive(upgrade);
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
