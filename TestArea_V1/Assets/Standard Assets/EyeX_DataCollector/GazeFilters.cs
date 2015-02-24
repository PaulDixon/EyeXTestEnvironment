using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GazeFilters{

	public GameObject hitObject;
	public List<Vector2> hitArray;
	public Vector2[] hitArray2;
	public int hitArrayMaxSize;
	public int hitIndex;
	public Vector2 lastFilteredPos;
	public Vector2 lastPos;

	// Use this for initialization
	public GazeFilters () {
		hitArrayMaxSize=20;
		hitIndex=0;
		hitArray = new List<Vector2>();
		lastFilteredPos = new Vector2();
		lastPos = new Vector2();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Vector2 snappy(Vector2 newHit)
	{
		//clear array if massive move has beet been made
		if(Vector2.Distance(newHit, lastPos)>20)
		{
			lastPos = newHit;
		}
		return lastPos;
	}
	
	public Vector2 smooth(Vector2 newHit)
	{
		int i 	= 0;
		float x = 0;
		float y	= 0;
		Vector2 retVector;
		
		//always store last hit, may not exist in hit array becouse of filterd out
		lastPos = newHit;
		
		//clear array if massive move has beet been made
		if(Vector2.Distance(newHit, lastFilteredPos)>100 && Vector2.Distance(newHit, lastPos)<30)
		{
			hitArray.Clear();
		}
		//insert new value , remove oldest if reached limit of array.
		if (hitArray.Count < hitArrayMaxSize)
		{
			hitArray.Add(newHit);
		}
		else
		{
			hitArray.RemoveAt(0);
			hitArray.Add(newHit);
		}
		
		//get the average of all hitpoints
		foreach(Vector2 hit in hitArray)
		{
			x += hit.x;
			y += hit.y;
			i++;
		}
		
		retVector.x = x / i;
		retVector.y = y / i;
		
		lastFilteredPos = retVector;
		
		return retVector;
	}

}
