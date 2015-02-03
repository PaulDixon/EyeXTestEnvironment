using UnityEngine;
using System.Collections;

public class SpinOnGaze : MonoBehaviour {

	private GazeAwareComponent _gazeAware;
	private UserPresenceComponent _userPresence;
	
	void Start () 
	{
		_gazeAware = GetComponent<GazeAwareComponent>();
		_userPresence = GetComponent<UserPresenceComponent>();

	}
	
	void Update () 
	{
		if (!_userPresence.IsUserPresent)
		{
			Debug.Log("WHERE ARE YOU!!!");
		}

// */
		if (_gazeAware.HasGaze) 
		{
			transform.Rotate(Vector3.up);
		}
	}
}
