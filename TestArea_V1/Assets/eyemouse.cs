using UnityEngine;
using System.Collections;
using Tobii.EyeX.Framework;


public class eyemouse : MonoBehaviour {
	public GameObject targetPointer;
	MouseLook mouseLook_X_script;
	MouseLook mouseLook_Y_script;
	CharacterMotor charMotor;
	GameObject aim_sprite;
	bool bGamestate = false;
	EyeXGazePoint _gazePoint;
	Ray aimRay;
	public GameObject targetHitObj;
	public float RotationSpeed;
	
	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Use this for initialization
	void Start () {
		mouseLook_X_script = GetComponent<MouseLook>();
		
		mouseLook_Y_script = transform.GetChild(1).GetComponent<MouseLook>();
		charMotor = GetComponent<CharacterMotor>();
		aim_sprite = GameObject.Find ("Aim") as GameObject;
		gameState ();
		bGamestate = true;
		//menuState ();
	}
	
	
	// Update is called once per frame
	void Update () {
		//Debug.DrawRay(aimRay.origin, aimRay.direction);
				snapToAim ();
				if (Input.GetKeyDown (KeyCode.W)) {
						if (bGamestate) {
								menuState ();
				
						} else {
								gameState ();
						}
				}
		
				if (Input.GetMouseButtonDown (0)) {
			snapToAim ();
				}
		
				if (Input.GetMouseButtonUp (0))
				{
					//cursorHitTest();
			cursorHitTest ();
				}
		
		if (Input.GetMouseButtonDown(2))
			cursorHitTest();
		
	}

	//30 fps
	public void updateEyeTrack(EyeXGazePoint gazePoint)
	{
		_gazePoint = gazePoint;
	}

	public void snapToAim()
	{
		Vector2 trackAim = _gazePoint.Screen;
		aimRay = Camera.main.ScreenPointToRay(trackAim);

		//float aimAngle = Vector3.Angle (ray.direction,transform.up);

		//Debug.Log ("aim angle" + aimAngle);
		RaycastHit hit;
		if (Physics.Raycast (aimRay, out hit, 500.0F)) 
		{
			targetHitObj.transform.position = hit.point;
			//Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
			targetPointer.transform.LookAt (targetHitObj.transform);
			//Vector3 differenceVector = targetHitObj.transform.position - transform.position;
			GameObject Camera_rotAroundX = GameObject.Find("Main Camera");
			GameObject Cam_Container_rotAroundY  = GameObject.Find("MouseEyetrack Controller");

			// http://answers.unity3d.com/questions/254130/how-do-i-rotate-an-object-towards-a-vector3-point.html
			//find the vector pointing from our position to the target
			_direction = (targetHitObj.transform.position - transform.position).normalized;

			//create the rotation we need to be in to look at the target
			_lookRotation = Quaternion.LookRotation(_direction);
			
			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

			//transform.Rotate(0, 20f, 0, Space.Self);
			/*
			 * Cam_Container_rotAroundY.transform.LookAt(new Vector3(targetHitObj.transform.position.x			                                  transform.position.y,
			                                  targetHitObj.transform.position.z));
*/
/*
			Camera_rotAroundX.transform.LookAt(new Vector3(transform.position.x,
			                                  targetHitObj.transform.position.y,
			                                  targetHitObj.transform.position.z));
*/
/*
			float newX= targetPointer.transform.forward.x;
			float newY= targetPointer.transform.forward.y;
			Vector3 newxVector = new Vector3(newX ,rotX.transform.rotation.y,rotX.transform.rotation.z);
			Vector3 newYVector = new Vector3(rotX.transform.rotation.x ,newY,rotX.transform.rotation.z);
*/
			//rotX.transform.RotateAround(Vector3.zero,Vector3.up, targetPointer.transform.rotation.x);
			//rotY.transform.RotateAround(Vector3.zero,Vector3.forward, targetPointer.transform.rotation.y);
			//rotY.transform.rotation = newYVector;

			//rotX.transform.rotation.eulerAngles = newxVector;
			//rotY.transform.rotation.eulerAngles = newYVector;

		}
		//transform.Rotate ();
	}

	void cursorHitTest()
	{
		Debug.Log ("cursorHitTest");
		int x = Screen.width / 2;
		int y = Screen.height / 2;
		Vector3 gPos = aim_sprite.GetComponent<RectTransform>().position;
		Debug.Log ("aim_sprite.position = "  +gPos);
		Vector2 convertedGUIPos = GUIUtility.GUIToScreenPoint(gPos);
		//Ray ray = camera.ScreenPointToRay(new Vector3(x, y));
		Ray ray = Camera.main.ScreenPointToRay(convertedGUIPos);
		
		//Ray ray = Camera.main.ScreenPointToRay(filteredscreenSpace);
		
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 500.0F))
		{
			
			if (hit.collider != null) 
			{
				Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
				ballBehaviour b = hit.collider.gameObject.GetComponent<ballBehaviour>();//.hit();
				if (b != null)
				{
					b.hit();
				}
			}
		}
		
		Debug.DrawRay(ray.origin, ray.direction * 1000, new Color(1f,0.922f,0.016f,1f));
	}
	public void gameState()
	{
		bGamestate = true;
		//Screen.showCursor = false;
		Screen.lockCursor = true;
		mouseLook_X_script.enabled = true;
		mouseLook_Y_script.enabled = true;
		charMotor.jumping.enabled = false;
	}
	
	public void menuState()
	{
		bGamestate = false;
		Screen.lockCursor = false;
		Screen.showCursor = true;
		
		mouseLook_X_script.enabled = false;
		mouseLook_Y_script.enabled = false;
		charMotor.enabled = false;
	}
}