using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour, IAgentInput
{

    [field: SerializeField] public UnityEvent<Vector2> OnMovementKeyPress { get; set; }
    [field: SerializeField] public UnityEvent<Vector2> OnPointerPositionChanged { get; set; }
    [field: SerializeField] public UnityEvent OnFireButtonPress { get; set; }
    [field: SerializeField] public UnityEvent OnFireButtonRelease { get; set; }

    private void Update()
    {
        GetMovementInput();
    }

    private void GetMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        OnMovementKeyPress?.Invoke(new Vector2(h, v));
    }
}
