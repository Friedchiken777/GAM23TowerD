using UnityEngine;
using System.Collections;

public class EnemyShip_MainMenu : MonoBehaviour 
{
	public float speed;
	public float destroy;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.left * Time.deltaTime * speed);
		Destroy (gameObject, destroy);
	}
}
