using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotationScript : MonoBehaviour {
	public float speed = 1f;

	public float direction = 1f;
	[HideInInspector]
	public float directionChangeSpeed = 2f;
	
	// Update is called once per frame
	void Update () {
		/*if (direction < 1f) {
			direction += Time.deltaTime / (directionChangeSpeed / 2);
		}*/

		transform.Rotate(Vector3.up, (speed * direction) * Time.deltaTime);
	}
}
