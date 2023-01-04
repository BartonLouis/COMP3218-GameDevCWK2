using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController current;
    public GameObject DeathMenu;
    public GameObject PauseMenu;
    private void Awake() {
        current = this;
    }

    private void Start() {
        DeathMenu.SetActive(false);
        PauseMenu.SetActive(false);
    }

    public void Pause() {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void UnPause() {
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
}
