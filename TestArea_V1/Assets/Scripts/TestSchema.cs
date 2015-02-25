using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestSchema : MonoBehaviour {

	private static bool _isActive= false; //singleton

	//private Dictionary<string, bool> tests 		= new Dictionary<string, bool>();
	//private Dictionary<string, bool> controls   = new Dictionary<string, bool>();

	private static Dictionary<string, bool> _tests   	= new Dictionary<string, bool>();
	private static Dictionary<string, bool> _testsTypes = new Dictionary<string, bool>();
	private static bool testValidation = false;

	//serialized test order (random?)
	private static List<string> testRunOrder 	= new List<string>();

	private static int currentTestsIndex 		= 0;
	//private int currentCtrlIndex 				= 0;
	// int maxTestsIndex 						= 3;
	//private int maxCtrlIndex 					= 3;
	
	private bool randomOrder = false;

	void Awake()
	{

		if (!_isActive)
		{
			DontDestroyOnLoad (this.gameObject);
			_isActive = true;

			//toggles for test
			_testsTypes["hitTargets90"] 		= false;
			_testsTypes["hitTargets360"]  		= false;
			_testsTypes["followTargets"] 		= false;
			_testsTypes["mouse"] 				= false;
			_testsTypes["eyeTrack"]  			= false;
			_testsTypes["eyeMouse"] 			= false;

			_tests["mouse90"]			= false;
			_tests["mouse360"]			= false;
			_tests["mouseFollow"]		= false;
		
			_tests["eyeTrack360"]		= false;
			_tests["eyeTrack90"]		= false;
			_tests["eyeTrackFollow"]	= false;

			_tests["eyeMouse90"]		= false;
			_tests["eyeMouse360"]		= false;
			_tests["eyeMouseFollow"]	= false;


		} 
		else 
		{
			Destroy (this.gameObject);
		}

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	public void startNewTest()
	{
		ProfileManager profileCheck = GetComponent<ProfileManager> ();

		if (profileCheck.profileExist()) 
		{
			//string nextScene = getNextTest();
			currentTestsIndex()

			if(nextScene != null) 
			{
				changeScene(nextScene);
			}
			else
			{
				changeScene("finishedScene");
			}
		}
		else
		{
			Debug.Log("Profile does not exist!");
		}
	}
	*/
	
	public void abortTest()
	{
		Debug.Log("Abort Test!");
		changeScene("mainmenu");
	}

	//testscheme is allready done(bindTestschemaToGUI), only gui remains to be greenlit!
	public void testValidCheck()
	{
		bool doReturn = false;

		//reload selections, in case missed selections(bad programflow, where toggle selection is not updated).

		MenuGui gui = GetComponent<MenuGui>();
		gui.setHitTargets90();
		gui.setHitTargets360();
		gui.setFollowTargets();
		gui.setEyeTrack();
		gui.setMouse();
		gui.setEyeMouse();
		
		if (_testsTypes["hitTargets90"]==false  && _testsTypes["hitTargets360"]==false  && _testsTypes["followTargets"]==false ) 
		{
			if(gui != null)
			{
				gui.badTestSelection();
			}
			testValidation = false;
			doReturn =true;
		}
		
		//check if all controls are missing(should not)
		if (_testsTypes["eyeMouse"] ==false && _testsTypes["mouse"] ==false && _testsTypes["eyeTrack"]==false ) 
		{
			if(gui != null)
			{
				gui.badControlsSelection();
			}
			testValidation = false;
			doReturn =true;
		}

		if (doReturn) {return;}

		testValidation = true;

		//restore colors
		MenuGui guiHandler = GetComponent<MenuGui>();
		guiHandler.setupTestDone ();
		
	}

	public void startNextTest()
	{
		ProfileManager profileCheck = GetComponent<ProfileManager>();

		//check profile
		if (!profileCheck.profileExist())
		{				
			Debug.Log("No Profile Exists!, transfer back to mainmenu");
			return;
		}
		//check teststart
		if (!testValidation) 
		{
			Debug.Log("No test Exists!, transfer back to mainmenu");
			return;
		}


		if (testRunOrder.Count == 0) 
		{
			buildTestSchema();
			buildTestOrder();
			//check build success!
			if(testRunOrder.Count == 0)
			{
				Debug.Log("Testorder list is empty!");
			}
			else
			{

				changeScene(testRunOrder[currentTestsIndex]);
				return;
			}
		}
	
		//get next test index
		currentTestsIndex++;

		//check if more tests or finished.
		if (currentTestsIndex < testRunOrder.Count) 
		{
			changeScene(testRunOrder[currentTestsIndex-1]);
			return;
		} 
		else 
		{
			changeScene("finished");
		}

		/*
		//change controls
		if (currentCtrlIndex < maxCtrlIndex) 
		{
			currentTestsIndex++;
		} 
		else 
		{
			//reset steering
			currentTestsIndex=0;
			if(currentTestsIndex< maxTestsIndex)
			{
				currentTestsIndex++

			}
			else
			{
				changeScene("finishedScene");
				//return "finishScene";
			}
		}



		if (currentTestsIndex < testRunOrder.Count) 
		{
			currentTestsIndex++;
			//start new scene
			return testRunOrder[currentTestsIndex-1];
		}
		else
		{
			return null;
		}
		*/
	}

	private void buildTestOrder()
	{
		//reset list
		testRunOrder.Clear();

		var keys = new List<string>(_tests.Keys);
		foreach (string key in keys)
		{
			if (_tests[key] == true)
			{
				testRunOrder.Add(key);
			}
		}
	}
	


	public void changeScene(string toScene)
	{
		//load base content
		//Application.LoadLevelAdditive("map_base");
		
		//load specific scene
		Application.LoadLevel(toScene);
		
		//apply gui
		if (toScene != "mainmenu") //cant have 2 gui systems
		{
			//Application.LoadLevelAdditive("gui");
		}
	}

	public string getCurrentTestName()
	{
		try
		{
			return testRunOrder[currentTestsIndex];
		}
		catch(System.Exception e)
		{
			return "none";
		}
	}

	public List<string> getTestRunOrder()
	{
		if (testRunOrder.Count <= 0) 
		{
			buildTestOrder();
		}
		return testRunOrder;
	}

	public Dictionary<string, bool> getTests()
	{
		return _tests;
	}
	/*
	public Dictionary<string, bool> getControls()
	{
		return controls;
	}

	*/
	public void toggleTestType(string name, bool active)
	{
		_testsTypes[name] = active;
	}
	public void buildTestSchema()
	{
		
		if (_testsTypes ["hitTargets90"]) 
		{			
			if(_testsTypes ["mouse"])	{_tests["mouse90"] 	  = true;}
			if(_testsTypes ["eyeTrack"]){_tests["eyeTrack90"] = true;}
			if(_testsTypes ["eyeMouse"]){_tests["eyeMouse90"] = true;}
		}
		if (_testsTypes ["hitTargets360"]) 
		{
			if(_testsTypes ["mouse"])	{_tests["mouse360"] 	= true;}
			if(_testsTypes ["eyeTrack"]){_tests["eyeTrack360"] 	= true;}
			if(_testsTypes ["eyeMouse"]){_tests["eyeMouse360"] 	= true;}
		}
		if (_testsTypes ["followTargets"]) 
		{
			if(_testsTypes ["mouse"])	
			{
				_tests["mouseFollow"] 		= true;
			}
			if(_testsTypes ["eyeTrack"])
			{
				_tests["eyeTrackFollow"] 	= true;
			}
			if(_testsTypes ["eyeMouse"])
			{
				_tests["eyeMouseFollow"] 	= true;
			}
		}
	}
	/*
	public void toggleTestSchema(string type,bool active)
	{
		
		if (type == "90") 
		{
			_tests["mouse90"] 	 = active;
			_tests["eyeTrack90"] = active;
			_tests["eyeMouse90"] = active;
		}
		else if(type == "360")
		{
			_tests["mouse360"] 		= active;
			_tests["eyeTrack360"] 	= active;
			_tests["eyeMouse360"] 	= active;
		}
		else if(type == "follow")
		{
			_tests["mouseFollow"] 		= active;
			_tests["eyeTrackFollow"] 	= active;
			_tests["eyeMouseFollow"] 	= active;
		}
		//tests
		else if (type == "mouse") 
		{
			_tests["mouse90"] 	  	= active;
			_tests["mouse360"]   	= active;
			_tests["mouseFollow"] 	= active;
		}
		else if (type == "eyeMouse") 
		{
			_tests["eyeMouse90"] 	 = active;
			_tests["eyeMouse360"]    = active;
			_tests["eyeMouseFollow"] = active;
		}
		else if (type == "eyeTrack") 
		{
			_tests["eyeTrack90"] 	 = active;
			_tests["eyeTrack360"]   = active;
			_tests["eyeTrackFollow"] = active;
		}


	}
			*/
	
}
