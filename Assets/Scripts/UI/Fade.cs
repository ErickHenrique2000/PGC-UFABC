using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private Image image;
    public float duration;
    public static Fade Instance;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, 1);
        image.enabled = true;

        image.DOFade(0, duration).OnComplete(() => { image.enabled = false; });
    }

    public void FadeToGameOver()
    {
        image.enabled = true;
        image.color = new Color(0, 0, 0, 0);
        image.DOFade(1, duration).OnComplete(() => { NewPlayerScript.Player.LoadGameOver(); });
    }

    public void FadeToGame(string fase)
    {
        image.enabled = true;
        image.color = new Color(0, 0, 0, 0);
        image.DOFade(1, duration).OnComplete(() => { SceneManager.LoadScene(fase); });
    }
}
