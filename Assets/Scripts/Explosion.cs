using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
	public GameObject explosion1;
	public GameObject explosion2;
	public GameObject explosion3;
	public GameObject explosion4;
	public GameObject explosion5;
	public GameObject explosion6;
	public GameObject explosion7;
	public GameObject explosion8;
	public GameObject explosion9;
	public GameObject explosion10;
	public GameObject explosion11;
	public GameObject explosion12;
	public GameObject explosion13;
	public float timer = 0.0f;

	// Use this for initialization
	void Start () 
	{
		explosion1.SetActive(false);
		explosion2.SetActive(false);
		explosion3.SetActive(false);
		explosion4.SetActive(false);
		explosion5.SetActive(false);
		explosion6.SetActive(false);
		explosion7.SetActive(false);
		explosion8.SetActive(false);
		explosion9.SetActive(false);
		explosion10.SetActive(false);
		explosion11.SetActive(false);
		explosion12.SetActive(false);
		explosion13.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer >= 23.0f)
		{
			explosion1.SetActive(true);
			explosion13.SetActive(true);
		}
		if (timer >= 25.5f)
		{
			explosion2.SetActive(true);
			explosion12.SetActive(true);
			explosion1.SetActive(false);
			explosion13.SetActive(false);
		}
		if (timer >= 28.0f)
		{
			explosion3.SetActive(true);
			explosion11.SetActive(true);
			explosion2.SetActive(false);
			explosion12.SetActive(false);
		}
		if (timer >= 30.5f)
		{
			explosion4.SetActive(true);
			explosion10.SetActive(true);
			explosion3.SetActive(false);
			explosion11.SetActive(false);
		}
		if (timer >= 33.0f)
		{
			explosion5.SetActive(true);
			explosion9.SetActive(true);
			explosion4.SetActive(false);
			explosion10.SetActive(false);
		}
		if (timer >= 35.5f)
		{
			explosion6.SetActive(true);
			explosion8.SetActive(true);
			explosion5.SetActive(false);
			explosion9.SetActive(false);
		}
		else if (timer >= 38.0f)
		{
			explosion7.SetActive(true);
			explosion10.SetActive(true);
			explosion6.SetActive(false);
			explosion8.SetActive(false);
		}
		if (timer >= 40.5f)
		{
			explosion8.SetActive(true);
			explosion4.SetActive(true);
			explosion7.SetActive(false);
			explosion10.SetActive(false);
		}
		if (timer >= 43.0f)
		{
			explosion9.SetActive(true);
			explosion3.SetActive(true);
			explosion8.SetActive(false);
			explosion4.SetActive(false);
		}
		if (timer >= 45.5f)
		{
			explosion10.SetActive(true);
			explosion4.SetActive(true);
			explosion9.SetActive(false);
			explosion3.SetActive(false);
		}
		if (timer >= 48.0f)
		{
			explosion11.SetActive(true);
			explosion2.SetActive(true);
			explosion10.SetActive(false);
			explosion4.SetActive(false);
		}
		if (timer >= 50.5f)
		{
			explosion12.SetActive(true);
			explosion7.SetActive(true);
			explosion11.SetActive(false);
			explosion2.SetActive(false);
		}
		if (timer >= 53.0f)
		{
			explosion13.SetActive(true);
			explosion5.SetActive(true);
			explosion12.SetActive(false);
			explosion7.SetActive(false);
			timer = 20.5f;
		}
		if (timer >= 20.5f)
		{
			explosion13.SetActive(false);
			explosion5.SetActive(false);
		}
	}
}
