using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundControllerScript : MonoBehaviour
{
    public static AudioClip espadada;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GameObject.Find("SoundController").GetComponent<AudioSource>();
        espadada = Resources.Load<AudioClip> ("espadaPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "espada":
                audioSrc.PlayOneShot(espadada);
                break;
        }
    }
}
