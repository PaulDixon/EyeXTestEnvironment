using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuGui : MonoBehaviour 
{
	//singleton
	static private bool isActive= false;
	
	//std setup
	public static TestSchema testSchema;
	
	void Awake()
	{

		if (isActive == false) //singleton initsialization
		{
			//create singleton
			isActive = true;
			DontDestroyOnLoad (this.gameObject);

		}

	}
	
	// Use this for initialization
	void Start () 
	{
		GameObject userPanel = GameObject.Find("userPanel");
		if (userPanel!=null) 
		{
			markYellowUi (userPanel);
		}

	}

	public void bindTestschemaToGUI(TestSchema schema)
	{
		testSchema = schema;
	}


	public void markRedUi(GameObject toGo)
	{
		toGo.GetComponent<Image>().color = new Color(0.7F, 0F, 0F, 1F);
	}
	public void markWhiteUi(GameObject toGo)
	{
		toGo.GetComponent<Image>().color = new Color(0.7F, 0F, 0F, 1F);
	}
	public void markGreenUi(GameObject toGo)
	{
		toGo.GetComponent<Image>().color = new Color(0.0F, 0.7F, 0F, 1F);
	}
	public void markYellowUi(GameObject toGo)
	{
		toGo.GetComponent<Image>().color = new Color(1F, 1F, 0F, 1F);
	}

	public void setupTestDone ()
	{
		GameObject.Find("TestsText").GetComponent<Text>().color = new Color(0.0F, 0F, 0F, 1F);
		GameObject.Find("ControlsText").GetComponent<Text>().color = new Color(0.0F, 0F, 0F, 1F);

		GameObject testPanel = GameObject.Find("testPanel");
		markGreenUi(testPanel);

		GameObject startPanel = GameObject.Find("startPanel");
		markYellowUi (startPanel);
	}

	public void pickFullTest()
	{

		GameObject.Find("ToggleHitTarget90").GetComponent<Toggle>().isOn  = true;
		GameObject.Find("ToggleHitTarget360").GetComponent<Toggle>().isOn = true;
		GameObject.Find("ToggleFollowTarget").GetComponent<Toggle>().isOn = true;
		
		GameObject.Find("ToggleEyetrack").GetComponent<Toggle>().isOn 	= true;
		GameObject.Find("ToggleMouse").GetComponent<Toggle>().isOn 		= true;
		GameObject.Find("ToggleEyeMouse").GetComponent<Toggle>().isOn 	= true;

		setHitTargets90();
		setHitTargets360();
		setFollowTargets();
		setEyeTrack();
		setMouse();
		setEyeMouse();
	}
	
	//setters for test toggles.
	//tests
	
	public void setHitTargets90()
	{
		TestSchema ts = GetComponent<TestSchema> ();
		bool isOn = GameObject.Find ("ToggleHitTarget90").GetComponent<Toggle> ().isOn;
		//testSchema.toggleTestSchema ("90", isOn);
		ts.toggleTestType("hitTargets90", isOn);
	}
	public void setHitTargets360()
	{
		TestSchema ts = GetComponent<TestSchema> ();
		bool isOn = GameObject.Find ("ToggleHitTarget360").GetComponent<Toggle> ().isOn;
		//testSchema.toggleTestSchema ("360", isOn);
		ts.toggleTestType("hitTargets360", isOn);
	}
	public void setFollowTargets()
	{
		TestSchema ts = GetComponent<TestSchema> ();
		bool isOn = GameObject.Find ("ToggleFollowTarget").GetComponent<Toggle> ().isOn;
		//testSchema.toggleTestSchema ("follow", isOn);
		ts.toggleTestType("followTargets", isOn);
	}

	//steerings
	public void setEyeTrack()
	{
		TestSchema ts = GetComponent<TestSchema> ();
		bool isOn = GameObject.Find ("ToggleEyetrack").GetComponent<Toggle> ().isOn;
		//testSchema.toggleTestSchema ("eyeTrack", isOn);
		ts.toggleTestType("eyeTrack", isOn);
	}
	public void setMouse()
	{
		TestSchema ts = GetComponent<TestSchema> ();
		bool isOn = GameObject.Find ("ToggleMouse").GetComponent<Toggle> ().isOn;
		//testSchema.toggleTestSchema ("mouse", isOn);
		ts.toggleTestType("mouse", isOn);
	}
	public void setEyeMouse()
	{
		TestSchema ts = GetComponent<TestSchema> ();
		bool isOn = GameObject.Find ("ToggleEyeMouse").GetComponent<Toggle> ().isOn;
		//testSchema.toggleTestSchema ("eyeMouse", isOn);
		ts.toggleTestType("eyeMouse", isOn);
	}

	public void badTestSelection()
	{
		GameObject.Find("TestsText").GetComponent<Text>().color = new Color(0.7F, 0F, 0F, 1F);
		GameObject.Find("TestsText").GetComponent<Text>().text = "CHOOSE YOUR TEST ENVIRONMENT(s)";
		markYellowUi(GameObject.Find("testPanel"));
	}
	
	public void badControlsSelection()
	{		
		GameObject.Find ("ControlsText").GetComponent<Text> ().color = new Color (0.7F, 0F, 0F, 1F);
		GameObject.Find ("ControlsText").GetComponent<Text> ().text = "CHOOSE CONTROL SYSTEM ( at least one)";
		markYellowUi(GameObject.Find("testPanel"));
	}
	public void setFormName()
	{
		InputField oName  = GameObject.Find("name_InputField").GetComponent<InputField>();
		ProfileManager.name = oName.text;
		markGreenUi(GameObject.Find("name_InputField"));
	}
	public void setFormAge()
	{	
		InputField oAge  = GameObject.Find("age_InputField").GetComponent<InputField>();
		//check if number is valid
		int insertedAge;
		if (int.TryParse(oAge.text, out insertedAge)) 
		{
			ProfileManager.age = insertedAge; 
			markGreenUi(GameObject.Find("age_InputField"));
		} 
		else 
		{
			markRedUi(GameObject.Find("age_InputField"));
		}
	}
	public void setFormGender(string gend) 
	{		
		ProfileManager.gender = gend;
	}


}