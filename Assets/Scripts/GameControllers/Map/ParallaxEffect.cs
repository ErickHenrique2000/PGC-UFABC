using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private Vector3 startPosition;
    [SerializeField]
    private Vector2 parallaxEffectMultiplier;


    private void Start()
    {
        startPosition = transform.position;
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        lastCameraPosition = cameraTransform.position;
    }

    
    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        if (Mathf.Abs(deltaMovement.x) < 2f) transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y, deltaMovement.z);
        lastCameraPosition = cameraTransform.position;
    }

    public void Reset()
    {
        transform.position = startPosition;
    }
}
