using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{
    [Range(1, 10)]
    public float maxSpeed;

    [Range(0.1f, 100f)]
    public float accelration = 50, deAcceleration = 50;
}
