using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {

    public GameObject settingsMenu;

    private bool isActive = false;
	// Use this for initialization
	void Start () {
        isActive = false;
        settingsMenu.SetActive(false);
	}

    public void ToggleActiveness() {
        isActive = !isActive;
        if (isActive) { Time.timeScale = 0f; } else { Time.timeScale = 1f; }
        settingsMenu.SetActive(isActive);
    }
}
