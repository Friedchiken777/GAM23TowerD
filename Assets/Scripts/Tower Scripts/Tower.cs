using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour 
{
    public int health = 20;
	public float firingRate = 0.0f;
	public GameObject[] gunPlacements;
    public GameObject projectile;
    public TowerType towerType;
    public FireRate rate;
	public Transform target;
    public AudioClip[] audioClip;

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
        switch (rate)
        {
            case FireRate.Slow:
                {
                    firingRate += Time.deltaTime;
                    if (firingRate >= 1.0f)
                    {
                        foreach (GameObject gunPlacement in gunPlacements)
                        {
                            Instantiate(projectile, gunPlacement.transform.position, gunPlacement.transform.rotation);
                        }
                        firingRate = 0.0f;
                    }
                    break;
                }
            case FireRate.Fast:
                {
                    firingRate += Time.deltaTime;
                    if (firingRate >= 0.25f)
                    {
                        foreach (GameObject gunPlacement in gunPlacements)
                        {
                            Instantiate(projectile, gunPlacement.transform.position, gunPlacement.transform.rotation);
                        }
                        firingRate = 0.0f;
                    }
                    break;
                }
            default:
                firingRate += Time.deltaTime;
                    if (firingRate >= 0.5f)
                    {
                        foreach (GameObject gunPlacement in gunPlacements)
                        {
                            Instantiate(projectile, gunPlacement.transform.position, gunPlacement.transform.rotation);
                        }
                        firingRate = 0.0f;
                    }
                break;
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
                    switch (rate)
                    {
                        case FireRate.Slow:
                            {

                                break;
                            }
                        case FireRate.Fast:
                            {

                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case TowerType.Flame:
                {
                    switch (rate)
                    {
                        case FireRate.Slow:
                            {

                                break;
                            }
                        case FireRate.Fast:
                            {

                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case TowerType.Electric:
                {
                    switch (rate)
                    {
                        case FireRate.Slow:
                            {

                                break;
                            }
                        case FireRate.Fast:
                            {

                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case TowerType.Spook:
                {
                    switch (rate)
                    {
                        case FireRate.Slow:
                            {

                                break;
                            }
                        case FireRate.Fast:
                            {

                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case TowerType.Crystal:
                {
                    switch (rate)
                    {
                        case FireRate.Slow:
                            {

                                break;
                            }
                        case FireRate.Fast:
                            {

                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            default:
                switch (rate)
                {
                    case FireRate.Slow:
                        {

                            break;
                        }
                    case FireRate.Fast:
                        {

                            break;
                        }
                    default:
                        break;
                }
                break;
        }
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip[clip]);
    }
}

public enum FireRate
{
    Slow,
    Normal,
    Fast
};

public enum TowerType
{
    Normal,
    Corrosive,
    Flame,
    Electric,
    Spook,
    Crystal
};
