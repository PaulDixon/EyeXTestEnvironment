using UnityEngine;
using System.Collections;

public class ballTouch : MonoBehaviour {
	int minWait = 5; 
	int maxWait = 20;
	//string name = "";
	bool hittable = false;
	bool loop = false;

	float timer;
	float WaitTime  = 0.5f;
	float ResetPoint;

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




	

	
	void Start () {
		
		ResetPoint = 1 + (Random.Range(minWait, maxWait));
		
	}
	
	
	void Update () {
		
		timer += Time.deltaTime;
		
		if(timer < WaitTime)
		{
			renderer.material.color = Color.white;
		}
		
		if(timer > WaitTime)
		{
			renderer.material.color = Color.red;
		}
		
		if(timer > ResetPoint)
		{
			timer = 0;
		}
		
	}
}
