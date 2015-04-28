using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    public float speed = 10.0f;

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
}
