using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	static GameManager instance_;
	public static GameManager Instance
	{
		get
		{
			if (instance_ == null) {
				instance_ = new GameManager();
			}
			return instance_;
		}
	}
	
	public static GameState currentState;
	

	
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
