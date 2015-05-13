using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour 
{
    public int health = 100;
    public float damageTimer = 0.0f;
    public bool isDamaged = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (isDamaged == true)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1.0f)
            {
                health -= 1;
                damageTimer = 0.0f;
            }
        }
        if (isDamaged == false)
        {
            damageTimer = 0.0f;
        }
        if (health <= 0)
        {
            Application.LoadLevel("GameOver");
        }
	}
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isDamaged = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isDamaged = false;
        }
    }
}
