using UnityEngine;
using System.Collections;

public class TowerSlow : Tower
{
    
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.LookAt(target);
        firingTimer += Time.deltaTime;
        if (firingTimer >= 1.0f)
        {
            foreach (GameObject gunPlacement in gunPlacements)
            {
                Instantiate(projectile, gunPlacement.transform.position, gunPlacement.transform.rotation);
            }
            firingTimer = 0.0f;
        }
	}
}
