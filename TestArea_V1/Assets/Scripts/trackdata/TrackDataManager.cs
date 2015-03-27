using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrackDataManager : MonoBehaviour 
{

	private SceneTrackData _sceneTrackData;
	private TrackSequence _trackSequence;

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
	}

	void Awake()
	{
		_sceneTrackData = new SceneTrackData ();
	}
	
	void OnDestroy() 
	{	
		if (_sceneTrackData.sequenceData.Count > 0) 
		{
			ProfileManager  profileManager = GameObject.Find("Scriptbinder").GetComponent<ProfileManager>();
			
			profileManager.storeSceneTestDataToProfile(_sceneTrackData);
		}
	}
	
	public void insertDataSet(Vector3 aim, Vector3 target)
	{
		TrackDataSet dataSet = new TrackDataSet ();
		
		dataSet.aim = aim;
		dataSet.target = target;
		dataSet.Timestamp = Time.time;
		
		_trackSequence.trackData.Add(dataSet);
	}

	public void storeSequence()
	{
		_sceneTrackData.sequenceData.Add (_trackSequence);
	}

	public void startNewSequence()
	{
		//store sequence data
		//storeSequence ();

		//start new sequence
		_trackSequence = new TrackSequence ();
	}
	public SceneTrackData getSceneTrackData()
	{
		return _sceneTrackData;
	}
}
