using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class triggerRandomball : MonoBehaviour {
	public GameObject spawnBox;
	public GameObject target_prefab;
	public GameObject wall_Prefab;
	public int minTime = 0;
	public int maxTime = 1;

	public bool b_RemovePrefab = false;
	public int maxBallCount = 10;
	public float radius = 5f;
	public float SpawnAngle_deg = 90;
	public float angleOffset = -180f;
	GameObject currentBall;
	int ballCounter = 0;
	// Use this for initialization



	IEnumerator delayStart() {
		
		yield return new WaitForSeconds(2);
		startSpawner ();
	}

	void Start () {
		spawnWallInArc ();
		// spawnInABox ();
		StartCoroutine ("delayStart");


	}

	void startSpawner()
	{
		ballCounter = 0;
		incrementBall ();
	
	}

	void incrementBall()
	{
		ballCounter++;
		if (ballCounter == maxBallCount) {
						finishScene ();
				} else {
			instatiateBall ();
				}
	}
	private void finishScene()
	{
		GameObject.Find ("Next Test").GetComponent<Button>().interactable = true;
		GameObject.Find ("Next Test").GetComponent<Image>().color = Color.green;
		//GameObject.Find ("Mouse Controller").GetComponent<mouseOnly>().menuState();
		BroadcastMessage ("menuState");
	}

	Vector3 spawnBallinBox()
	{
		float xpos	= Random.Range(spawnBox.collider.bounds.min.x, spawnBox.collider.bounds.max.x);
		float ypos	= Random.Range(spawnBox.collider.bounds.min.y, spawnBox.collider.bounds.max.y);
		//float zpos	= Random.Range(spawnBox.collider.bounds.min.z, spawnBox.collider.bounds.max.z);
		float zpos	= spawnBox.transform.position.z;
		 
		return new Vector3(xpos,ypos,zpos);
	}
	Vector3 spawnBallInArc()
	{
		float rad_angle_x = (Random.Range(0f,SpawnAngle_deg) - (SpawnAngle_deg*0.5f)) * Mathf.Deg2Rad + angleOffset*Mathf.Deg2Rad;
		float rad_angle_y = (Random.Range(0f,20f) + 11f) * Mathf.Deg2Rad;
		float xpos		= Mathf.Cos(rad_angle_x) * radius;
		float ypos		= Mathf.Sin (rad_angle_y) * radius;
		float zpos		= Mathf.Sin (rad_angle_x) * radius;
		
		return new Vector3(xpos, ypos, zpos);
		/*
			pos += spawnBox.transform.position;
			GameObject tempObject = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
			tempObject.transform.parent = spawnBox.transform;
			*/
		
	}
	void spawnWallInArc()
	{
		int i = 0;
		for (i = 0; i < 36; i++) 
		{
				float rad_angle_x = i * 10 * Mathf.Deg2Rad;
				
				float xpos = Mathf.Cos (rad_angle_x) * 1.5f;
				float ypos = 0.5f;
				float zpos = Mathf.Sin (rad_angle_x) * 1.5f;
	
				Vector3 pos =  new Vector3 (xpos, ypos, zpos+2);

				//pos += spawnBox.transform.position;
			GameObject tempObject = Instantiate (wall_Prefab, pos, Quaternion.identity) as GameObject;
				tempObject.transform.parent = spawnBox.transform;

		}
		}
	void instatiateBall()
	{//Vector3 size = spawnBox.collider.bounds;
		//Vector3 SpawnOrigin = spawnBox.transform.position;
		

		// zpos = 0;
		//Vector3 pos = spawnBallinBox(); 
		Vector3 pos = spawnBallInArc(); 
		currentBall = Instantiate(target_prefab, pos, Quaternion.identity) as GameObject;
		currentBall.transform.parent = spawnBox.transform;
		ballBehaviour ballbehaveScript =  currentBall.GetComponent<ballBehaviour>();
		ballbehaveScript.setReciever (this);
		ballbehaveScript.maxTime = Random.Range (minTime,maxTime);
		ballbehaveScript.startTheCount();
	}
	void targetHit( float time)
	{
		removeCurrentBall ();
		incrementBall ();
		;
	}

	void targetFail()
	{
		Debug.Log ("fail(): Message Recieved!!");
		removeCurrentBall ();
		incrementBall ();

	}
	void removeCurrentBall()
	{
		if (b_RemovePrefab)
		{
			if (currentBall != null ) {
				Destroy (currentBall);
			}
		}
	}
	/* -----------------------------------------------*/



	IEnumerator spawnInABox()
	{
		for (int i = 0; i < maxBallCount; i++) {

			currentBall.renderer.enabled = false;
			yield return new WaitForSeconds(i*4);
			instatiateBall();
			currentBall.renderer.enabled = true;
			yield return new WaitForSeconds(i*16);
			currentBall.renderer.enabled = false;
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
