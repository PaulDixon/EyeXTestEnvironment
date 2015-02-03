using UnityEngine;
using System.Collections;

public class RayCastCollide : MonoBehaviour {
	public GameObject particle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			Debug.Log("mouse fire");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (collider.Raycast(ray, out hit, 100.0F))
				Debug.Log(ray.origin.ToString() + " " + hit.point.ToString());
			Debug.DrawLine(ray.origin, hit.point);
			particle.transform.position = hit.point;
			
			
		}
	}
}
