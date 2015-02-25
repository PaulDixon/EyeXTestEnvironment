using UnityEngine;
using System.Collections;

public class AbortTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void abortTest()
	{
		ProfileManager pm = GameObject.Find ("Scriptbinder").GetComponent<ProfileManager> ();
		pm.resetProfileData ();
		Application.LoadLevel("mainmenu");
	}
}
