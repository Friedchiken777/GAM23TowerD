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
	public static GameObject currentPlayer;
	public GameState levelStartingState;

	
	// Use this for initialization
	void Start () 
	{
		currentPlayer = GameObject.FindGameObjectWithTag("Player");
		currentState = levelStartingState;
		StartingState();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
			MakeBuildPhase();
		}
		if(Input.GetKeyDown(KeyCode.N))
		{
			MakeDefensePhase();
			
		}
	}
	
	
	static void StartingState()
	{
		switch(currentState)
		{
		case GameState.BuildPhase:
		{
			MakeBuildPhase();
			break;
		}
		case GameState.DefensePhase:
		{
			MakeDefensePhase();
			break;
		}
		case GameState.LevelSelect:
		{
			MakeLevelSelect();
			break;
		}
		case GameState.LoadingScreen:
		{
			MakeLoadingScreen();
			break;
		}
		case GameState.Loadout:
		{
			MakeLoadout();
			break;
		}
		case GameState.LossScreen:
		{
			MakeLossScreen();
			break;
		}
		case GameState.MainMenue:
		{
			MakeMainMenue();
			break;
		}
		case GameState.Pause:
		{
			MakePause();
			break;
		}
		case GameState.WinScreen:
		{
			MakeWinScreen();
			break;
		}
		default:
		{
			Debug.LogError("No Gamestate Present");
			break;
		}
		}
	}
	
	public static void MakeBuildPhase()
	{
		currentState = GameState.BuildPhase;
		GUIManager.ShowTowerChoices();
		GUIManager.ShowCrosshair();
		currentPlayer.GetComponent<TDCharacterController>().SetArms(true);
		currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
	}
	
	public static void MakeDefensePhase()
	{
		currentState = GameState.DefensePhase;
		GUIManager.HideTowerChoices();
		GUIManager.ShowCrosshair();
		currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
		currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(true);
	}
	
	public static void MakeMainMenue()
	{
		GUIManager.HideTowerChoices();
		GUIManager.HideCrosshair();
		currentState = GameState.MainMenue;
		if(currentPlayer != null)
		{
			currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
			currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
		}
	}
	
	public static void MakeLevelSelect()
	{
		GUIManager.HideTowerChoices();
		GUIManager.HideCrosshair();
		currentState = GameState.LevelSelect;
		if(currentPlayer != null)
		{
			currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
			currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
		}
	}
	
	public static void MakeLoadout()
	{
		GUIManager.HideTowerChoices();
		GUIManager.HideCrosshair();
		currentState = GameState.Loadout;
		if(currentPlayer != null)
		{
			currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
			currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
		}
	}
	
	public static void MakePause()
	{
		GUIManager.HideTowerChoices();
		GUIManager.HideCrosshair();
		currentState = GameState.Pause;
		if(currentPlayer != null)
		{
			currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
			currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
		}
	}
	
	public static void MakeLoadingScreen()
	{
		GUIManager.HideTowerChoices();
		GUIManager.HideCrosshair();
		currentState = GameState.LoadingScreen;
		if(currentPlayer != null)
		{
			currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
			currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
		}
	}
	
	public static void MakeWinScreen()
	{
		GUIManager.HideTowerChoices();
		GUIManager.HideCrosshair();
		currentState = GameState.WinScreen;
		if(currentPlayer != null)
		{
			currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
			currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
		}
	}
	
	public static void MakeLossScreen()
	{
		GUIManager.HideTowerChoices();
		GUIManager.HideCrosshair();
		currentState = GameState.LossScreen;
		if(currentPlayer != null)
		{
			currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
			currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
		}
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
	WinScreen,
	LossScreen
}
