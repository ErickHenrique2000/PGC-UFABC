using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public GameObject   pauseMenu, opMenu;
    public bool         isPaused = false;
    public AudioSource  audioUI;
    public AudioClip    buttonSound;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = Pause();
        }
    }

    public void PauseMenuPrincipal()
    {
        audioUI.Play();
        pauseMenu.SetActive(true);
        opMenu.SetActive(false);
    }

    public void CarregarMenuOptions()
    {
        audioUI.Play();
        pauseMenu.SetActive(false);
        opMenu.SetActive(true);
    }

    public void ButtonPause()
    {
        audioUI.Play();
        isPaused = Pause();
    }

    public void MainMenu()
    {
        audioUI.Play();
        SceneManager.LoadScene("Menu");
    }

    public bool Pause()
    {
        if(Time.timeScale == 0f)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            return (false);
        }else
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            PauseMenuPrincipal();
            return (true);
        }
    }

    public void VolController()
    {
        
    }
}
