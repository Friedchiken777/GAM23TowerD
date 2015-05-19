using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour 
{
	public AudioClip[] gameMusicTracks;
	public AudioClip[] playerSounds;
	public AudioClip[] towerSounds;
	public AudioClip[] explosions;
	public AudioClip[] cannonSounds;
	public AudioClip[] weaponSounds;
	public AudioClip[] enemySounds;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void playGameMusicTracks (AudioSource a, int index, float volume = 0.25f)
	{
		if (index < gameMusicTracks.Length) 
		{
			a.PlayOneShot (gameMusicTracks [index], volume);
		} 
		else 
		{
			print ("No Track Selected");
		}
	}

	public void playPlayerSounds (AudioSource b, int index, float volume = 1.0f)
	{
		if (index < playerSounds.Length) 
		{
			b.PlayOneShot (playerSounds [index], volume);
		} 
		else 
		{
			print ("No Track Selected");
		}
	}

	public void playTowerSounds (AudioSource c, int index, float volume = 1.0f)
	{
		if (index < towerSounds.Length) 
		{
			c.PlayOneShot (towerSounds [index], volume);
		} 
		else 
		{
			print ("No Track Selected");
		}
	}

	public void playExplosions (AudioSource d, int index, float volume = 1.0f)
	{
		if (index < explosions.Length) 
		{
			d.PlayOneShot (explosions [index], volume);
		} 
		else 
		{
			print ("No Track Selected");
		}
	}

	public void playCannonSounds (AudioSource e, int index, float volume = 1.0f)
	{
		if (index < cannonSounds.Length) 
		{
			e.PlayOneShot (cannonSounds [index], volume);
		} 
		else 
		{
			print ("No Track Selected");
		}
	}

	public void playWeaponSounds (AudioSource f, int index, float volume = 1.0f)
	{
		if (index < weaponSounds.Length) 
		{
			f.PlayOneShot (weaponSounds [index], volume);
		} 
		else 
		{
			print ("No Track Selected");
		}
	}

	public void playEnemySounds (AudioSource g, int index, float volume = 1.0f)
	{
		if (index < enemySounds.Length) 
		{
			g.PlayOneShot (enemySounds [index], volume);
		} 
		else 
		{
			print ("No Track Selected");
		}
	}
}
