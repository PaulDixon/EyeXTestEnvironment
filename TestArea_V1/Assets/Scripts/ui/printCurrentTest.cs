using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class printCurrentTest : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GameObject application = GameObject.Find("Scriptbinder");
		if(application!= null)
		{
			//load current test name
			TestSchema appSchema = application.GetComponent<TestSchema>();
			string currentTest = appSchema.getCurrentTestName();
			//set name to text ui lable.
			Text outputTo = GetComponent<Text>();
			if (outputTo == null) {
				outputTo.text = currentTest;
			}
			outputTo.text = currentTest;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
