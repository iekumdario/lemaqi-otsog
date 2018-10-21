using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class StartEffect : MonoBehaviour, IPointerDownHandler {
	SerializedObject halo;
	bool reset;

	// Use this for initialization
	void Start () {
		halo = new SerializedObject(gameObject.GetComponent("Halo"));
		reset = false;
		addPhysicsRaycaster();
	}
	
	// Update is called once per frame
	void Update () {
		float size = halo.FindProperty("m_Size").floatValue;
		if (size > 15 && !reset) {
			halo.FindProperty("m_Size").floatValue -= 0.5f + (0.05f * Time.deltaTime);
		}
		else {
			reset = true;
		}
		if (size < 25 && reset) {
			halo.FindProperty("m_Size").floatValue += 0.5f + (0.05f * Time.deltaTime);
		}
		else {
			reset = false;
		}
		halo.ApplyModifiedProperties();
	}

	public void OnPointerDown(PointerEventData eventData) {
        GameObject missionObject = eventData.pointerCurrentRaycast.gameObject;
        Mission mission = missionObject.GetComponent<Mission>();
        GameObject missionDetails = GameObject.FindGameObjectWithTag("MissionDetails");

        for (int i = 0; i < missionDetails.transform.childCount; i++) {
        	switch (missionDetails.transform.GetChild(i).name) {
        		case "Title":
        			Text[] title = missionDetails.transform.GetChild(i).GetComponents<Text>();
        			title[0].text = mission.title;
        			break;
        		case "Description":
        			Text[] description = missionDetails.transform.GetChild(i).GetComponents<Text>();
        			description[0].text = mission.description;
        			break;
        		case "Image":
        			Image[] image = missionDetails.transform.GetChild(i).GetComponents<Image>();
        			image[0].sprite = mission.image;
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
