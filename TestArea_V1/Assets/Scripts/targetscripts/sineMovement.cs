using UnityEngine;
using System.Collections;

public class sineMovement : MonoBehaviour {
	public float start_Angle_Offset = 67;
	public float amplitudeX = 10.0f;
	public float amplitudeY = 4.0f;
	public float omegaX = 0.5f;
	public float omegaY = 2.0f;

	public float xOffset = 0f;
	public float yOffset = 0f;
	public float zOffset = 0f;

	float index;

	// Use this for initialization
	void Start () {
	
	}
	

	
	public void Update(){
		index  += Time.deltaTime;
		float x = amplitudeX*Mathf.Cos (omegaX*index + start_Angle_Offset) + xOffset;
		float z = (amplitudeX*Mathf.Sin (omegaX*index + start_Angle_Offset) + zOffset)*2;//2 dont know why it makes it round
		float y = Mathf.Abs (amplitudeY*Mathf.Sin (omegaY*index)) + yOffset;
		transform.localPosition= new Vector3(x,y,z);
	}


	float sineWave()
	{
		float z = (amplitudeX*Mathf.RoundToInt(Mathf.Sin (omegaX*index)) + zOffset);
		return z;
	}

}
