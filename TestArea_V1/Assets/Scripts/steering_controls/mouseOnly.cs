using UnityEngine;
using System.Collections;



public class mouseOnly : MonoBehaviour {
	MouseLook mouseLook_X_script;
	MouseLook mouseLook_Y_script;
	CharacterMotor charMotor;
	GameObject aim_sprite;
	bool bGamestate = false;


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
		if (Input.GetKeyDown (KeyCode.W)) {
			if(bGamestate)
			{
				menuState();

			}
			else
			{
				gameState();
			}
		}

		if (Input.GetMouseButtonDown(0))
			cursorHitTest();

		if (Input.GetMouseButtonDown(1))
			cursorHitTest();
		
		if (Input.GetMouseButtonDown(2))
			cursorHitTest();

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
