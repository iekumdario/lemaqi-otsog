using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class StarEffect : MonoBehaviour, IPointerDownHandler {
    public Behaviour halo;
    Mission mission;
    bool missionsEnabled;

	// Use this for initialization
	void Start () {
		addPhysicsRaycaster();

        mission = gameObject.GetComponent<Mission>();
        List<MissionData> missions = GameControl.control.playerData.missions;

        foreach (MissionData mi in missions) {
        // for (int i = 0; i < missions.Size(); i++) {
            if (mi.missionNumber == mission.missionNumber) {
                mission.completed = mi.completed;
                mission.score = mi.score;
            }
        }

        // By Default hide all Missions.
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
        gameObject.GetComponent<Renderer>().enabled = enabled;
        Component halo = gameObject.GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, enabled, null);
        missionsEnabled = enabled;
    }

	public void OnPointerDown(PointerEventData eventData) {
        GameObject missionObject = eventData.pointerCurrentRaycast.gameObject;
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

        // Appy this to save Game state.
        /*if (GameControl.control.playerData.missions.Exists(m => m.missionNumber == mission.missionNumber)) {
            MissionData missionData = GameControl.control.playerData.missions.Find(m => m.missionNumber == mission.missionNumber);
            missionData.completed = mission.completed;
            missionData.score = mission.score;
            GameControl.control.playerData.missions.Remove(missionData);
            GameControl.control.playerData.missions.Add(missionData);
        }
        else {
            MissionData missionData = new MissionData();
            missionData.missionNumber = mission.missionNumber;
            missionData.completed = mission.completed;
            missionData.score = mission.score;
            GameControl.control.playerData.missions.Add(missionData);
        }
        GameControl.control.Save();*/
    }

	void addPhysicsRaycaster() {
        PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null) {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }
}
