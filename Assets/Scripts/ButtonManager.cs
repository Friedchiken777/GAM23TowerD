using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour 
{
    public AudioClip[] audioClip;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	public void ChangeToScene (string changeScene)
	{
		Application.LoadLevel (changeScene);
	}
	public void QuitGame ()
	{
		Application.Quit ();
	}
    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip[clip]);
    }
}
