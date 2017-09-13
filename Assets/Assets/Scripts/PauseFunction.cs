using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunction : MonoBehaviour {

    public GameObject pausePanel;

    private bool isPaused = false;

    private void Start() {
        isPaused = false;
        pausePanel.SetActive(isPaused);
    }

    public void TogglePause() {
        isPaused = !isPaused;
        if (isPaused) { Time.timeScale = 0; } else { Time.timeScale = 1f; }
        pausePanel.SetActive(isPaused);
    }
}
