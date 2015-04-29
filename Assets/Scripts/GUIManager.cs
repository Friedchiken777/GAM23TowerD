using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour 
{

	GameManager gm;
	
	// Use this for initialization
	void Start () 
	{
		gm = GetComponent<GameManager>();
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
		if(gm.currentState == GameState.DefensePhase || gm.currentState == GameState.BuildPhase)
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
		GetComponent<GameManager>().currentState = GameState.BuildPhase;
	}
	
	public void MakeDefensePhase()
	{
		GetComponent<GameManager>().currentState = GameState.DefensePhase;
	}
}
