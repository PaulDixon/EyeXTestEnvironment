using UnityEngine;
using System.Collections;

public class EyeMouse : MonoBehaviour 
{

	//values that will be set in the Inspector
	// public Transform Target;
	public GameObject aim_sprite;
	public float RotationSpeed;
	EyeXGazePoint _gazePoint;
	bool bGamestate = false;
	MouseLook mouseLook_X_script;
	MouseLook mouseLook_Y_script;

	GameObject parentTransformX;

	
	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Use this for initialization
	void Start () 
	{
		// transform.GetChild(1).GetComponent<MouseLook>()
		mouseLook_X_script = transform.parent.GetComponent<MouseLook>();
		
		mouseLook_Y_script = GetComponent<MouseLook>();
		aim_sprite = GameObject.Find ("Aim") as GameObject;
		gameState ();
		parentTransformX = transform.parent.gameObject;
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
			snapToTarget();
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
		//snapToTarget ();
		//cursorHitTest ();
	}
	private void snapToTarget()
	{
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
			Vector3 rot = Quaternion.Slerp (transform.rotation, _lookRotation, 180f).eulerAngles;
			//set transformation on diffrent gamebjects.
			 
			MouseLook rotateY = GetComponent<MouseLook>();
			MouseLook rotateX = parentTransformX.GetComponent<MouseLook>();
			//rotateY.forceRotation(rot);
			//rotateX.forceRotation(rot);

			//transform.Rotate(new Vector3 (rot[0],rot[1],0));
			//transform.eulerAngles =new Vector3 (rot[0],rot[1],0);

			parentTransformX.transform.eulerAngles = new Vector3 (0,rot[1],0);
			//transform.eulerAngles = new Vector3(rot[0],rot[1],0);
			transform.localEulerAngles = (new Vector3 (rot[0],0,0));


			//transform.Rotate(new Vector3(rot[1],0,0));
			//parentTransformX.transform.Rotate(new Vector3(0,rot[1],0));
			// Debug.Log(rot[0]);
		} 
	}
	/*
	private Vector3 parentchildRotationVector()
	{
		return new Vector3 (parentTransformX.transform.rotation.eulerAngles[0], transform [1], 0);
	}
	*/

	void cursorHitTest()
	{
	//	Debug.Log ("cursorHitTest");
		//int x = Screen.width / 2;
		//int y = Screen.height / 2;
		Vector3 gPos = aim_sprite.GetComponent<RectTransform>().position;
	//	Debug.Log ("aim_sprite.position = "  +gPos);
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
					// Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
					b.hit();
				}
			}
		}
		
	//	Debug.DrawRay(ray.origin, ray.direction * 1000, new Color(1f,0.922f,0.016f,1f));
	}

	//30 fps
	public void updateEyeTrack(EyeXGazePoint gazePoint)
	{
		_gazePoint = gazePoint;
	}

	public void gameState()
	{
		bGamestate = true;
		//Screen.showCursor = false;
		Screen.lockCursor = true;
		if (mouseLook_X_script != null && mouseLook_Y_script != null) {
			mouseLook_X_script.enabled = true;
			mouseLook_Y_script.enabled = true;
		}

	}
	
	public void menuState()
	{
		bGamestate = false;
		Screen.lockCursor = false;
		Screen.showCursor = true;
		if (mouseLook_X_script != null && mouseLook_Y_script != null) {
			mouseLook_X_script.enabled = false;
			mouseLook_Y_script.enabled = false;
		}

	}

}
