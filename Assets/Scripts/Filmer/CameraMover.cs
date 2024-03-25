using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
    public float passoX;
    public float passoY;
    public Vector3 posInicial;

    private GameObject camera;
    private GameObject playerCamera;

    // Start is called before the first frame update
    void Start() {
        camera = GameObject.Find("Main Camera");
        playerCamera = GameObject.Find("PlayerCamera");
        playerCamera.SetActive(false);
        camera.transform.position = posInicial;
    }

    private void FixedUpdate() {
        camera.transform.position = new Vector3(camera.transform.position.x + (passoX * Time.fixedDeltaTime), camera.transform.position.y + (passoY * Time.fixedDeltaTime), camera.transform.position.z);
    }
}
