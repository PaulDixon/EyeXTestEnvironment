using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

///tracking data snippet of frame.
/// 

[XmlRoot("Person")]
public class Person
{
	//[XmlAttribute("name")]
	[XmlElement]
	public string name;
	//[XmlAttribute("gender")]
	[XmlElement]
	public string gender;
	//[XmlAttribute("age")]
	[XmlElement]
	public int age;
	[XmlArray("Tests")]
	[XmlArrayItem("Test")]
	public List<SceneTrackData> tests= new List<SceneTrackData>();
}

public class SceneTrackData
{
	//[XmlAttribute("TestName")]
	[XmlElement]
	public string testName = Application.loadedLevelName;
	[XmlArray("AimSequences")]
	[XmlArrayItem("AimSequence")]
	public List<TrackSequence> sequenceData = new List<TrackSequence>();
}

public class TrackSequence
{
	//[XmlAttribute("Id")]
	[XmlElement]
	public int id;
	[XmlArray("DataSets")]
	[XmlArrayItem("DataSet")]
	public  List<TrackDataSet> trackData = new List<TrackDataSet>();
}

public struct TrackDataSet
{
	//[XmlAttribute("timestamp")]
	[XmlElement]
	public float Timestamp;
	//[XmlAttribute("aim")]
	[XmlElement]
	public Vector2 aim;
	//[XmlAttribute("target")]	
	[XmlElement]
	public Vector2 target;
}

//old manager fro tracking data!!!!!!
public class sceneTrackdata 
{

	private Dictionary<string,recordState_a<sceneTrackdata>> states;
	private Dictionary<long,List<trackDataSet>> trackData;
	private long trackDataSequence;
	private TestDataManager manager;

	private StateMachine<sceneTrackdata> state;
	private recordState_a<sceneTrackdata> currentState;
	private recordState_a<sceneTrackdata> offState;
	private recordState_a<sceneTrackdata> onState;

	//singleton
	static private sceneTrackdata _instance;

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
		states = new Dictionary<string,recordState_a<sceneTrackdata>>();

		offState = new offState() as recordState_a<sceneTrackdata>;
		onState = new onState()as recordState_a<sceneTrackdata>;
		currentState = offState;



		states.Add("passiveState", (new offState()) as recordState_a<sceneTrackdata>);
		states.Add("recordState", (new onState())as recordState_a<sceneTrackdata>);
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

	public recordState_a<sceneTrackdata> getOffState()
	{
		return offState;
	}

	public recordState_a<sceneTrackdata> getOnState()
	{
		return onState;
	}

	public void changeState(recordState_a<sceneTrackdata> state)
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

	//singleton
	public static sceneTrackdata instance()
	{
		if(_instance == null)
		{
			_instance= new sceneTrackdata();
		}
		return _instance;
	}

}

