using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public int health = 20;
    public float attackTimer = 0.0f;
    public EnemyType enemyType;
    public Transform target;
    public AudioClip[] audioClip;

	// Use this for initialization
	void Start () 
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip[clip]);
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