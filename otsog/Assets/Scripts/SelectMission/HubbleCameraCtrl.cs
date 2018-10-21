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
	public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;

	[SerializeField]
	Camera camera;

	void Start() {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

    void Update() {
    	if (Input.touchSupported) {
    		if (Input.touchCount == 1) {
		        pointerY = Input.touches[0].deltaPosition.y;
		        f_difY = Mathf.Abs(f_lastY - pointerY);
				pointerX = Input.touches[0].deltaPosition.x;
		        f_difX = Mathf.Abs(f_lastX - pointerX);
		        var touch = Input.GetTouch(0);

		        if (f_lastY < Input.GetAxis("Mouse Y")) {
	                i_direction = 1;
	                transform.Rotate(Vector3.right, f_difY * touchSpeed * Time.deltaTime, 0);
	            }
	            if (f_lastY > Input.GetAxis("Mouse Y")) {
	                i_direction = -1;
	                transform.Rotate(Vector3.right, -f_difY * touchSpeed * Time.deltaTime, 0);
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
		        // f_difY = 500f;
		        // f_difX = 500f;
	    	}

	    	if (Input.touchCount == 2) {
	            // Store both touches.
	            Touch touchZero = Input.GetTouch(0);
	            Touch touchOne = Input.GetTouch(1);

	            // Find the position in the previous frame of each touch.
	            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
	            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

	            // Find the magnitude of the vector (the distance) between the touches in each frame.
	            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
	            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

	            // Find the difference in the distances between each frame.
	            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

	            // If the camera is orthographic...
	            if (camera.orthographic)
	            {
	                // ... change the orthographic size based on the change in distance between the touches.
	                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

	                // Make sure the orthographic size never drops below zero.
	                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 29.9f);
	            }
	            else
	            {
	                // Otherwise change the field of view based on the change in distance between the touches.
	                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

	                // Clamp the field of view to make sure it's between 10 and 60.
	                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 29.9f, 59.9f);
	            }
	        }
    	}
    	else if (Input.GetMouseButton(0)) {
    		float xRotate = Input.GetAxis("Mouse X") * speed * Mathf.Deg2Rad;
			float yRotate = Input.GetAxis("Mouse Y") * speed * Mathf.Deg2Rad;

			if (xRotate != 0) {
				transform.Rotate(Vector3.up, -xRotate, 0);
			}
			if (yRotate != 0) {
				transform.Rotate(Vector3.right, yRotate, 0);
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