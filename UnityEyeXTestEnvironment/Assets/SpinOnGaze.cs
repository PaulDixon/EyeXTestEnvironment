using UnityEngine;
using System.Collections;

public class SpinOnGaze : MonoBehaviour {


	private GazeAwareComponent _gazeAware;
	private UserPresenceComponent _userPresence;
	private GazePointDataComponent _lastGazePoint;

	
	void Start () 
	{

		_gazeAware = GetComponent<GazeAwareComponent>();
		_userPresence = GetComponent<UserPresenceComponent>();
		_lastGazePoint = GetComponent<GazePointDataComponent>();

	
	}/*
	
	public static float DistanceToLine(Ray ray, Vector3 point)
	{
		return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
	}
	*/


	void Update () 
	{



			// Get the last gaze point.

			if (_lastGazePoint.LastGazePoint.IsValid) {
						// Convert the fixation data to screen space.
						Vector2 screenSpace = _lastGazePoint.LastGazePoint.Screen;
						//Debug.Log (screenSpace.ToString());
		

				} else {
			Debug.Log("Not Valid gaze point");
				}


		if (!_userPresence.IsUserPresent) {
					//	Debug.Log ("WHERE ARE YOU!!!");
						renderer.enabled = false;

				}
		else {
			renderer.enabled = true;
				}


// */
		if (_gazeAware.HasGaze) {
			transform.Rotate (Vector3.up);
			renderer.material.color = Color.red;
		} else {
			renderer.material.color = Color.white;		
		}
		/*
		if (Physics.Raycast (ray, hit, 100.0)) {
			//hit.distance is the distance to the first collider.
			distanceToGround = hit.distance;
			//This draws a line, that shows the ray, in the editor.
			Debug.DrawLine (ray.origin, hit.point);
		}
		*/
	}
}
