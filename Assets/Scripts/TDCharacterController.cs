/// <summary>
/// TDCharacterController.cs
/// Written by William George (with lots of internet help...)
/// Basically just a custom first person controller :)
/// </summary>

using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class TDCharacterController : MonoBehaviour {

	CharacterController characterControler;

	public Camera cam;
	public Camera gunCam;

	public GameState gameState;
	
	public float currentCurrency;
	public float currentTowerBases;
	
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
	public GameObject arm, armAttached;
	public GameObject weapon;
	public int ammo, ammoShotType;
	
	public float maxHealth, currentHealth;
	
	public float playerDamage;

	public float enemiesAggroed;
	
	public GameObject spawnPad;
    
    public bool sprinting;
    public float sprintMultiplier;
    float sprintModifyer;
    
    public float deathTimer = 0.0f;
    public AudioManager playerSound;
    public AudioSource b;
    
	public float regenSpeed;
	public float regenOverTime;
	public float regenCooldown;
	public float lastFrameHealth;
	
	public bool playerIsGettingAttacked;
	
	float intensity;

	void Awake()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		gunCam = GameObject.Find("WeaponCamera").GetComponent<Camera>();
		armAttached = transform.FindChild("player_vecrody_asset").FindChild("right_arm").gameObject;
		weapon = cam.transform.FindChild("Weapon").gameObject;
		gun = weapon.transform.FindChild("SpawnBullet").gameObject;
		arm = cam.transform.FindChild("Arm").gameObject;
	}
	void Start ()
	{
		gameState = GameManager.currentState;
		jump = true;
		sprinting = false;
		sprintModifyer = 1;
		currentHealth = maxHealth;
		characterControler = this.GetComponent<CharacterController> ();
		originalRotation = cam.transform.localRotation;
		origionalPlayerRotation = transform.localRotation;
	}
	
	void Update ()
	{
		gameState = GameManager.currentState;
	
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
				moveDirec.x = vertical * walkSpeed * this.transform.forward.x ;
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
			playerSound.playPlayerSounds(b, 6, 0.25f);
			if(jumps < totalJumps)
			{
				moveDirec.y = jumpHeight;
				jump = true;
				jumpGroundClear = 0;
				jumps++;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			sprintModifyer = sprintMultiplier;
			sprinting = true;
			if(GameManager.currentState == GameState.DefensePhase)
			{
				weapon.SetActive (!sprinting);
			}
		}
		
		if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			sprintModifyer = 1;
			sprinting = false;
			if(GameManager.currentState == GameState.DefensePhase)
			{
				weapon.SetActive (!sprinting);
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
			characterControler.Move (moveDirec * Time.deltaTime * sprintModifyer);
		}
		
		if(ammo > 0 && gameState == GameState.DefensePhase)
		{
			if(Input.GetMouseButtonDown(0) && !sprinting)
			{
				ammoShotType = Random.Range(0,bullets.Length);
				GameObject proj = Instantiate(bullets[0], gun.transform.position, gun.transform.localRotation) as GameObject;
				proj.GetComponent<Bullet>().damage = playerDamage;
				//GetComponent<AudioSource>().PlayOneShot(gun.GetComponent<AudioSource>().clip);
				ammo --;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.R)||Input.GetMouseButtonDown(1))
		{
			ammo = 10;
		}
		

        intensity = (maxHealth - currentHealth) / maxHealth;
        cam.gameObject.GetComponent<NoiseAndScratches>().grainIntensityMin = intensity;
        cam.gameObject.GetComponent<NoiseAndScratches>().grainIntensityMin = intensity;
        cam.gameObject.GetComponent<NoiseAndScratches>().scratchIntensityMin = intensity;
        cam.gameObject.GetComponent<NoiseAndScratches>().scratchIntensityMax = intensity;

		if(currentHealth < maxHealth)
		{
			if(playerIsGettingAttacked)
			{
				regenCooldown = 0;
			}
			else
			{
				regenCooldown += Time.deltaTime;
			}
			if(regenSpeed < regenCooldown)
			{
				currentHealth += Time.deltaTime * (currentHealth/regenOverTime);
			}
		}
		
		if(currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		
        if (currentHealth < maxHealth / 4)
        {
            if (!b.isPlaying)
            {
               //playerSound.playGameMusicTracks(b, 2, 0.25f);
            }
        }

		if(currentHealth < 0 || transform.position.y > 30 || transform.position.y < -10)
		{
            deathTimer += Time.deltaTime;
			playerSound.playPlayerSounds(b, 4, 0.25f);
            if (deathTimer >= 5.64f)
            {
                print("YOU DIED");
                Respawn();
				playerSound.playPlayerSounds(b, 7, 0.25f);
                //Application.LoadLevel(1);
                deathTimer = 0.0f;
            }
		}
		
	}

	public void Respawn()
	{
		Vector3 spawnPosition = new Vector3(spawnPad.transform.position.x, spawnPad.transform.position.y + 1, spawnPad.transform.position.z);
		transform.position = spawnPosition;
		transform.rotation = spawnPad.transform.rotation;
		currentHealth = maxHealth;
	}
	
	public void SetArms(bool b)
	{
		arm.SetActive(b);
		armAttached.SetActive(!b);
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
