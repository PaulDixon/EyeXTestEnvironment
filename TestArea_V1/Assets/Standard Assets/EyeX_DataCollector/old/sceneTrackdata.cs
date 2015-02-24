using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///tracking data snippet of frame.
public struct trackDataSet
{
	public float Timestamp;
	public Vector2 aim;
	public Vector2 target;
}

public class sceneTrackdata 
{

	private Dictionary<string,recordState<sceneTrackdata>> states;
	private Dictionary<long,List<trackDataSet>> trackData;
	private long trackDataSequence;
	private TestDataManager manager;
	private StateMachine<sceneTrackdata> state;
	private recordState<sceneTrackdata> currentState;
	private recordState<sceneTrackdata> offState;
	private recordState<sceneTrackdata> onState;

	/// Use this for initialization
	public sceneTrackdata() 
	{ 
		///get manager for long term data storage
		//TestDataManager manager = TestDataManager.instance;

		///init data for this scene
		trackData = new Dictionary<long,List<trackDataSet>>();
		trackDataSequence =0;
		newdataSequence();

		///create states
		states = new Dictionary<string,recordState<sceneTrackdata>>();

		offState = new offState() as recordState<sceneTrackdata>;
		onState = new onState()as recordState<sceneTrackdata>;
		currentState = offState;



		states.Add("passiveState", (new offState()) as recordState<sceneTrackdata>);
		states.Add("recordState", (new onState())as recordState<sceneTrackdata>);
		/*
		///create stateMachine
		state = new StateMachine<sceneTrackdata>();
		state.Configure(this, states["passiveState"]);
		*/

	}



	public void storeTrackPoint(EyeXGazePoint trackPoint)
	{
		if (currentState != null) 
		{
			currentState.Execute(this,trackPoint);
		}

	}

	public recordState<sceneTrackdata> getOffState()
	{
		return offState;
	}

	public recordState<sceneTrackdata> getOnState()
	{
		return onState;
	}

	public void changeState(recordState<sceneTrackdata> state)
	{
		currentState.Exit(this);
		currentState = state;
		state.Enter (this);
	}

		/// Update is called once per frame


	public void newdataSequence()
	{
		trackDataSequence++;
		trackData.Add(trackDataSequence, new List<trackDataSet>());
	}

	public void insertData(trackDataSet data)
	{
		//insert data to current sequence
		trackData[trackDataSequence].Add(data);
	}

	void OnDestroy()
	{
		//store the data to long term storage manager
		//manager.storeScenedata ("scene1", this);
	}
}

