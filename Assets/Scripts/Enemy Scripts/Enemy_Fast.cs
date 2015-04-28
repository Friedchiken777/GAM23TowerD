using UnityEngine;
using System.Collections;

public class Enemy_Fast : Enemy
{
    public float speed = 10.0f;
    public bool isAttacking = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.LookAt(target);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (isAttacking == true)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= 1.0f)
            {
                attackTimer = 0.0f;
            }
        }
        if (isAttacking == false)
        {
            attackTimer = 0.0f;
        }
	}
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAttacking = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAttacking = false;
        }
    }
}
