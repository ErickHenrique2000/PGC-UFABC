using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public Text      titulo;
	public InputField altura;
	public InputField largura;

	public Slider volumeSlider;
	public AudioMixer mixer;

	public bool fullScreen = true;
	public int alturaTela = 768;
	public int larguraTela = 1366;

	public GameObject menuPrincipalUI;
	public GameObject menuOpUI;
	public GameObject levelSelectUI;

	public AudioSource   audioUI;
	public AudioSource 	 menuSong;
	public AudioClip     menuSound;
	public AudioClip     buttonAC;
	public Button[] loadLevelBt;

	void Start () 
	{
		menuPrincipalUI.SetActive(true);
		menuOpUI.SetActive(false);
		levelSelectUI.SetActive(false);

		menuSong = GameObject.Find ("UI").GetComponent<AudioSource> ();
		menuSong.clip = menuSound;
		
		audioUI = GameObject.Find ("MenuSound").GetComponent<AudioSource> ();

		audioUI.clip = buttonAC;
		menuSong.Play ();
	}

	public void LoadLevel(int levelIndex)
    {
		audioUI.Play();
		SceneManager.LoadScene(levelIndex);
    }

	public void volumeConfig(float sliderValue)
	{
		mixer.SetFloat ("Volume", Mathf.Log10(sliderValue) * 20);
	}
		
	public void QuitGame()
	{
		audioUI.Play ();
		Application.Quit();
	}

	public void TelaCheia()
	{
		audioUI.Play ();
		Screen.SetResolution(Screen.width, Screen.height, fullScreen);
	}

	public void ModoJanela()
	{
		audioUI.Play ();
		Screen.SetResolution(Screen.width, Screen.height, !fullScreen);
	}

	public void Resolucao800x600()
	{
		audioUI.Play ();
		Screen.SetResolution (800, 600, fullScreen);
	}

	public void Resolucao1024x768()
	{
		audioUI.Play ();
		Screen.SetResolution (1024, 768, fullScreen);
	}

	public void Resolucao1280x720()
	{
		audioUI.Play ();
		Screen.SetResolution (1280, 720, fullScreen);
	}

	public void Resolucao1366x768()
	{
		audioUI.Play ();
		Screen.SetResolution (1366, 768, fullScreen);
	}

		public void Resolucao1920x1080()
	{
		audioUI.Play ();
		Screen.SetResolution (1920, 1080, fullScreen);
	}
}
