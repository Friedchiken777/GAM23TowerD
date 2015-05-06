using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public int health = 20;
    public float attackTimer = 0.0f;
    public EnemyType enemyType;
    public Transform target;
    public GameObject[] particlePlacements;
    public AudioClip[] audioClip;

	// Use this for initialization
	void Start () 
    {
        SetParticles();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}
    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip[clip]);
    }
    public void SetParticles ()
    {
        switch(enemyType)
        {
            case EnemyType.Corrosive: 
                {
                    for (int i = 0; i < particlePlacements.Length; i++)
                    {
                       GameObject parent = (GameObject) Instantiate(Resources.Load("ParticleSystem_Corrosive"), particlePlacements[i].transform.position, particlePlacements[i].transform.rotation);
                       parent.transform.parent = transform;
                    }
                        break; 
                }
            case EnemyType.Flame:
                {
                    for (int i = 0; i < particlePlacements.Length; i++)
                    {
                        GameObject parent = (GameObject)Instantiate(Resources.Load("ParticleSystem_Flame"), particlePlacements[i].transform.position, particlePlacements[i].transform.rotation);
                        parent.transform.parent = transform;
                    }
                    break;
                }
            case EnemyType.Electric:
                {
                    for (int i = 0; i < particlePlacements.Length; i++)
                    {
                        GameObject parent = (GameObject)Instantiate(Resources.Load("ParticleSystem_Electric"), particlePlacements[i].transform.position, particlePlacements[i].transform.rotation);
                        parent.transform.parent = transform;
                    }
                    break;
                }
            case EnemyType.Spook:
                {
                    for (int i = 0; i < particlePlacements.Length; i++)
                    {
                        GameObject parent = (GameObject)Instantiate(Resources.Load("ParticleSystem_Spook"), particlePlacements[i].transform.position, particlePlacements[i].transform.rotation);
                        parent.transform.parent = transform;
                    }
                    break;
                }
            case EnemyType.Crystal:
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
public enum EnemyType
{
    None,
    Corrosive,
    Flame,
    Electric,
    Spook,
    Crystal
};