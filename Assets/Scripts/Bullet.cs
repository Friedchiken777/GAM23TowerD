using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public float deathTime;
	Vector3 mousePos;
	public Camera cam;
	public float bulletSpeed;
	GameObject player;
	Vector3 playerVelocity;
	bool playedSound, noMoreHurt;
	public float damage;
	public DamageType dt;
	
	// Use this for initialization
	void Start () 
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerVelocity = player.GetComponent<CharacterController>().velocity;
		if(this.transform.parent)
		{
			Destroy (this.transform.parent.gameObject,deathTime);
		}
		else
		{
			Destroy (this.gameObject,deathTime);
		}
		Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width*0.5f, Screen.height*0.5f));
		mousePos = ray.direction;
		if(playerVelocity.x <= 0)
		{
			playerVelocity.x = 0;
		}
		if(playerVelocity.z <= 0)
		{
			playerVelocity.z = 0;
		}
		this.GetComponent<Rigidbody>().velocity += new Vector3(playerVelocity.x,0,playerVelocity.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
        
            transform.position += mousePos.normalized * Time.deltaTime * bulletSpeed;
            //transform.Rotate(Time.deltaTime * 100, Time.deltaTime * 100, 0);
        
	}
	
	void OnTriggerEnter(Collider other)
	{
		
		if(other.tag == "Enemy")
		{
			other.GetComponent<Enemy>().health -= GameManager.TypeCheckDamageAdjustment(damage, other.gameObject.GetComponent<Enemy>().enemyType, dt);
		}
		if(other.tag != "Tower" || other.tag != "TowerBase" || other.tag != "NormalProjectile" || other.tag != "Player")
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
