using UnityEngine;
using System.Collections;

public class ballTouch : MonoBehaviour {
	int minWait = 5; 
	int maxWait = 20;
	//string name = "";
	bool hittable = false;
	void OnMouseDown () 
	{
		// Debug.Log("Mouse Down");
		if (Input.GetMouseButtonDown(0)) {
			// Do right-click stuff here
			//Debug.Log("left - click");
			if (hittable)
			{

				Start();

			}
			else
			{
				Debug.Log("left - click");

			}
		}
		/*
		if (Input.GetMouseButtonDown(1)) {
			// Do right-click stuff here
			Debug.Log("right - click");
		}
		*/
	}

	WaitForSeconds doTheWait ()
	{
		float time = Random.Range (minWait, maxWait);
		//yield WaitForSeconds(time);
		yield WaitForSeconds(time);
		while (true) {
			//transform.Translate(-Vector3.up * objectSpeed * Time.deltaTime);
			//yield;
			hittable = true;
		}
	}

	// Use this for initialization
	void Start () {
		hittable = false;
		doTheWait ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
