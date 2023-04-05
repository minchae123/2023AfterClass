using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField]
    private MovementDataSO movementData; 
    
    protected float currentVelocity = 0;
    protected Vector2 movementDirection;

    public UnityEvent<float> OnVelocityChange; //플레이어의 속도가 변했을 때 발생하는 이벤트 

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void StopImmediately()
    {
        rigid.velocity = Vector2.zero;
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
            currentVelocity += movementData._acceleration * Time.deltaTime;
        }else
        {
            currentVelocity -= movementData._deAcceleration * Time.deltaTime;
        }
        return Mathf.Clamp(currentVelocity, 0, movementData._maxSpeed);
    }

    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(currentVelocity); //현재 속도를 계속 업데이트 시켜준다.
        rigid.velocity = movementDirection * currentVelocity;
    }
}
