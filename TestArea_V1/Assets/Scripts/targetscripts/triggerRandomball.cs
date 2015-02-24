using UnityEngine;
using System.Collections;

public class triggerRandomball : MonoBehaviour {
	public GameObject spawnBox;
	public GameObject prefab;
	public int numberOfObjects = 20;
	public float radius = 5f;
	GameObject currentBall;
	int ballCounter = 0;
	// Use this for initialization

	void OnTriggerEnter(Collider other) 
	{
		//StartCoroutine ("spawnInABox");
		//Debug.Log("FUCKING WORK TWAT BAG!!");
		//StartCoroutine ("Example");
		startSpawner ();
	}



	void Start () {
		// spawnInABox ();
		startSpawner ();
	}

	void startSpawner()
	{
		ballCounter = 0;
		increment ();
		instatiateBall ();
	}

	void increment()
	{
		ballCounter++;
	}

	void instatiateBall()
	{//Vector3 size = spawnBox.collider.bounds;
		//Vector3 SpawnOrigin = spawnBox.transform.position;
		float xpos	= Random.Range(spawnBox.collider.bounds.min.x, spawnBox.collider.bounds.max.x);
		float ypos	= Random.Range(spawnBox.collider.bounds.min.y, spawnBox.collider.bounds.max.y);
		//float zpos	= Random.Range(spawnBox.collider.bounds.min.z, spawnBox.collider.bounds.max.z);
		float zpos	= spawnBox.transform.position.z;
		
		// zpos = 0;
		Vector3 pos = new Vector3(xpos,ypos,zpos); 
		currentBall = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
		currentBall.transform.parent = spawnBox.transform;
		ballBehaviour ballbehaveScript =  currentBall.GetComponent<ballBehaviour>();
		ballbehaveScript.setReciever (this);
		ballbehaveScript.maxTime = Random.Range (4,10);
		ballbehaveScript.startTheCount();
	}
	void targetHit( float time)
	{
		removeCurrentBall ();
		increment ();
		instatiateBall ();
	}

	void targetFail()
	{
		Debug.Log ("fail(): Message Recieved!!");
		removeCurrentBall ();
		increment ();
		instatiateBall ();
	}
	void removeCurrentBall()
	{
		if (currentBall != null) {
			Destroy (currentBall);
		}
	}

	IEnumerator Example() {
		Debug.Log ("Firing up example2");
		Debug.Log("Firing up example2" + Time.time);
		yield return new WaitForSeconds(5);
		Debug.Log("Firing up example2" + Time.time);
	}

	IEnumerator spawnInABox()
	{
		for (int i = 0; i < numberOfObjects; i++) {

			currentBall.renderer.enabled = false;
			yield return new WaitForSeconds(i*4);
			instatiateBall();
			currentBall.renderer.enabled = true;
			yield return new WaitForSeconds(i*16);
			currentBall.renderer.enabled = false;
		}
	}

	void circleSpawner()
	{
		for (int i = 0; i < numberOfObjects; i++) {
			float angle		= i * Mathf.PI * 2 / numberOfObjects;
			float xpos		= Mathf.Cos(angle) * radius;
			float ypos		= Mathf.Cos (angle) * radius;
			float zpos		= Mathf.Sin (angle) * radius;
			
			Vector3 pos = new Vector3(xpos, ypos, zpos);
			pos += spawnBox.transform.position;
			GameObject tempObject = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
			tempObject.transform.parent = spawnBox.transform;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
