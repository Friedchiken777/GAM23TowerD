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
	
	// Use this for initialization
	void Awake () 
	{
		GameObject[] tc = GameObject.FindGameObjectsWithTag("TowerChoose");
		towerChoices.AddRange(tc);
		towerChoices.Sort(CompareListByName);
		GameObject[] tch = GameObject.FindGameObjectsWithTag("TowerChooseHighlight");
		towerSelectors.AddRange(tch);
		towerSelectors.Sort (CompareListByName);
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
		for(int i = 0; i < towerChoices.Count; i++)
		{
			towerChoices[i].SetActive(true);
			//towerChoices[i].GetComponent<Image>().sprite = GameManager.currentPlayer.GetComponent<TowerPlacer>().availableTowers[i].GetComponent<Tower>().sprite;
		}
		towerSelectors[0].SetActive(true);
	}
	
	public static void HideTowerChoices()
	{
		for(int i = 0; i < towerSelectors.Count; i++)
		{
			towerChoices[i].SetActive(false);
		}
		for(int i = 0; i < towerChoices.Count; i++)
		{
			towerSelectors[i].SetActive(false);
		}
	}
	
	public static void ChangeSelectedTower(int t)
	{
		for(int i = 0; i < towerChoices.Count; i++)
		{
			towerSelectors[i].SetActive(false);
		}
		towerSelectors[t].SetActive(true);
	}
	
	private static int CompareListByName(GameObject i1, GameObject i2)
	{
		return i1.name.CompareTo(i2.name); 
	}
}
