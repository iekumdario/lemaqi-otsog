using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class StarEffect : MonoBehaviour, IPointerDownHandler {
    public Behaviour halo;
    GameObject[] missionObjects;
    bool missionsEnabled;

	// Use this for initialization
	void Start () {
		addPhysicsRaycaster();

        // By Default hide all Missions.
        missionObjects = GameObject.FindGameObjectsWithTag("MissionObject");
        toggleMissions(false);
	}

    void Update() {
        if (Camera.main.fieldOfView <= 15 && !missionsEnabled) {
            toggleMissions(true);
        }
        else if (Camera.main.fieldOfView > 15 & missionsEnabled) {
            toggleMissions(false);
        }
    }

    void toggleMissions(bool enabled) {
        foreach (GameObject missionObject in missionObjects) {
            missionObject.GetComponent<Renderer>().enabled = enabled;
            Component halo = missionObject.GetComponent("Halo");
            halo.GetType().GetProperty("enabled").SetValue(halo, enabled, null);
        }
        missionsEnabled = enabled;
    }

	public void OnPointerDown(PointerEventData eventData) {
        GameObject missionObject = eventData.pointerCurrentRaycast.gameObject;
        Mission mission = missionObject.GetComponent<Mission>();
        GameObject missionDetails = GameObject.FindGameObjectWithTag("MissionDetails");

        for (int i = 0; i < missionDetails.transform.childCount; i++) {
        	switch (missionDetails.transform.GetChild(i).name) {
        		case "Title":
        			Text title = missionDetails.transform.GetChild(i).GetComponent<Text>();
        			title.text = mission.title;
        			break;
        		case "Description":
        			Text description = missionDetails.transform.GetChild(i).GetComponent<Text>();
        			description.text = mission.description;
        			break;
        		case "Image":
        			Image image = missionDetails.transform.GetChild(i).GetComponent<Image>();
        			image.sprite = mission.image;
        			break;
        	}
        }
        missionDetails.GetComponent<Canvas>().enabled = true;
        GameObject missionManager = GameObject.FindGameObjectWithTag("MissionManager");
        missionManager.GetComponent<MissionManager>().mission = mission;
    }

	void addPhysicsRaycaster() {
        PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null) {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }
}
