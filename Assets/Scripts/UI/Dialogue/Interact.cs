using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject message;

    // Update is called once per frame
    void Update()
    {
        bool input = Input.GetKeyDown(KeyCode.E);
        if (input && message.activeSelf) {
            message.SetActive(false);
        } else {
            bool enemyNear = Physics2D.OverlapCircle(this.transform.position, 1.5f, playerLayer);
            if(input && enemyNear && !message.activeSelf) {
                message.SetActive(true);
            }
        }

    }
}
