using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrintCurrentCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		string stateInfo = SteeringControls.instance().stateInfo();
		Text outputTo = GetComponent<Text>();

		if (outputTo == null) {
			outputTo = gameObject.AddComponent<Text>();
		}

		outputTo.text = stateInfo;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
