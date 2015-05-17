using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour 
{
    public int gateHealth = 100;
    public float damageTimer = 0.0f;
    public bool isDamaged = false;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
    void Update()
    {
        if (isDamaged == true)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 2.0f)
            {
                gateHealth -= 5;
                damageTimer = 0.0f;
            }
        }
        if (isDamaged == false)
        {
            damageTimer = 0.0f;
        }
        if (gateHealth <= 0)
        {
            Application.LoadLevel("GameOver");
        }
    }
}
