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
    public float radius;
    public LayerMask firstTarget;
    public Vector3 center;
	public GameObject target;
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
        Fire();
    }
    void Fire()
    {
        firingRate += Time.deltaTime;
        if (firingRate >= rate)
        {
            target = FindTargetWithinReach(center, radius, firstTarget);
            if (target == null)
            {
                target = FindTargetWithinReach(center, radius, firstTarget);
            }
            else
            {
                transform.LookAt(target.transform);
                foreach (GameObject gunPlacement in gunPlacements)
                {
                    Instantiate(projectile, gunPlacement.transform.position, gunPlacement.transform.rotation);
                }
				firingRate = 0.0f;
            }			
        }
    }
    GameObject FindTargetWithinReach(Vector3 center, float radius, LayerMask firstTarget)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(center, radius, firstTarget);
        if (hitEnemies.Length > 0)
        {
            return hitEnemies[0].gameObject;
        }
        else
        {
            return null;
        }
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
