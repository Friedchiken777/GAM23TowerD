using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour 
{
	public float speed = 2.5f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (Vector3.down * Time.deltaTime * speed);
	}
}
