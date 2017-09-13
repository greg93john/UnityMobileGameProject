using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneInfo : MonoBehaviour {

    private static Object instance;
    private void Start() {
        if (instance) { Destroy(this.gameObject); } else { instance = this; DontDestroyOnLoad(this.gameObject); }
    }

    const string divisionKey = "DIVISION";
    public void SetDivisionChoice(string choice) {
        PlayerPrefs.SetString(divisionKey, choice.ToLower());
    } public string GetDivisionChoice() {
        return PlayerPrefs.GetString(divisionKey);
    }

}
