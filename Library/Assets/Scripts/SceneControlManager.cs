using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour {

    /*private static SceneControlManager instance { get { return s_instance; } }
    private static SceneControlManager s_instance;

    private void Awake() {
        if (s_instance != null) { Destroy(gameObject); return; }
        s_instance = this; DontDestroyOnLoad(gameObject);
    }*/

    public float autoLoadNextLevelAfter;

    void Start() {
        if (autoLoadNextLevelAfter <= 0) {
            Debug.Log("Level auto load disabled, use a postive number is seconds");
        } else {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }
    }

    public void LoadLevel(string name) {
        Debug.Log("New Level load: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest() {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
