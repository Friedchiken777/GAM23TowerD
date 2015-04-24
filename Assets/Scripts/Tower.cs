using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour 
{
    public int CORROSIVE_DAMAGE = 1;
    public int ELECTRIC_DAMAGE = 2;
    public int FLAME_DAMAGE = 2;
    public int SPOOK_DAMAGE = 1 / 3;
    public int CRYSTAL_DAMAGE = 1;
    public int health = 20;
    public GameObject projectile;
    public AudioClip[] audioClip;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (health <= 0)
        {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "CorrosiveEnemy")
        {
            health -= 1 / 3;
        }
        if (other.gameObject.tag == "FlameEnemy")
        {
            health -= 1;
        }
        if (other.gameObject.tag == "ElectricEnemy")
        {
            health -= 1;
        }
        if (other.gameObject.tag == "SpookEnemy")
        {
            health -= 2;
        }
        if (other.gameObject.tag == "CrystalEnemy")
        {
            health -= 2;
        }
    }
    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip[clip]);
    }
}
