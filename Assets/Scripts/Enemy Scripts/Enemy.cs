using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public int health = 20;
    public float attackRate = 0.0f;
    public bool isAttacking = false;
    public EnemyType enemyType;
    public Transform target;
    public float rate;
    public GameObject[] particlePlacements;
    public AudioClip[] audioClip;

	// Use this for initialization
	void Start () 
    {
        EnemySetup();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (health <= 0)
        {
            Destroy(gameObject);
			GameManager.enemiesOnField --;
        }
        Attack();
	}
    void Attack()
    {
        attackRate += Time.deltaTime;
        if (isAttacking == true)
        {
            if (attackRate >= rate)
            {

                attackRate = 0.0f;
            }
        }
        if (isAttacking == false)
        {
            attackRate = 0.0f;
        }
    }
    // Player damaging enemy
    void OnCollisionEnter (Collision other)
    {
        switch (enemyType)
        {
            case EnemyType.Corrosive:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {
                        health -= 1;
                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {
                        health -= 4;
                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {
                        health -= 4;
                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {
                        health -= 3;
                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {
                        health -= 3;
                    }
                }
                break;
            case EnemyType.Flame:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {
                        health -= 3;
                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {
                        health -= 1;
                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {
                        health -= 3;
                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {
                        health -= 4;
                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {
                        health -= 4;
                    }
                }
                break;
            case EnemyType.Electric:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {
                        health -= 3;
                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {
                        health -= 4;
                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {
                        health -= 1;
                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {
                        health -= 4;
                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {
                        health -= 3;
                    }
                }
                break;
            case EnemyType.Spook:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {
                        health -= 4;
                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {
                        health -= 3;
                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {
                        health -= 3;
                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {
                        health -= 1;
                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {
                        health -= 4;
                    }
                }
                break;
            case EnemyType.Crystal:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {
                        health -= 4;
                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {
                        health -= 3;
                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {
                        health -= 4;
                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {
                        health -= 3;
                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {
                        health -= 1;
                    }
                }
                break;
            default:
                    if (other.gameObject.tag == "NormalProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {
                        health -= 2;
                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {
                        health -= 2;
                    }
                break;
        }
    }

    // Enemy Attack is Triggered
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Gate")
        {
            isAttacking = true;
        }
    }

    // Enemy Attack is not triggered or untriggered
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Gate")
        {
            isAttacking = false;
        }
    }
    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip[clip]);
    }

    // Enemy Setup for Particle Systems and Attack Rate for spawning
    public void EnemySetup ()
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
    Normal,
    Corrosive,
    Flame,
    Electric,
    Spook,
    Crystal
};