using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMoviment : BasicEnemyController
{
    public Vector3          initialPosition;
    public float            movementLenght;
    public bool             isVertical;
    void Start()
    {
        initialPosition = transform.position;
        if(healthBar != null)
        {
            healthBar.SetMaxHealth(health);
        }   
    }

    protected override void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        if(isVertical)
        {
            VerticalMovement();
        }else
        {
            HorizontalMovement();
        }
    }

    void VerticalMovement()
    {
        transform.position = new Vector3(initialPosition.x, 
        initialPosition.y + Mathf.PingPong(Time.time, movementLenght), initialPosition.z);
    }

    void HorizontalMovement()
    {
        transform.position = new Vector3(initialPosition.x + Mathf.PingPong(Time.time, movementLenght), 
        initialPosition.y, initialPosition.z);
    }
}