using UnityEngine;
using System.Collections;

public class iconFade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("Fade");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Fade() {
		for (float f = 1f; f >= 0; f -= 0.01f) {
			Color c = renderer.material.color;
			if (f <= 0.01f)
			{
				f = 0;
				Destroy(this);
			}
			c.a = f;

			renderer.material.color = c;
			yield return null;
		}
	}
}
