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
	Animation anim;
	public EnemyAttackAnimation enemyAttack;
	public EnemyDeathAnimation enemyDeath;

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
			//EnemyAnimDied();
            Destroy(gameObject, 1.5f);
            GameManager.enemiesOnField--;
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
				//EnemyAnimAtk();
            }
            if (target.gameObject.GetComponent<Gate>() != null)
            {
                target.gameObject.GetComponent<Gate>().gateHealthCurrent -= damage;
				playerSound.playPlayerSounds(b, 0, 0.25f);
				//EnemyAnimAtk();
            }
        }
    }
	void EnemyAnimAtk ()
	{
		switch(enemyAttack)
		{
		case EnemyAttackAnimation.Bruiser: 
			anim.Play("bruiser_attack_animation");
			break;
		case EnemyAttackAnimation.Sprinter: 
			anim.Play("sprinter_attack_animation");
			break;
		case EnemyAttackAnimation.Dasher: 
			anim.Play("dasher_attack_animation");
			break;
		case EnemyAttackAnimation.Bulwark: 
			anim.Play("bulwark_attack_animation");
			break;
		case EnemyAttackAnimation.Tank: 
			anim.Play("tank_attack_animation");
			break;
		default:
			break;
		}
	}
	void EnemyAnimDied ()
	{
		switch(enemyDeath)
		{
		case EnemyDeathAnimation.Bruiser:
			anim.Play("bruiser_death_animation");
			break;
		case EnemyDeathAnimation.Sprinter:
			anim.Play("sprinter_death_animation");
			break;
		case EnemyDeathAnimation.Dasher:
			anim.Play("dasher_death_animation");
			break;
		case EnemyDeathAnimation.Bulwark:
			anim.Play("bulwark_death_animation");
			break;
		case EnemyDeathAnimation.Tank:
			anim.Play("tank_death_animation");
			break;
		default:
			break;
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
public enum EnemyAttackAnimation
{
	Bruiser,
	Sprinter,
	Dasher,
	Bulwark,
	Tank
};
public enum EnemyDeathAnimation
{
	Bruiser,
	Sprinter,
	Dasher,
	Bulwark,
	Tank
};
//public enum EnemyType
//{
//    Normal,
//    Corrosive,
//    Flame,
//    Electric,
//    Spook,
//    Crystal
//};