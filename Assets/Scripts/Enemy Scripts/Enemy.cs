using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public float health = 20;
    public float attackRate = 0.0f;
    public bool isAttacking = false;
    public DamageType enemyType;
    public Transform target;
    public float rate;
    public float damage;
    public GameObject[] particlePlacements;
    public AudioManager playerSound;
    public AudioSource b;

	// Use this for initialization
	void Start () 
    {
        EnemySetup();
        playerSound = GameObject.Find("_AudioManager").GetComponent<AudioManager>();
        b = playerSound.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (health <= 0)
        {
            Destroy(gameObject);
			GameManager.enemiesOnField --;
        }
        attackRate += Time.deltaTime;
	}
    public void Attack(GameObject target)
    {
        if (attackRate >= rate)
        {
            attackRate = 0.0f;
            if (target.gameObject.GetComponent<TDCharacterController>() != null)
            {
                target.gameObject.GetComponent<TDCharacterController>().currentHealth -= damage;
				playerSound.playPlayerSounds(b, 3, 0.25f);

            }
            if (target.gameObject.GetComponent<Gate>() != null)
            {
                target.gameObject.GetComponent<Gate>().gateHealthCurrent -= damage;
				playerSound.playPlayerSounds(b, 0, 0.25f);
            }
        }
    }


    // Enemy Setup for Particle Systems and Attack Rate for spawning
    public void EnemySetup ()
    {
        switch(enemyType)
        {
			case DamageType.Corrosive: 
                {
                    for (int i = 0; i < particlePlacements.Length; i++)
                    {
                       GameObject parent = (GameObject) Instantiate(Resources.Load("ParticleSystem_Corrosive"), particlePlacements[i].transform.position, particlePlacements[i].transform.rotation);
                       parent.transform.parent = transform;
                    }
                        break; 
                }
			case DamageType.Flame:
                {
                    for (int i = 0; i < particlePlacements.Length; i++)
                    {
                        GameObject parent = (GameObject)Instantiate(Resources.Load("ParticleSystem_Flame"), particlePlacements[i].transform.position, particlePlacements[i].transform.rotation);
                        parent.transform.parent = transform;
                    }
                    break;
                }
			case DamageType.Electric:
                {
                    for (int i = 0; i < particlePlacements.Length; i++)
                    {
                        GameObject parent = (GameObject)Instantiate(Resources.Load("ParticleSystem_Electric"), particlePlacements[i].transform.position, particlePlacements[i].transform.rotation);
                        parent.transform.parent = transform;
                    }
                    break;
                }
			case DamageType.Spook:
                {
                    for (int i = 0; i < particlePlacements.Length; i++)
                    {
                        GameObject parent = (GameObject)Instantiate(Resources.Load("ParticleSystem_Spook"), particlePlacements[i].transform.position, particlePlacements[i].transform.rotation);
                        parent.transform.parent = transform;
                    }
                    break;
                }
            case DamageType.Crystal:
                {
                    for (int i = 0; i < particlePlacements.Length; i++)
                    {
                        GameObject parent = (GameObject)Instantiate(Resources.Load("ParticleSystem_Crystal"), particlePlacements[i].transform.position, particlePlacements[i].transform.rotation);
                        parent.transform.parent = transform;
                    }
                    break;
                }
            default:
                break;
        }
    }
}

//public enum EnemyType
//{
//    Normal,
//    Corrosive,
//    Flame,
//    Electric,
//    Spook,
//    Crystal
//};