
using System;
using UnityEngine;
using System.Collections;

public class offState :  recordState_a<sceneTrackdata> 
{
	public override  void Enter(sceneTrackdata entity)
	{
		Debug.Log("Starting recording aim data!");
	}
	public override  void Execute(sceneTrackdata entity, EyeXGazePoint trackPoint)
	{
		
		trackDataSet mydata = new trackDataSet ();
		mydata.Timestamp = Time.time;
		mydata.target = new Vector2();
		mydata.aim = new Vector2();
		
		entity.insertData(mydata);
		
	}
	public override  void Exit(sceneTrackdata entity)
	{
		Debug.Log("Stop recording aim data!");
	}
}
