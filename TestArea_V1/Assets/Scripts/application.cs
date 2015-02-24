using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//application is an "hock" for classes to get updated.

public class application : MonoBehaviour {

	private static bool _isActive = false;  		//singleton

	static private SteeringControls Steering;		//shoose steering behavior
	//static private sceneTrackdata Trackcapture;		//track data to profile
	//static private changeScene SceneChanger;
	static private ProfileManager userProfile;
	static private TestSchema testSchema;
	static private MenuGui menuUI;

	void Awake () 
	{
		if (!_isActive)
		{
			DontDestroyOnLoad (this.gameObject);
			_isActive = true;

			//init
			Steering 		= SteeringControls.instance();
			//Trackcapture 	= sceneTrackdata.instance();
			//SceneChanger 	= GetComponent<changeScene>();
			//unsign monobehaviors for more easy to bind gui events
			userProfile 	= GetComponent<ProfileManager>();
			testSchema 		= GetComponent<TestSchema>();
			menuUI			= GetComponent<MenuGui>();

		} 
		else
		{
			Destroy (this.gameObject);
		}
	}
	void Start()
	{
		menuUI.bindTestschemaToGUI(testSchema);
	}

	public void ExitApplication()
	{
		Application.Quit();
	}
	// Update is called once per frame
	void Update () 
	{
		Steering.Update();
	}
}

