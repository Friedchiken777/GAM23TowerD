using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class WaveSpawner : MonoBehaviour 
{
	WaveData n = new WaveData ();
	public WaveData test = new WaveData ();
	public string[] teststring;
	public int testStringIndex;
	
	string dPath;
	string filename = "level ";
	int savefiles;

	public GameObject prefabTest;
	
	DirectoryInfo dir;
	FileInfo[] info;
	
	public string fileToLoad;
	
	public float waitTime;
	public static bool allWaveEnemiesSpawned, lastWave;
	
	public float bruisersInNextWave, bulwarksInNextWave, dashersInNextWave, sprintersInNextwave, tanksInNextWave;
	

	void Start () 
	{
		dPath = Application.dataPath + "/";
		dir = new DirectoryInfo(Application.dataPath);
		info = dir.GetFiles(filename + "*.xml");
		savefiles = info.Length;
		//Debug.Log (savefiles);
		//SaveGame ();
		testStringIndex = 0;
		lastWave = false;
		LoadLevel ();
	}
	
	void Update()
	{
		
	}
	
	void SaveGame()
	{
		savefiles = info.Length + 1;
		if(savefiles < 7)
		{
			XMLizer<WaveData>.CreateXMLGeneric (n, dPath + filename + "0" + savefiles + ".xml");
		}
		else{
			XMLizer<WaveData>.CreateXMLGeneric (n, dPath + filename + savefiles + ".xml");
		}
	}
	
	void LoadGame(string loadfile)
	{
		if(savefiles == 7)
		{
			Debug.Log("No Save Files Yet!");
		}
		else{
			//Debug.Log (loadfile+" load");
			test = XMLizer<WaveData>.ReadXMLGeneric (loadfile);
		}
	}
	void LoadLevel()
	{
			LoadGame (dPath+""+fileToLoad);
			teststring = test.wave.enemyToSpawn;
			//print (teststring.Length);			
	}
	
	public void LoadWave()
	{
		StartCoroutine(WaveDelay());
	}
	
	IEnumerator WaveDelay()
	{
		GameObject newEnemy;
		allWaveEnemiesSpawned = false;
		for (int i=testStringIndex; i < teststring.Length; i++)
		{
			yield return new WaitForSeconds(waitTime);
			string[] enemyData = teststring[i].Split(',');
			string enemyToLoad = enemyData[0];
			string enemyType = enemyData[1];
			waitTime = float.Parse(enemyData[2]);
			//print ("Enemy: "+enemyToLoad+" Type: "+enemyType+" Delay: "+waitTime);
			newEnemy = (GameObject) Instantiate (Resources.Load("Enemies/" + enemyToLoad), Pathfinder.start.GetComponent<GridSquare>().pathMarker.transform.position, Pathfinder.start.GetComponent<GridSquare>().pathMarker.transform.rotation);
			newEnemy.GetComponent<Enemy>().enemyType = DetermineType(enemyType);
			GameManager.enemiesOnField ++;
			if(GameManager.currentState == GameState.WinScreen)
			{
				i = teststring.Length;
			}
			if(waitTime > 9000)
			{
				lastWave = true;
			}
			if(waitTime > 1000)
			{
				int currencyToGive = (int)waitTime;
				float towerBasesToGive = waitTime - currencyToGive;
				towerBasesToGive =  Mathf.Round(towerBasesToGive * 100)/100;
				towerBasesToGive *= 100;
				GameManager.currentPlayer.GetComponent<TDCharacterController>().currentCurrency += currencyToGive;
				GameManager.currentPlayer.GetComponent<TDCharacterController>().currentTowerBases += towerBasesToGive;
				testStringIndex = i+1;
				waitTime = 1;
				allWaveEnemiesSpawned = true;
				break;
			}
		}
		yield return null;
	}
	
	public void DetermineNumberOfEnemiesForNextWave()
	{
		float endcheck;
		sprintersInNextwave = tanksInNextWave = bulwarksInNextWave = bruisersInNextWave = dashersInNextWave = 0;
		for (int i=testStringIndex; i < teststring.Length; i++)
		{
			string[] enemyData = teststring[i].Split(',');
			string nextTallyCheck = enemyData[0];
			
			switch(nextTallyCheck)
			{
			case "enemy_bruiser_asset":
			{
				bruisersInNextWave++;
				break;
			}
			case "enemy_bulwark_asset":
			{
				bulwarksInNextWave++;
				break;
			}
			case "enemy_dasher_asset":
			{
				dashersInNextWave++;
				break;
			}
			case "enemy_sprinter_asset":
			{
				sprintersInNextwave++;
				break;
			}
			case "enemy_tank_asset":
			{
				tanksInNextWave++;
				break;
			}
			default:
			{
				print ("Warning, Invalid Enemy...");
				break;
			}
			}
			
			endcheck = float.Parse(enemyData[2]);
			if(endcheck > 1000)
			{
				break;
			}
		}
	}

	DamageType DetermineType(string stringtype)
	{
		switch (stringtype) 
		{
		case "Flame":
		{
			return DamageType.Flame;
		}
		case "Electric":
		{
			return DamageType.Electric;
		}
		case "Corrosive":
		{
			return DamageType.Corrosive;
		}
		case "Crystal":
		{
			return DamageType.Crystal;
		}
		case "Spook":
		{
			return DamageType.Spook;
		}
		default:
		{
			return DamageType.Normal;
		}
		}
	}
	
	// UserData is our custom class that holds our defined objects we want to store in XML format 
	public class WaveData 
	{ 
		// We have to define a default instance of the structure 
		public WaveDataStruct wave; 
		// Default constructor doesn't really do anything at the moment 
		public WaveData() { } 
		
		// Anything we want to store in the XML file, we define it here 
		public struct WaveDataStruct 
		{ 
			public int waveNumber;
			public string[] enemyToSpawn; 
		} 
	}
}
