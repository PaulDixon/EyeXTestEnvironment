using UnityEngine;
using System.Collections;

public class ballBehaviour : MonoBehaviour {

	public float maxTime;
	public float startTime;
	public double endTime;
	public double timeTaken;
	public triggerRandomball motherScript;

	private TrackDataManager trackMan;
	private Camera camera;
	private GameObject Aim;

	public Vector3 aimScreenCord;
	public Vector3 targetScreenCord;
	public bool _isdead;

	void Awake() 
	{
		//renderer.enabled = false;

	}

	// Use this for initialization

	void Start () 
	{

		Aim = GameObject.Find ("Aim") as GameObject;
		camera = GameObject.Find ("Main Camera").camera;

		GameObject trackerManObj = GameObject.Find ("SceneScriptBinder"); 
		trackMan = trackerManObj.GetComponent<TrackDataManager> ();

		trackMan.startNewSequence ();
		_isdead = false;
	}

	void Update()
	{
		if (!_isdead) 
		{
				Vector3 targetScreen = camera.WorldToScreenPoint (transform.position);
				Vector3 aimScreen = Aim.transform.position;

				if (!transform.renderer.isVisible) {
						//			Debug.LogWarning("targetScreen =" + targetScreen);
						targetScreen.x = 9999;
						targetScreen.y = 9999;

				}



				trackMan.insertDataSet (aimScreen, targetScreen);

				//		Debug.Log (aimScreen);
				//		Debug.Log (targetScreen);
				aimScreenCord = aimScreen;
				targetScreen = targetScreen;
		}



	}

	/*
	void OnGUI()
	{
		Vector3 targetScreen = camera.WorldToScreenPoint(transform.position);
		Vector3 aimScreen = Aim.transform.position;

		GUI.Label(new Rect(0, 0, 100, 100), (int)(1.0f / Time.smoothDeltaTime));   
		GUI.Label(new Rect(0, 100, 100, 100), aimScreen);  
		GUI.Label(new Rect(0, 200, 100, 100), targetScreen);  
	}
*/
	
	void OnDestroy()
	{
		//trackMan.storeSequence ();
	}
	public void startTheCount()
	{
		StartCoroutine ("ActivateAndCountDown");
	}

	/*
	void OnMouseDown()
	{
		Debug.Log ("Object Mouse Click");
		hit ();
	}
*/
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
	public void hit()
	{
		_isdead = true;
		deactivate ();
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.green;
		rend.material.shader = Shader.Find("Color");
		rend.material.SetColor ("_Color", Color.green);

		timeTaken = Time.fixedTime - startTime;
		//Debug.Log ("SendMessageUpwards ('hit', " + timeTaken + ")");
		motherScript.SendMessage("targetHit", timeTaken);


	}
	void fail()
	{
		if (!_isdead) 
		{
			deactivate ();
		}
		//Debug.Log ("SendMessageUpwards ('fail')");
		motherScript.SendMessage("targetFail");	//target timeout really!

	}

	void deactivate()
	{
		trackMan.storeSequence ();
		collider.enabled = false; 

	}







}
