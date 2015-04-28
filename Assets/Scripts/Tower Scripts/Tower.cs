using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour 
{
    public int health = 20;
    public float firingTimer = 0.0f;
    public TowerType towerType;
    public GameObject projectile;
    public Transform target;
    public GameObject[] gunPlacements;
    public AudioClip[] audioClip;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerStay(Collider other)
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
public enum TowerType
{
    None,
    Corrosive,
    Flame,
    Electric,
    Spook,
    Crystal
};
