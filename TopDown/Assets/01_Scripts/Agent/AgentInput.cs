using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class AgentInput : MonoBehaviour, IAgentInput
{
    [field: SerializeField] public UnityEvent<Vector2> OnMovementKeyPress { get; set; }
    [field: SerializeField] public UnityEvent<Vector2> OnPointerPositionChanged { get; set; }
    [field: SerializeField] public UnityEvent OnFireButtonPress { get; set; }
    [field: SerializeField] public UnityEvent OnFireButtonRelease { get; set; }

    private bool fireButtonDown = false; // �Ѿ� ��ư ���� ���� ����

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
    }

    private void GetFireInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if(fireButtonDown == false) // ó������ ��ư�� �����Ÿ� ������ ����
            {
                fireButtonDown = true;
                OnFireButtonPress?.Invoke();
            }
        }
        else
        {
            if (fireButtonDown == true) // ��ư�� �ôµ� ������ �����ִ°Ŷ��
            {
                fireButtonDown = false;
                OnFireButtonRelease?.Invoke(); // ��ư ���� ����
            }
        }
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector2 mouseInWorldPos = MainCam.ScreenToWorldPoint(mousePos);
        OnPointerPositionChanged?.Invoke(mouseInWorldPos);
    }

    private void GetMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        OnMovementKeyPress?.Invoke(new Vector2(h, v));
    }
}
