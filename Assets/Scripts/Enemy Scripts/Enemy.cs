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
    void OnTriggerEnter (Collider other)
    {
        switch (enemyType)
        {
            case EnemyType.Corrosive:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {

                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {

                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {

                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {

                    }
                }
                break;
            case EnemyType.Flame:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {

                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {

                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {

                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {

                    }
                }
                break;
            case EnemyType.Electric:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {

                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {

                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {

                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {

                    }
                }
                break;
            case EnemyType.Spook:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {

                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {

                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {

                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {

                    }
                }
                break;
            case EnemyType.Crystal:
                {
                    if (other.gameObject.tag == "NormalProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {

                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {

                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {

                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {

                    }
                }
                break;
            default:
                if (other.gameObject.tag == "NormalProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CorrosiveProjectile")
                    {

                    }
                    if (other.gameObject.tag == "FlameProjectile")
                    {

                    }
                    if (other.gameObject.tag == "ElectricProjectile")
                    {

                    }
                    if (other.gameObject.tag == "SpookProjectile")
                    {

                    }
                    if (other.gameObject.tag == "CrystalProjectile")
                    {

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