using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Totem : MonoBehaviour, Iinteractive {
    public UnityEvent unityEvent;

    public Vector2 roomDimensions;
    public float playerRadius;
    public LayerMask enemiesLayer;
    public LayerMask playerLayer;

    private bool player;
    private bool enemies;

    public void interaction() {
        unityEvent.Invoke();
    }

    void Update() {
        if (!player || enemies || !Input.GetKeyDown(KeyCode.E)) {
            return;
        }

        interaction();
    }

    private void FixedUpdate() {
        player = Physics2D.OverlapCircle(transform.position, playerRadius, playerLayer);
        enemies = Physics2D.OverlapBox(transform.position, roomDimensions, 1, enemiesLayer);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, playerRadius);
        Gizmos.DrawWireCube(transform.position, roomDimensions);
    }
}
