using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject menu;
    public GameObject skill;
    public GameObject missoes;

    private void Start() {
        Resume();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            missoes.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.M)) {
            missoes.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }

       
    }

    public void Resume() {
        menu.SetActive(false);
        skill.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        menu.SetActive(true);
        skill.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void mainMenu() {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("menu");
    }

    public void quit() {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void ChangeDifficulty(int newDiff) {
        DifficultyController.instance.setDifficulty((Difficulty)newDiff);
    }
}
