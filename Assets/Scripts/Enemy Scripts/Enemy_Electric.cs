using UnityEngine;
using System.Collections;

public class Enemy_Electric : MonoBehaviour 
{
    public int CORROSIVE_DAMAGE = 2;
    public int ELECTRIC_DAMAGE = 1 / 3;
    public int FLAME_DAMAGE = 1;
    public int SPOOK_DAMAGE = 1;
    public int CRYSTAL_DAMAGE = 2;
    public int health = 10;
    public NavMeshAgent navMeshEnemy;
    public AudioClip[] audioClip;

    // Use this for initialization
    void Start()
    {
        navMeshEnemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CorrosiveProjectile")
        {
            health -= CORROSIVE_DAMAGE;
        }
        if (other.gameObject.tag == "ElectricProjectile")
        {
            health -= ELECTRIC_DAMAGE;
        }
        if (other.gameObject.tag == "FlameProjectile")
        {
            health -= FLAME_DAMAGE;
        }
        if (other.gameObject.tag == "SpookProjectile")
        {
            health -= SPOOK_DAMAGE;
        }
        if (other.gameObject.tag == "CrystalProjectile")
        {
            health -= CRYSTAL_DAMAGE;
        }
    }
    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip[clip]);
    }
}
