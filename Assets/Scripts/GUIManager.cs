using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour 
{
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		CrosshairLogic();
		
		if(Input.GetKeyDown(KeyCode.B))
		{
			MakeBuildPhase();
		}
		if(Input.GetKeyDown(KeyCode.N))
		{
			MakeDefensePhase();
		}
	}
	
	void CrosshairLogic()
	{
		if(GameManager.currentState == GameState.DefensePhase || GameManager.currentState == GameState.BuildPhase)
		{
			GameObject.Find("Crosshair").GetComponent<Image>().enabled = true;
		}
		else
		{
			GameObject.Find("Crosshair").GetComponent<Image>().enabled = false;
		}
	}
	
	public void MakeBuildPhase()
	{
		GameManager.currentState = GameState.BuildPhase;
	}
	
	public void MakeDefensePhase()
	{
		GameManager.currentState = GameState.DefensePhase;
	}
}
