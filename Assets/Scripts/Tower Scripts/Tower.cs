using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tower : MonoBehaviour 
{
	public string towerName;
	public int health = 20;
    public int rank;
    public float range;
    public float cost;
    public float upgrade1Cost;
    public float upgrade2Cost;
    public float totalValue;
	public float firingRate = 0.0f;
	public GameObject[] gunPlacements;
    public GameObject projectile;
    public GameObject upgradeTower;
    public TowerType towerType;
    public float rate;
	public Transform target;
    public AudioClip[] audioClip;
    public Sprite sprite;

	// Use this for initialization
	void Start () 
    {
        SetTowers();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        transform.LookAt(target);
        Fire();

    }
    void Fire()
    {
        firingRate += Time.deltaTime;
        if (firingRate >= rate)
        {
            foreach (GameObject gunPlacement in gunPlacements)
            {
                Instantiate(projectile, gunPlacement.transform.position, gunPlacement.transform.rotation);
            }
            firingRate = 0.0f;
        }
    }
    void OnTriggerStay(Collider other)
    {
        
    }

    public void SetTowers()
    {
        switch (towerType)
        {
            case TowerType.Corrosive:
                {

                    break;
                }
            case TowerType.Flame:
                {

                    break;
                }
            case TowerType.Electric:
                {

                    break;
                }
            case TowerType.Spook:
                {

                    break;
                }
            case TowerType.Crystal:
                {

                    break;
                }
            default:
 
                break;
        }
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip[clip]);
    }
}

public enum TowerType
{
    Normal,
    Corrosive,
    Flame,
    Electric,
    Spook,
    Crystal
};
