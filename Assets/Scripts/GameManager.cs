using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GameState currentState;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

public enum GameState
{
	MainMenue,
	LevelSelect,
	Loadout,
	Pause,
	BuildPhase,
	DefensePhase,
	LoadingScreen,
	WinScreen
}
