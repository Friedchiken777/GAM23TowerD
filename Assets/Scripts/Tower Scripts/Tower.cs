using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Tower : MonoBehaviour 
{
	public string towerName;
	public float towerBaseDamage;
	public float damageModifier = 1;
    public int rank;
    public float range;
    public float cost;
    public float upgrade1Cost;
    public float upgrade2Cost;
    public float totalValue;
	public float firingRate;
	public float firingRateModifier = 1;
	public GameObject[] gunPlacements;
    public GameObject[] projectiles;
    public int projectileIndex;
    public GameObject upgradeTower;
	public DamageType towerType;
	public bool supportTower;
	public float towerDamageMultiplier;
	public float towerFiringRateMultiplier;
	public float towerRangeMultiplier;
	public bool buffed;
    public float rate;
    public float radius;
    public float raduisModifier = 1;
    public LayerMask firstTarget;
    public Vector3 center;
	public GameObject target;
    public GameObject[] xLookAt;
    public GameObject[] yLookAt;
    public AudioClip[] audioClip;
    public Sprite sprite;
    

	// Use this for initialization
	void Start () 
    {
        SetTowerType();
        buffed = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        
            Fire();
        
    }
    void Fire()
    {
        firingRate += Time.deltaTime;
        if ((firingRate * firingRateModifier) >= rate)
        {
            target = FindTargetWithinReach(transform.position, radius, firstTarget);
            if (target == null)
            {
				target = FindTargetWithinReach(transform.position, radius, firstTarget);
            }
            else
            {
                foreach (GameObject gunPlacement in gunPlacements)
                {
                    GameObject proj = Instantiate(projectiles[projectileIndex], gunPlacement.transform.position, gunPlacement.transform.rotation) as GameObject;
					proj.GetComponent<Projectile>().damage = towerBaseDamage * damageModifier;
                }
				firingRate = 0.0f;
            }			
        }
        if (target != null)
        {
            Vector3 enemy = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            for (int towerPartsX = 0; towerPartsX < xLookAt.Length; towerPartsX++)
            {
                xLookAt[towerPartsX].transform.LookAt(enemy);
            }
            for (int towerPartsY = 0; towerPartsY < yLookAt.Length; towerPartsY++)
            {
                yLookAt[towerPartsY].transform.LookAt(target.transform.position);
            }
        }
    }
    GameObject FindTargetWithinReach(Vector3 center, float radius, LayerMask firstTarget)
    {
		Collider[] hitEnemies = Physics.OverlapSphere(center, (radius * raduisModifier), firstTarget);
        if (hitEnemies.Length > 0)
        {
            return hitEnemies[0].gameObject;
        }
        else
        {
            return null;
        }
    }


    public void SetTowerType()
    {
		if(projectileIndex > projectiles.Length)
		{
			print(projectileIndex);
			projectileIndex = 0;
		}
		switch (projectileIndex)
        {
		case 1:
        {
			towerType = DamageType.Corrosive;
            break;
        }
		case 2:
        {
			towerType = DamageType.Flame;
            break;
        }
		case 3:
        {
			towerType = DamageType.Electric;
            break;
        }
		case 4:
        {
			towerType = DamageType.Spook;
            break;
        }
		case 5:
        {
			towerType = DamageType.Crystal;
            break;
        }
        default:
        {
			towerType = DamageType.Normal;
        	break;
        }
        }
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip[clip]);
    }
}

//public enum TowerType
//{
//    Normal,
//    Corrosive,
//    Flame,
//    Electric,
//    Spook,
//    Crystal
//};
