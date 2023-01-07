using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public bool mainMenu = false;
    public static SceneController current;
    public GameObject DeathMenu;
    public GameObject PauseMenu;
    public GameObject PacifistTutorialPrefab;
    public GameObject ViolentTutorialPrefab;

    public Transform PacifistStartPoint;
    public Transform ViolentStartPoint;
    public int nextSceneIndex;
    public int previousSceneIndex;

    private bool paused = false;

    private void Awake() {
        current = this;
    }

    void EventHandler(string eventType) {
        if (!mainMenu) {
            if (eventType == "PacifistChoice" && SceneManager.GetActiveScene().buildIndex == 1) {
                Player.current.SetPosition(PacifistStartPoint);
                Instantiate(PacifistTutorialPrefab);
            } else if (eventType == "ViolentChoice" && SceneManager.GetActiveScene().buildIndex == 3) {
                Player.current.SetPosition(ViolentStartPoint);
                Instantiate(ViolentTutorialPrefab);
            }
        }
    }

    private void Start() {
        if (!mainMenu) {

            DeathMenu.SetActive(false);
            PauseMenu.SetActive(false);
            if (StoryEngine.current.HasOccured("PacifistChoice")) {
                Player.current.SetPosition(PacifistStartPoint);
                if (SceneManager.GetActiveScene().buildIndex == 1) {
                    Instantiate(PacifistTutorialPrefab);
                }
            } else if (StoryEngine.current.HasOccured("ViolentChoice")) {
                Player.current.SetPosition(ViolentStartPoint);
                if (SceneManager.GetActiveScene().buildIndex == 3) {
                    Instantiate(ViolentTutorialPrefab);
                }
            }
            StoryEngine.current.EventOccured += EventHandler;
        }
    }

    private void Update() {
        if (!mainMenu) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (paused) {
                    UnPause();
                } else {
                    Pause();
                }
            }
        }
    }

    public void Pause() {
        paused = true;
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void UnPause() {
        paused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }

    public void Restart() {
        StoryEngine.current.RestartFromCheckpoint();
    }

    public void PlayerDied() {
        DeathMenu.SetActive(true);
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNext() {
        StoryEngine.current.CheckPoint();
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadPrev() {
        StoryEngine.current.CheckPoint();
        SceneManager.LoadScene(previousSceneIndex);
    }

    public void ExitGame() {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
