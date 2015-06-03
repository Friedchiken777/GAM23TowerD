using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour 
{
    public float gateHealthMAX;
    public float gateHealthCurrent;
//    public float damageTimer = 0.0f;
//    public bool isDamaged = false;

	// Use this for initialization
	void Start () 
    {
        gateHealthCurrent = gateHealthMAX;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (gateHealthCurrent <= 0)
        {
            Application.LoadLevel("GameOver");
        }
        GUIManager.UpdateGateHealthDisplay(gateHealthCurrent,gateHealthMAX);
    }
}
