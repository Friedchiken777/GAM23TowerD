using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    public float speed = 10.0f;
    public float damage;
	public DamageType dt;

	// Use this for initialization
	void Start () 
    {
        Destroy(gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
	
	void OnTriggerEnter(Collider other)
	{
		
		if(other.tag == "Enemy")
		{
			other.GetComponent<Enemy>().health -= GameManager.TypeCheckDamageAdjustment(damage, other.gameObject.GetComponent<Enemy>().enemyType, dt);
		}
		if(other.tag != "Tower" || other.tag != "TowerBase" || other.tag != "NormalProjectile")
		{
			if(this.transform.parent)
			{
				
				Destroy (this.transform.parent.gameObject, 0.4f);
				
			}
			else
			{
				Destroy (this.gameObject, 0.4f);
			}
		}
	}
}
