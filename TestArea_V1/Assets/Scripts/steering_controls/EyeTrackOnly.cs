using UnityEngine;
using System.Collections;

public class EyeTrackOnly : MonoBehaviour 
{

	//values that will be set in the Inspector
	public Transform Target;
	GameObject aim_sprite;
	public float RotationSpeed;
	EyeXGazePoint _gazePoint;

	
	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Use this for initialization
	void Start () 
	{
		aim_sprite = GameObject.Find ("Aim") as GameObject;
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 trackAim = _gazePoint.Screen;
		Ray aimRay = Camera.main.ScreenPointToRay(trackAim);

		RaycastHit hit;
		if (Physics.Raycast (aimRay, out hit, 500.0F)) 
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

	//30 fps
	public void updateEyeTrack(EyeXGazePoint gazePoint)
	{
		_gazePoint = gazePoint;
	}
	

}
