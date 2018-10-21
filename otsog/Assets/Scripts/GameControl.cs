using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
	public PlayerData playerData;
	static String saveFileDir = "";

	public static GameControl control;

	void Awake() {
		if (control == null) {
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if (control != this) {
			Destroy(gameObject);
		}
		saveFileDir = Application.persistentDataPath + "/playerData.dat";
		LoadGame();
	}

	public void Save() {
		FileStream file;
		BinaryFormatter bf = new BinaryFormatter();
		file = File.Create(saveFileDir);
    	bf.Serialize(file, playerData);
    	file.Close();
	}

	public void LoadGame() {
    	if (File.Exists(saveFileDir)) {
    		BinaryFormatter bf = new BinaryFormatter();
    		FileStream file = File.Open(saveFileDir, FileMode.Open);
    		playerData = (PlayerData) bf.Deserialize(file);
    		file.Close();
    	}
    }
}

[Serializable]
public class PlayerData {
	public List<MissionData> missions = new List<MissionData>();
	public int elements = 0;
}

[Serializable]
public class MissionData {
	public int missionNumber;
	public bool completed = false;
	public float score = 0;
}
