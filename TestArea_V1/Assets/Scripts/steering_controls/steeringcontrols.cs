using UnityEngine;
using System.Collections;

public class SteeringControls
{
	private StateMachine<SteeringControls> ctr_states;
	static private SteeringControls _instance;

	public SteeringControls()
	{
		ctr_states = new StateMachine<SteeringControls>();

		ctr_states.Configure(this, EyeTrackSteering.instance() as state_a<SteeringControls>);
	}

	//change state
	public void setControl(state_a<SteeringControls> newsteerState )
	{
		ctr_states.Configure(this, newsteerState);
	}

	//print short info of steering state
	public string stateInfo()
	{
		return ctr_states.getState().getInfo();
	}

	public void Update() 
	{
		ctr_states.Update();
	}


	//singleton instance
	static public SteeringControls instance()
	{
		if (_instance == null) 
		{
			_instance =  new SteeringControls();
		}
		return _instance;
			
	}
}
