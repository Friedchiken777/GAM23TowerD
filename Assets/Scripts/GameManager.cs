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
	public static WaveSpawner spawnerOfWaves;
	public static int enemiesOnField;
	public static AudioManager gameTrack;
	public static AudioSource a;

	// Use this for initialization
	void Start () 
	{
		currentPlayer = GameObject.FindGameObjectWithTag("Player");
		spawnerOfWaves = GetComponent<WaveSpawner>();
		enemiesOnField = 0;
		currentState = levelStartingState;
		gameTrack = GameObject.Find("_AudioManager").GetComponent<AudioManager>();
		a = gameTrack.gameObject.GetComponent<AudioSource>();
		StartingState();
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if(Input.GetKeyDown(KeyCode.B))
//		{
//			
//			MakeBuildPhase();
//			
//		}
//		if(Input.GetKeyDown(KeyCode.N))
//		{
//
//			MakeDefensePhase();
//		}
		
		switch(currentState)
		{
		case GameState.BuildPhase:
		{
            DoBuildPhase();
            if (!a.isPlaying)
            {
                gameTrack.playGameMusicTracks(a, 0, 0.25f);
            }
			break;
		}
		case GameState.DefensePhase:
		{
			DoDefensePhase();
            if (!a.isPlaying)
            {
                gameTrack.playGameMusicTracks(a, 1, 0.25f);
            }
			break;
		}
		case GameState.LevelSelect:
		{
			DoLevelSelect();
			break;
		}
		case GameState.LoadingScreen:
		{
			DoLoadingScreen();
			break;
		}
		case GameState.Loadout:
		{
			DoLoadout();
			break;
		}
		case GameState.LossScreen:
		{
			DoLossScreen();
			break;
		}
		case GameState.MainMenue:
		{
			DoMainMenue();
			break;
		}
		case GameState.Pause:
		{
			DoPause();
			break;
		}
		case GameState.WinScreen:
		{
			DoWinScreen();
			break;
		}
		default:
		{
			Debug.LogError("No Gamestate Present");
			break;
		}
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
		GUIManager.ShowDefendReadyText(true);
		GUIManager.ShowBuildReadyText(false);
		GUIManager.ChangeCurrencyDisplay(currentPlayer.GetComponent<TDCharacterController>().currentCurrency);
		GUIManager.ChangeTowerBaseDisplay(currentPlayer.GetComponent<TDCharacterController>().currentTowerBases);
		currentPlayer.GetComponent<TDCharacterController>().SetArms(true);
		currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
		gameTrack.playGameMusicTracks(a, 0, 0.25f);
	}
	
	public static void MakeDefensePhase()
	{
		enemiesOnField = 0;
		currentState = GameState.DefensePhase;
		GUIManager.HideTowerChoices();
		GUIManager.ShowCrosshair();
		GUIManager.ShowTowerInterface(false);
		GUIManager.ShowDefendReadyText(false);
		GUIManager.ShowBuildReadyText(false);
		currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
		currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(true);
		spawnerOfWaves.LoadWave();
		gameTrack.playGameMusicTracks(a, 1, 0.25f);
	}
	
	public static void MakeMainMenue()
	{
		GUIManager.HideTowerChoices();
		GUIManager.HideCrosshair();
		GUIManager.ShowTowerInterface(false);
		GUIManager.ShowDefendReadyText(false);
		GUIManager.ShowBuildReadyText(false);
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
		GUIManager.ShowTowerInterface(false);
		GUIManager.ShowDefendReadyText(false);
		GUIManager.ShowBuildReadyText(false);
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
		GUIManager.ShowTowerInterface(false);
		GUIManager.ShowDefendReadyText(false);
		GUIManager.ShowBuildReadyText(true);
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
		GUIManager.ShowTowerInterface(false);
		GUIManager.ShowDefendReadyText(false);
		GUIManager.ShowBuildReadyText(false);
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
		GUIManager.ShowTowerInterface(false);
		GUIManager.ShowDefendReadyText(false);
		GUIManager.ShowBuildReadyText(false);
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
		GUIManager.ShowTowerInterface(false);
		GUIManager.ShowDefendReadyText(false);
		GUIManager.ShowBuildReadyText(false);
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
		GUIManager.ShowTowerInterface(false);
		GUIManager.ShowDefendReadyText(false);
		GUIManager.ShowBuildReadyText(false);
		currentState = GameState.LossScreen;
		if(currentPlayer != null)
		{
			currentPlayer.GetComponent<TDCharacterController>().SetArms(false);
			currentPlayer.GetComponent<TDCharacterController>().weapon.SetActive(false);
		}
	}
	
	public static void DoBuildPhase()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			MakeDefensePhase();
		}
	}
	
	public static void DoDefensePhase()
	{
		if(WaveSpawner.allWaveEnemiesSpawned && enemiesOnField <= 0)
		{
			if(WaveSpawner.lastWave)
			{
				MakeWinScreen();
			}
			else
			{
				MakeBuildPhase();
			}			
		}
	}
	
	public static void DoMainMenue()
	{
		print ("No Main Menue Logic");
	}
	
	public static void DoLevelSelect()
	{
		print ("No Level Select Logic");
	}
	
	public static void DoLoadout()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			MakeBuildPhase();
		}
	}
	
	public static void DoPause()
	{
		print ("No Pause Logic");
	}
	
	public static void DoLoadingScreen()
	{
		print ("No Loading Screen Logic");
	}
	
	public static void DoWinScreen()
	{
		print ("No Win Screen Logic");
	}
	
	public static void DoLossScreen()
	{
		print ("No Loss Screen Logic");
	}
	
	public static float TypeCheckDamageAdjustment(float unmodedDamage, DamageType defendertype, DamageType attackertype)
	{
		switch (attackertype)
		{
		case DamageType.Corrosive:
		{
			if (defendertype == DamageType.Normal)
			{
				unmodedDamage *= 1.1f;
			}
			else if (defendertype == DamageType.Corrosive)
			{
				unmodedDamage *= 0.3f;
			}
			else if (defendertype == DamageType.Crystal)
			{
				unmodedDamage *= 1f;
			}
			else if (defendertype == DamageType.Electric)
			{
				unmodedDamage *= 2f;
			}
			else if (defendertype == DamageType.Flame)
			{
				unmodedDamage *= 2f;
			}
			else if (defendertype == DamageType.Spook)
			{
				unmodedDamage *= 1f;
			}
			return unmodedDamage;
		}
		case DamageType.Flame:
		{
			if (defendertype == DamageType.Normal)
			{
				unmodedDamage *= 1.1f;
			}
			else if (defendertype == DamageType.Corrosive)
			{
				unmodedDamage *= 1f;
			}
			else if (defendertype == DamageType.Crystal)
			{
				unmodedDamage *= 2f;
			}
			else if (defendertype == DamageType.Electric)
			{
				unmodedDamage *= 1f;
			}
			else if (defendertype == DamageType.Flame)
			{
				unmodedDamage *= 0.3f;
			}
			else if (defendertype == DamageType.Spook)
			{
				unmodedDamage *= 2f;
			}
			return unmodedDamage;
		}
		case DamageType.Electric:
		{
			if (defendertype == DamageType.Normal)
			{
				unmodedDamage *= 1.1f;
			}
			else if (defendertype == DamageType.Corrosive)
			{
				unmodedDamage *= 1f;
			}
			else if (defendertype == DamageType.Crystal)
			{
				unmodedDamage *= 1f;
			}
			else if (defendertype == DamageType.Electric)
			{
				unmodedDamage *= 0.3f;
			}
			else if (defendertype == DamageType.Flame)
			{
				unmodedDamage *= 2f;
			}
			else if (defendertype == DamageType.Spook)
			{
				unmodedDamage *= 2f;
			}
			return unmodedDamage;
		}
		case DamageType.Spook:
		{
			if (defendertype == DamageType.Normal)
			{
				unmodedDamage *= 1.1f;
			}
			else if (defendertype == DamageType.Corrosive)
			{
				unmodedDamage *= 2f;
			}
			else if (defendertype == DamageType.Crystal)
			{
				unmodedDamage *= 2f;
			}
			else if (defendertype == DamageType.Electric)
			{
				unmodedDamage *= 1f;
			}
			else if (defendertype == DamageType.Flame)
			{
				unmodedDamage *= 1f;
			}
			else if (defendertype == DamageType.Spook)
			{
				unmodedDamage *= 0.3f;
			}
			return unmodedDamage;
		}
		case DamageType.Crystal:
		{
			if (defendertype == DamageType.Normal)
			{
				unmodedDamage *= 1.1f;
			}
			else if (defendertype == DamageType.Corrosive)
			{
				unmodedDamage *= 2f;
			}
			else if (defendertype == DamageType.Crystal)
			{
				unmodedDamage *= 0.3f;
			}
			else if (defendertype == DamageType.Electric)
			{
				unmodedDamage *= 2f;
			}
			else if (defendertype == DamageType.Flame)
			{
				unmodedDamage *= 1f;
			}
			else if (defendertype == DamageType.Spook)
			{
				unmodedDamage *= 1f;
			}
			return unmodedDamage;
		}
		default:
		{
			return unmodedDamage;
		}
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

public enum DamageType
{
	Normal,
	Corrosive,
	Flame,
	Electric,
	Spook,
	Crystal
};
