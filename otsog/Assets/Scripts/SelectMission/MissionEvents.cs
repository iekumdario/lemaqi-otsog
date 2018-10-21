using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionEvents : MonoBehaviour {

	public void loadByIndex(GameObject missionObject) {
		MissionManager missionManager = missionObject.GetComponent<MissionManager>();
		DontDestroyOnLoad(missionManager);
        DontDestroyOnLoad(missionManager.mission);
        SceneManager.LoadScene(missionManager.mission.scene);
	}

	public void closeCanvas(Canvas canvas) {
		canvas.enabled = false;
	}
}
