using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaseController : MonoBehaviour
{
    public string nomeFase;
    // Start is called before the first frame update
    void Start()
    {
        nomeFase = SceneManager.GetActiveScene().name;
    }

}
