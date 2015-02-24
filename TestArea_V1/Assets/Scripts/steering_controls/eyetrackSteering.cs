using UnityEngine;
using System.Collections;

public class EyeTrackSteering : state_a<SteeringControls>
{
	public static EyeTrackSteering _instance;


	override public void Enter (SteeringControls entity)
	{

	}
	override public void Execute (SteeringControls entity)
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Debug.Log ("fire by eye");
		}
	}
	override public void Exit(SteeringControls entity)
	{

	}
	override public string getInfo()
	{
		return "EyeTrackSteering";
	}

	void Awake()
	{
	}
	static public EyeTrackSteering instance()
	{
		if(_instance == null)
		{
			_instance = new EyeTrackSteering();
		}
		
		return _instance;
	}
}
