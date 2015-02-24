using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
	static private Person _Profile;
	static private bool _profileExist = false;

	static private ProfileManager _instance;

	static private MenuGui menuGUI;
	static private TestSchema testSchema;

	//store to profiles to files
	static private string _filepath 			= Application.dataPath + @"\trackData\tempTestdata_";
	static private string _filepath_ending 		= ".xml";
	static private int _currentFileIndex		= 0;

	public static int age 				= -1;
	public static string name			= "";
	public static string gender			= "female";
	
	void Awake()
	{
		if (_profileExist == false) //singleton initsialization
		{
			_profileExist = true;

			_Profile = new Person();
			DontDestroyOnLoad (this.gameObject);

			menuGUI = GetComponent<MenuGui>();
			testSchema = GetComponent<TestSchema>();
			//tell gui to modify this test scheme
		}
	}
	void Start()
	{
	}
	
	public void CreateProfile()
	{
		//get name input text
		GameObject oName  		= GameObject.Find("name_InputField");
		GameObject oAge  		= GameObject.Find("age_InputField");
		GameObject userPanel 	= GameObject.Find("userPanel");
		GameObject testPanel 	= GameObject.Find("testPanel");

		if (menuGUI == null)
		{
			menuGUI = GetComponent<MenuGui>();
		}

		bool formOK = true;
		if (name == "")
		{
			//set color to red
			menuGUI.markRedUi(oName);
			formOK = false;
		}

		if (age < 0 || age > 100) 
		{
			menuGUI.markRedUi(oAge);
			formOK = false;
		}
		//check if form is valid
		if (formOK != true) 
		{
			menuGUI.markYellowUi(userPanel);
			return;
		} 
		else 
		{
			//userPanel.SetActive(false);
			menuGUI.markGreenUi(userPanel);
			menuGUI.markYellowUi(testPanel);
			oName.GetComponent<InputField>().DeactivateInputField();
		}


		_Profile = new Person ();

		_Profile.name 	= name;
		_Profile.age 	= age;
		_Profile.gender = gender;

		_profileExist = true;

	}
	public bool profileExist()			{return _profileExist;}
	public string getProfileName()		{return _Profile.name;}
	public int getProfileAge()			{return _Profile.age;}
	public string getProfilegender()	{return _Profile.gender;}

	/*
	public void saveProfileData()
	{
		string filepath = Application.dataPath + @"\trackData\testdata.xml";

		//hack to easy convert object to xml with XmlSerializer.
		saveTempProfileData ();
		XmlDocument tempProfileXmlDoc = loadTempProfileData ();

		XmlDocument realProfilexmlDoc = new XmlDocument();

		if (!File.Exists (filepath))
		{
			realProfilexmlDoc.AppendChild(realProfilexmlDoc.CreateElement("Persons"));
			realProfilexmlDoc.Save(filepath); // save file.
		}

		realProfilexmlDoc = new XmlDocument();
		realProfilexmlDoc.Load (filepath);			//load exising root
		
		//get elements from eml
		XmlElement elmRoot = realProfilexmlDoc.DocumentElement;
		XmlElement tempProfile = tempProfileXmlDoc.DocumentElement;

		//insert temp person to real longterm storage.
		//realProfilexmlDoc.AppendChild(tempProfile);
		XmlNode tempPersonNode = realProfilexmlDoc.ImportNode(tempProfile,true);
		realProfilexmlDoc.AppendChild(tempPersonNode);
		realProfilexmlDoc.Save(filepath); // save file.
	}


	private XmlDocument loadTempProfileData()
	{
		string filepath = Application.dataPath + @"\trackData\tempTestdata.xml";

		XmlDocument xmlDoc = new XmlDocument();

		if(File.Exists (filepath))
		{
			xmlDoc.Load(filepath);
		}
		else
		{
			Debug.Log("cant find temp profile xml file!");
		}
		return xmlDoc;
	}
	*/
	//store person to temp xml
	private void saveProfileData()
	{
		//loop until file dosent exist!
		while(true){
			string filepath = _filepath + _currentFileIndex + _filepath_ending;
			if(!File.Exists (filepath))
			{
				var serializer = new XmlSerializer(typeof(Person));
				var stream = new FileStream(filepath, FileMode.Create);
				serializer.Serialize(stream, _Profile);
				stream.Close();
				break;
			}else
			{
				_currentFileIndex++;
			}
		}
	}

	public void resetProfileData()
	{
		_Profile.tests.Clear ();
	}
	public void storeSceneTestDataToProfile(SceneTrackData sceneTrackData)
	{
		_Profile.tests.Add(sceneTrackData);
	}
	
	public ProfileManager instance()
	{
		if (_instance == null) 
		{
			_instance= new ProfileManager();
		}
		return _instance;
	}
}


