using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D rigid;

    protected float currentVelocity = 0;
    protected Vector2 movementDirection;

    [SerializeField] private MovementDataSO movementDataSO;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void MoveAgent(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0)
        {
            if(Vector2.Dot(movementInput, movementDirection) < 0)
            {
                currentVelocity = 0;
            }
            movementDirection = movementInput.normalized;
        }
        currentVelocity = CalcSpeed(movementInput);
    }

    private float CalcSpeed(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0)
        {
            currentVelocity += movementDataSO.accelration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= movementDataSO.deAcceleration * Time.deltaTime;
        }
        return Mathf.Clamp(currentVelocity, 0, 5);
    }

    private void FixedUpdate()
    {
        rigid.velocity = movementDirection * currentVelocity;
    }
}
