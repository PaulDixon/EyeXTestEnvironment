using UnityEngine;
using System.Collections;

public class EyeTrackOnly : MonoBehaviour 
{

	//values that will be set in the Inspector
	// public Transform Target;
	public GameObject aim_sprite;
	public float RotationSpeed;
	EyeXGazePoint _gazePoint;
	bool bGamestate = false;

	
	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Use this for initialization
	void Start () 
	{
		aim_sprite = GameObject.Find ("Aim") as GameObject;
		gameState ();
	}

	// Update is called once per frame
	void Update()
	{

			//Debug.DrawRay(aimRay.origin, aimRay.direction);
			
		if (Input.GetKeyDown (KeyCode.W)) {
			if (bGamestate) {
				menuState ();
				
			} else {
				gameState ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			cursorHitTest ();
		}


		if (Input.GetMouseButtonDown (0)) 
		{
			cursorHitTest ();
		}
		
		if (Input.GetMouseButtonDown (1))
		{
			//cursorHitTest();
			cursorHitTest ();
		}
		
		if (Input.GetMouseButtonDown(2))
		{
			cursorHitTest();
		
		}

		Vector2 trackAim = _gazePoint.Screen;
		Ray aimRay = Camera.main.ScreenPointToRay(trackAim);

		RaycastHit hit;
		if (bGamestate && Physics.Raycast (aimRay, out hit, 500.0F)) 
		{
						// targetHitObj.transform.position = hit.point;
						//find the vector pointing from our position to the target
						_direction = (hit.point - transform.position).normalized;
		
						//create the rotation we need to be in to look at the target
						_lookRotation = Quaternion.LookRotation (_direction);
		
						//rotate us over time according to speed until we are in the required rotation
						transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			}
	}

	void cursorHitTest()
	{
		Debug.Log ("cursorHitTest");

		Vector3 gPos = aim_sprite.GetComponent<RectTransform>().position;

		Debug.Log ("aim_sprite.position = "  +gPos);
		//Vector2 convertedGUIPos = GUIUtility.GUIToScreenPoint(gPos);
		//Ray ray = camera.ScreenPointToRay(new Vector3(x, y));
		Ray ray = Camera.main.ScreenPointToRay(gPos);
		
		//Ray ray = Camera.main.ScreenPointToRay(filteredscreenSpace);
		
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 500.0F))
		{
			
			if (hit.collider != null) 
			{
				//GameObject.Find ("Sphere").transform.position = hit.point;

				ballBehaviour b = hit.collider.gameObject.GetComponent<ballBehaviour>();//.hit();
				if (b != null)
				{
					Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
					b.hit();
				}
			}
		}
		
		Debug.DrawRay(ray.origin, ray.direction * 1000, new Color(1f,0.922f,0.016f,1f));
	}

	//30 fps
	public void updateEyeTrack(EyeXGazePoint gazePoint)
	{
		_gazePoint = gazePoint;

		Vector3 gPos = _gazePoint.Screen;
		aim_sprite.GetComponent<RectTransform> ().position = gPos;
	}

	public void gameState()
	{
		bGamestate = true;
		//Screen.showCursor = false;
		Screen.lockCursor = true;

	}
	
	public void menuState()
	{
		bGamestate = false;
		Screen.lockCursor = false;
		Screen.showCursor = true;
		

	}

}
