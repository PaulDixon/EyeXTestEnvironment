using UnityEngine;
using System.Collections;

public class ballBehaviour : MonoBehaviour {

	public float maxTime = 10f;
	public float startTime;
	public double endTime;
	public double timeTaken;
	public triggerRandomball motherScript;


	void Awake() {
		//renderer.enabled = false;
		}
	// Use this for initialization
	void Start () {



	}

	public void startTheCount()
	{
		StartCoroutine ("ActivateAndCountDown");
	}

	void OnMouseDown()
	{
		Debug.Log ("Object Mouse Click");
		hit ();
	}

	IEnumerator ActivateAndCountDown()
	{
		startTime = Time.fixedTime;
		activate ();
		yield return new WaitForSeconds(maxTime);
		fail();
	}
	public void setReciever(triggerRandomball givenScript)
	{
		motherScript = givenScript;
	}
	void activate()
	{
		//Debug.Log ("Object Active");
		renderer.enabled = true;
	}
	void hit()
	{
		//Debug.Log ("Object deactivate");
		deactivate ();
		timeTaken = Time.fixedTime - startTime;
		//Debug.Log ("SendMessageUpwards ('hit', " + timeTaken + ")");
		motherScript.SendMessage("targetHit", timeTaken);
	}
	void fail()
	{
		deactivate ();
		//Debug.Log ("SendMessageUpwards ('fail')");
		motherScript.SendMessage("targetFail");
	}

	void deactivate()
	{
		renderer.enabled = false;
	}

	// 
	// Update is called once per frame
	void Update () {
	
	}


}
