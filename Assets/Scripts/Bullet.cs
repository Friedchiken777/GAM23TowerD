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
	
	// Use this for initialization
	void Start () 
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		player = GameObject.Find("PlayerTD");
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
			if(Vector3.Distance(other.transform.position, player.transform.position) < 4)
			{
				
			}
			if(!noMoreHurt)
			{
				
			}
		}
		if(other.gameObject.tag != "Player")
		{
			if(!playedSound)
			{
				//GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
				playedSound = true;
				noMoreHurt = true;
			}
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
