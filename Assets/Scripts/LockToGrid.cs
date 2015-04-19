using UnityEngine;
using System.Collections;

public class LockToGrid : MonoBehaviour 
{
	Vector3 tempPos;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		tempPos = transform.position;
		if(tempPos.x % 1 != 0 || tempPos.y % 1 != 0 || tempPos.z % 1 != 0)
		{
			Vector3 newPos = new Vector3(Mathf.Floor(tempPos.x),Mathf.Floor(tempPos.y),Mathf.Floor(tempPos.z));
			transform.position = newPos;
		}
	}
}
