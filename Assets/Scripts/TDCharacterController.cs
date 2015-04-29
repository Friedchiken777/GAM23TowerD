﻿/// <summary>
/// TDCharacterController.cs
/// Written by William George (with lots of internet help...)
/// Basically just a custom first person controller :)
/// </summary>

using UnityEngine;
using System.Collections;

public class TDCharacterController : MonoBehaviour {

	CharacterController characterControler;

	public Camera cam;
	public GameManager gm;
	public GameState gameState;
	
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationX = 0F;
	float rotationY = 0F;
	
	Quaternion originalRotation, origionalPlayerRotation;
	Quaternion xQuaternion;
	Quaternion yQuaternion;

	public float gravity;
	public float fallGravity;
	public float jumpHeight;					
	public bool jump;									
	int jumpGroundClear;
	public bool grounded = false;
	public int jumps;
	public int totalJumps;
	public float walkSpeed;	

	public Vector3 moveDirec = Vector3.zero;
	float horizontal = 0;
	float vertical = 0;
	
	public GameObject[] bullets;
	public GameObject gun;
	GameObject weapon;
	public int ammo, ammoShotType;
	
	public float maxHealth, currentHealth;
	
	public float playerDamage;

	
	void Start ()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		gm = GameObject.Find ("_GameManager").GetComponent<GameManager>();
		gameState = gm.currentState;
		gun = GameObject.Find("SpawnBullet");
		weapon = cam.transform.FindChild("Weapon").gameObject;
		jump = true;
		currentHealth = maxHealth;
		characterControler = this.GetComponent<CharacterController> ();
		originalRotation = cam.transform.localRotation;
		origionalPlayerRotation = transform.localRotation;		
	}
	
	void Update ()
	{
		gameState = gm.currentState;
		
		if(gameState == GameState.DefensePhase)
		{
			weapon.SetActive(true);
		}
		else
		{
			weapon.SetActive(false);
		}
	
		rotationX += Input.GetAxis("Mouse X") * sensitivityX/10;
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY/10;
		
		rotationX = ClampAngle (rotationX, minimumX, maximumX);
		rotationY = ClampAngle (rotationY, minimumY, maximumY);
		
		xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
		yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
		
		cam.transform.localRotation = originalRotation  * yQuaternion;
		transform.transform.localRotation = origionalPlayerRotation * xQuaternion;

		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
		

		if(((vertical > 0.01f && horizontal > 0.01f) || (vertical < -0.01f && horizontal < -0.01f) 
		    || (vertical > 0.01f && horizontal < -0.01f) || (vertical < -0.01f && horizontal > 0.01f)))
		{
			moveDirec.x = (horizontal * walkSpeed * this.transform.right.x + vertical * walkSpeed * this.transform.forward.x)/Mathf.Sqrt(2);
			moveDirec.z = (horizontal * walkSpeed * this.transform.right.z + vertical * walkSpeed * this.transform.forward.z)/Mathf.Sqrt(2);
		}
		else
		{
			if((vertical > 0.01f || vertical < -0.01f))
			{		
				moveDirec.x = vertical * walkSpeed * this.transform.forward.x;
				moveDirec.z = vertical * walkSpeed * this.transform.forward.z;
			}
			if ((horizontal > 0.01f || horizontal < -0.01f)) 
			{
				moveDirec.x = horizontal * walkSpeed * this.transform.right.x;
				moveDirec.z = horizontal * walkSpeed * this.transform.right.z;
			}
		}

		if((vertical < 0.01f && vertical > -0.01f) && (horizontal < 0.01f && horizontal > -0.01f))
		{
			moveDirec.x = 0;
			moveDirec.z = 0;
		}

		grounded = characterControler.isGrounded;

		if (Input.GetButtonDown ("Jump")) 
		{
			if(jumps < totalJumps)
			{
				moveDirec.y = jumpHeight;
				jump = true;
				jumpGroundClear = 0;
				jumps++;
			}
		}

		if (jump && grounded)
		{
			if(jumpGroundClear++ > 3)
			{
				jump = false;
				jumps = 0;
			}
		}
		if (!jump && !grounded)
		{
			moveDirec.y -= fallGravity * Time.deltaTime;
		}
		else
		{
			moveDirec.y += gravity * Time.deltaTime;
		}

		if(characterControler.enabled)
		{
			characterControler.Move (moveDirec * Time.deltaTime);
		}
		
		if(ammo > 0 && gameState == GameState.DefensePhase)
		{
			if(Input.GetMouseButtonDown(0))
			{
				ammoShotType = Random.Range(0,bullets.Length);
				Instantiate(bullets[0], gun.transform.position, gun.transform.localRotation);
				//GetComponent<AudioSource>().PlayOneShot(gun.GetComponent<AudioSource>().clip);
				ammo --;
			}
		}
		
		if(Input.GetKey(KeyCode.R)||Input.GetMouseButtonDown(1))
		{
			ammo = 10;
		}
		
		
		if(currentHealth < 0)
		{			
			print ("YOU LOOSE");
			//Application.LoadLevel(1);
		}
	}

	
	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
		{
			angle += 360F;
		}
		if (angle > 360F)
		{
			angle -= 360F;
		}
		return Mathf.Clamp (angle, min, max);
	}
	
}
