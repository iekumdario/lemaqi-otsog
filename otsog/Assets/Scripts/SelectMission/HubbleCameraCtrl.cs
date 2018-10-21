using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubbleCameraCtrl : MonoBehaviour {
 	float rotationSpeed = 0.2f;
 	float speed = 30f;
 	float ZoomAmount = 0;

 	float touchSpeed = 1f;
    float pointerY;
    float pointerX;
	float f_lastX = 0.0f;
	float f_lastY = 0.0f;
	float f_difX = 0.5f;
	float f_difY = 0.5f;
	int i_direction = 1;

	[SerializeField]
	Camera camera;

    void Update() {
    	if (Input.touchSupported) {
    		if (Input.touchCount == 1) {
		        pointerY = Input.touches[0].deltaPosition.y;
		        f_difY = Mathf.Abs(f_lastY - pointerY);
				pointerX = Input.touches[0].deltaPosition.x;
		        f_difX = Mathf.Abs(f_lastX - pointerX);
		        var touch = Input.GetTouch(0);

		        if (f_lastY < Input.GetAxis("Mouse Y")) {
	                i_direction = -1;
	                transform.Rotate(Vector3.right, -f_difY * touchSpeed * Time.deltaTime, 0);
	            }
	            if (f_lastY > Input.GetAxis("Mouse Y")) {
	                i_direction = 1;
	                transform.Rotate(Vector3.right, f_difY * touchSpeed * Time.deltaTime, 0);
	            }

	            if (f_lastX < Input.GetAxis("Mouse X")) {
	                i_direction = -1;
	                transform.Rotate(Vector3.up, -f_difX * touchSpeed * Time.deltaTime, 0);
	            }
	            if (f_lastX > Input.GetAxis("Mouse X")) {
	                i_direction = 1;
	                transform.Rotate(Vector3.up, f_difX * touchSpeed * Time.deltaTime, 0);
	            }
		        f_lastY = -pointerY;
		        f_lastX = -pointerX;
		        f_difY = 500f;
		        f_difX = 500f;
	    	}
	    	else if (Input.touchCount == 2) {
	    		if (camera.fieldOfView > 1) {
					camera.fieldOfView--;
				}
	    	}
	    	else {
	    		if (camera.fieldOfView < 30 ) {
					camera.fieldOfView++;
				}
	    	}
    	}
    	else if (Input.GetMouseButton(0)) {
    		float xRotate = Input.GetAxis("Mouse X") * speed * Mathf.Deg2Rad;
			float yRotate = Input.GetAxis("Mouse Y") * speed * Mathf.Deg2Rad;

			if (xRotate != 0) {
				transform.Rotate(Vector3.up, xRotate, 0);
			}
			if (yRotate != 0) {
				transform.Rotate(Vector3.right, -yRotate, 0);
			}
    	}
    	else if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			if (camera.fieldOfView > 1) {
	            camera.fieldOfView--;
	        }
	    }
	    else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
	        if (camera.fieldOfView < 30) {
	            camera.fieldOfView++;
	        }
	    }
    }
}