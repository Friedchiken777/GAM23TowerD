using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class WaveSpawner : MonoBehaviour 
{
	WaveData n = new WaveData ();
	public WaveData test = new WaveData ();
	public string[] teststring;
	
	string dPath;
	string filename = "level ";
	int savefiles;

	public GameObject prefabTest;
	
	private bool firstPlay;
	
	DirectoryInfo dir;
	FileInfo[] info;
	
	public string fileToLoad;
	
	public float waitTime;
	

	void Start () 
	{
		dPath = Application.dataPath + "/";
		dir = new DirectoryInfo(Application.dataPath);
		info = dir.GetFiles(filename + "*.xml");
		savefiles = info.Length;
		//Debug.Log (savefiles);
		//SaveGame ();
		firstPlay = true;
	}
	
	void Update()
	{
		LoadSong ();
	}
	
	void SaveGame()
	{
		savefiles = info.Length + 1;
		if(savefiles < 7)
		{
			string xml = XMLizer<WaveData>.CreateXMLGeneric (n, dPath + filename + "0" + savefiles + ".xml");
		}
		else{
			string xml = XMLizer<WaveData>.CreateXMLGeneric (n, dPath + filename + savefiles + ".xml");
		}
	}
	
	void LoadGame(string loadfile)
	{
		if(savefiles == 7)
		{
			Debug.Log("No Save Files Yet!");
		}
		else{
			Debug.Log (loadfile+" load");
			test = XMLizer<WaveData>.ReadXMLGeneric (loadfile);
		}
	}
	void LoadSong()
	{
		if (GameManager.currentState == GameState.DefensePhase && firstPlay == true)
		{
			firstPlay = false;
			LoadGame (dPath+""+fileToLoad);
			teststring = test.wave.enemyToSpawn;
			print (teststring.Length);
			StartCoroutine(SongDelay());
		}
	}
	
	IEnumerator SongDelay()
	{
		GameObject notePath;
		Animator animator;
		for (int i=0; i < teststring.Length; i++)
		{
			yield return new WaitForSeconds(waitTime);
			notePath = (GameObject) Instantiate (Resources.Load(teststring[i]), Pathfinder.start.transform.position, Pathfinder.start.transform.rotation);
			animator = notePath.GetComponent<Animator>();
			if(GameManager.currentState == GameState.WinScreen)
			{
				i = teststring.Length;
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
