using UnityEngine;
using System.Collections;

public class mouse90 : MonoBehaviour {

	private TrackDataManager sceneTrackData;
	// Use this for initialization
	void Awake()
	{
		//GameObject.Find("ScriptBinder").GetComponent<>;
		//sceneTrackData = new TrackDataManager ();

	}
	void Start () {
	 	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDestroy() 
	{	
		ProfileManager  profileManager = GameObject.Find("Scriptbinder").GetComponent<ProfileManager>();

		profileManager.storeSceneTestDataToProfile(sceneTrackData.getSceneTrackData());

	}
}
